using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Linq;
using PM.WebAPI.Data;
using PM.WebAPI.Models;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using System.Globalization;
using PM.Localizations;
using Microsoft.Extensions.Localization;
using GoogleReCaptcha.V3.Interface;
using GoogleReCaptcha.V3;
using PM.WebAPI.Middlewares;

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
                    Configuration.GetConnectionString("DefaultConnection")));

            services.AddDefaultIdentity<ApplicationUser>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddErrorDescriber<MultilanguageIdentityErrorDescriber>();

            services.AddIdentityServer(o =>
            {
                o.UserInteraction.LoginUrl = "/Login";
            })
                .AddApiAuthorization<ApplicationUser, ApplicationDbContext>();

            services.AddAuthentication()
                .AddIdentityServerJwt();
            services.Configure<IdentityOptions>(o =>
            {
                o.User.RequireUniqueEmail = true;
            });
            services.AddControllersWithViews().AddDataAnnotationsLocalization(o =>
            {
                o.DataAnnotationLocalizerProvider = (type, factory) => factory.Create(typeof(Localization));
            });
            services.AddRazorPages();
            services.AddServerSideBlazor();
            services.AddGrpc();
            services.AddHttpClient<ICaptchaValidator, GoogleReCaptchaValidator>();
            services.AddLocalization(opts =>
            {
                opts.ResourcesPath = "Resources";
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
                app.UseWebAssemblyDebugging();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseBlazorFrameworkFiles();
            app.UseStaticFiles();

            app.UseRouting();

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
                endpoints.MapGrpcService<GreeterService>().EnableGrpcWeb();
                endpoints.MapFallbackToFile("index.html");
            });
        }
    }
}
