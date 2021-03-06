﻿using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using PM.Common.JSUtilities;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace PM.Components.CulturesComponents
{
    public partial class CultureSelector
    {
        [Inject] IJSRuntime JSRuntime { get; set; }
        [Inject] NavigationManager NavigationManager { get; set; }
        [Parameter]
        public IEnumerable<string> AvailableCultures { get; set; }
        private IEnumerable<CultureInfo> GetAvailableCultures()
        {
            return AvailableCultures.Where(x => x != CultureInfo.CurrentCulture.Name).Select(x => new CultureInfo(x));
        }
        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender && !AvailableCultures.Any(x => x == CultureInfo.CurrentCulture.Name))
            {
                CurrentCultureISO = GetAvailableCultures().First().TwoLetterISOLanguageName.ToUpper();
                await SetCultureAsync(GetAvailableCultures().First());
            }
        }
        public string CurrentCultureISO { get; set; } = CultureInfo.CurrentCulture.TwoLetterISOLanguageName.ToUpper();
        public async Task SetCultureAsync(CultureInfo culture)
        {
            if (CultureInfo.CurrentCulture.Name != culture.Name)
            {
                await JSRuntime.InvokeVoidAsync(JSFunctions.SetBlazorCulture, culture.Name);
                NavigationManager.NavigateTo(NavigationManager.Uri, forceLoad: true);
            }
        }
    }
}
