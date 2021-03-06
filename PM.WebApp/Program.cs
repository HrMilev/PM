using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Grpc.Net.Client;
using Grpc.Net.Client.Web;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System.Globalization;
using PM.Common.JSUtilities;
using PM.WebApp.Infrastructure.Repositories.Interfaces;
using PM.WebApp.Infrastructure.Repositories;
using PM.WebApp.Infrastructure.Utils.Interfaces;
using PM.WebApp.Infrastructure.Utils;
using PM.WebApp.Configurations;

namespace PM.WebApp
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");

            builder.Services.AddHttpClient("PM.WebAPI", client => client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress))
                .AddHttpMessageHandler<BaseAddressAuthorizationMessageHandler>();
            builder.Services.AddScoped(sp => sp.GetRequiredService<IHttpClientFactory>().CreateClient("PM.WebAPI"));
            builder.Services.AddApiAuthorization(o =>
            {
                o.AuthenticationPaths.LogOutSucceededPath = "/";
                o.UserOptions.RoleClaim = "role";
            });

            builder.Services.AddLocalization(opts =>
            {
                opts.ResourcesPath = "Resources";
            });

            builder.Services.AddPMServices();
            var host = builder.Build();
            await host.Services.GetRequiredService<IJSRuntime>().SetCulture();
            await host.RunAsync();
        }
    }
}
