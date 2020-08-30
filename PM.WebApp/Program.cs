using System;
using System.Net.Http;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Text;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Grpc.Net.Client;
using GrpcGreeter;
using Grpc.Net.Client.Web;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System.Globalization;
using PM.WebApp.Constants;

namespace PM.WebApp
{
    public class Program
    {
        public static async Task Main( string[] args )
        {
            var builder = WebAssemblyHostBuilder.CreateDefault( args );
            builder.RootComponents.Add<App>("app");

            builder.Services.AddHttpClient( "PM.WebAPI", client => client.BaseAddress = new Uri( builder.HostEnvironment.BaseAddress ) )
                .AddHttpMessageHandler<BaseAddressAuthorizationMessageHandler>();

            // Supply HttpClient instances that include access tokens when making requests to the server project
            builder.Services.AddScoped( sp => sp.GetRequiredService<IHttpClientFactory>().CreateClient( "PM.WebAPI" ) );
            builder.Services.AddSingleton( services =>
            {
                // Create a gRPC-Web channel pointing to the backend server
                var httpClient = new HttpClient( new GrpcWebHandler( GrpcWebMode.GrpcWeb, new HttpClientHandler() ) );
                var baseUri = services.GetRequiredService<NavigationManager>().BaseUri;
                var channel = GrpcChannel.ForAddress( baseUri, new GrpcChannelOptions { HttpClient = httpClient } );

                // Now we can instantiate gRPC clients for this channel
                return new Greeter.GreeterClient( channel );
            } );
            builder.Services.AddApiAuthorization(o => {
                o.AuthenticationPaths.LogOutSucceededPath = "/";
            });

            builder.Services.AddLocalization( opts => { opts.ResourcesPath = "Resources"; } );
            var host = builder.Build();
            await SetCulture( host );
            await host.RunAsync();
        }

        public static async Task SetCulture(WebAssemblyHost host)
        {
            var js = host.Services.GetRequiredService<IJSRuntime>();
            var result = await js.InvokeAsync<string>( JSFunctions.GetBlazorCulture );
            if (result != null)
            {
                var culture = new CultureInfo( result );
                CultureInfo.DefaultThreadCurrentCulture = culture;
                CultureInfo.DefaultThreadCurrentUICulture = culture;
            }
        }
    }
}
