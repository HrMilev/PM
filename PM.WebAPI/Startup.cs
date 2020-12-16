using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PM.WebAPI.Models;
using PM.Localizations;
using GoogleReCaptcha.V3.Interface;
using GoogleReCaptcha.V3;
using PM.WebAPI.Middlewares;
using PM.WebAPI.Configurations;
using AutoMapper;
using PM.Data;
using System.Text.Json;
using PM.WebAPI.Extensions;
using PM.Data.Entities;
using System.IdentityModel.Tokens.Jwt;

namespace PM.WebAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection"),
                    b => b.MigrationsAssembly("PM.WebAPI")));

            services.AddAutoMapper(typeof(Startup));

            services.AddDefaultIdentity<ApplicationUser>()
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddErrorDescriber<MultilanguageIdentityErrorDescriber>();

            services.AddIdentityServer(o =>
                {
                    o.UserInteraction.LoginUrl = "/Login";
                })
                .AddApiAuthorization<ApplicationUser, ApplicationDbContext>(o =>
                {
                    o.IdentityResources["openid"].UserClaims.Add("role");
                });
            services.AddHttpContextAccessor();
            services.AddAuthentication()
                .AddIdentityServerJwt();
            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Remove("role");
            services.AddControllersWithViews()
                .AddDataAnnotationsLocalization(o =>
                {
                    o.DataAnnotationLocalizerProvider = (type, factory) => factory.Create(typeof(Localization));
                })
                .AddJsonOptions(o =>
                {
                    o.JsonSerializerOptions.IgnoreNullValues = true;
                });
            services.Configure<IdentityOptions>(o =>
            {
                o.User.RequireUniqueEmail = true;
            });
            services.AddRazorPages();
            services.AddServerSideBlazor();
            services.AddGrpc();
            services.AddHttpClient<ICaptchaValidator, GoogleReCaptchaValidator>();
            services.AddLocalization(opts =>
            {
                opts.ResourcesPath = "Resources";
            });
            services.AddPMServices();
            services.AddDatabaseDeveloperPageExceptionFilter();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.EnsureMigrationOf<ApplicationDbContext>();
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseMigrationsEndPoint();
                app.UseWebAssemblyDebugging();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseBlazorFrameworkFiles();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseIdentityServer();
            app.UseAuthentication();
            app.UseAuthorization();
            app.SeedAdmin();
            app.UseGrpcWeb();

            app.UseMiddleware<CultureMiddleware>();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
                endpoints.MapControllers();
                endpoints.MapBlazorHub();
                endpoints.MapGrpcService<GreeterService>().EnableGrpcWeb();
                endpoints.MapFallbackToFile("index.html");
            });
        }
    }
}
