using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using PM.WebApp.Constants;
using PM.WebApp.StaticData;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace PM.WebApp.Shared
{
    public partial class CultureSelector
    {
        [Inject] IJSRuntime JSRuntime { get; set; }
        [Inject] NavigationManager NavigationManager { get; set; }
        public IEnumerable<CultureInfo> AvailableCultures = SupportedCultures.Cultures.Where(x => x.Name != CultureInfo.CurrentCulture.Name);
        public string CurrentCultureISO { get; set; } = CultureInfo.CurrentCulture.TwoLetterISOLanguageName.ToUpper();
        public async Task SetCulture(CultureInfo culture)
        {
            if (CultureInfo.CurrentCulture.Name != culture.Name)
            {
                await JSRuntime.InvokeVoidAsync(JSFunctions.SetBlazorCulture, culture.Name);
                NavigationManager.NavigateTo(NavigationManager.Uri, forceLoad: true);
            }
        }
    }
}
