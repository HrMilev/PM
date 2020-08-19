using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using PM.WebApp.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PM.WebApp.Shared.Buttons
{
    public partial class BrowserHistoryBackButton
    {
        [Inject] IJSRuntime JSRuntime { get; set; }
        public async Task Back()
        {
            await JSRuntime.InvokeVoidAsync(JSFunctions.BrowserHistoryBack);
        }
    }
}
