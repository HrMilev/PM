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
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using PM.Domain;
using PM.Data.ServiceCollectionExtension;
using PM.Application.ServiceCollectionExtension;
using PM.Infrastructure.ServiceCollectionExtension;
using PM.Data.ApplicationBuilderExtension;
using Microsoft.AspNetCore.Http;
using Microsoft.Data.Sqlite;
using IdentityServer4.Extensions;

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
            var connection = new SqliteConnection(Configuration.GetConnectionString("SqliteConnection"));
            connection.Open();

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlite(connection,
                    b => b.MigrationsAssembly("PM.Data")));

            services.AddAutoMapper(typeof(Startup));

            services.AddDefaultIdentity<ApplicationUser>()
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddErrorDescriber<MultilanguageIdentityErrorDescriber>();

            services.AddIdentityServer(o =>
                {
                    o.UserInteraction.LoginUrl = "/Login";
                })
                .AddApiAuthorization<ApplicationUser, ApplicationDbContext>(options =>
                {
                    options.IdentityResources["openid"].UserClaims.Add("name");
                    options.ApiResources.Single().UserClaims.Add("name");
                    options.IdentityResources["openid"].UserClaims.Add("role");
                    options.ApiResources.Single().UserClaims.Add("role");
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
            services.AddPMData();
            services.AddPMServices();
            services.AddPMApplication();
            services.AddPMInfrastructure();
            services.AddDatabaseDeveloperPageExceptionFilter();
            services.AddCors(options =>
            {
                options.AddPolicy(name: "myApps",
                                  builder =>
                                  {
                                      builder.WithOrigins("http://localhost:3000")
                                      .AllowAnyHeader()
                                      .AllowAnyMethod();
                                  });
            });
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
                //app.Use(async (ctx, next) =>
                //{
                //    ctx.SetIdentityServerOrigin("https://blazorpm.azurewebsites.net");
                //    await next();
                //});
            }

            app.UseHttpsRedirection();
            app.UseCookiePolicy(new CookiePolicyOptions { MinimumSameSitePolicy = SameSiteMode.Lax });
            app.UseBlazorFrameworkFiles();
            app.UseStaticFiles();

            app.UseRouting();
            app.UseCors("myApps");

            app.UseIdentityServer();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseGrpcWeb();

            app.UseMiddleware<CultureMiddleware>();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
                endpoints.MapControllers();
                endpoints.MapBlazorHub();
                endpoints.MapFallbackToFile("index.html");
            });
        }
    }
}
