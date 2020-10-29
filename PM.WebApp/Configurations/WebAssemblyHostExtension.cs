using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.JSInterop;
using PM.Common.JSUtilities;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace PM.WebApp.Configurations
{
    public static class WebAssemblyHostExtension
    {
        public static async Task SetCulture(this IJSRuntime js)
        {
            var result = await js.InvokeAsync<string>(JSFunctions.GetBlazorCulture);
            if (result != null)
            {
                var culture = new CultureInfo(result);
                CultureInfo.DefaultThreadCurrentCulture = culture;
                CultureInfo.DefaultThreadCurrentUICulture = culture;
            }
        }
    }
}
