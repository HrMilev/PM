using Microsoft.AspNetCore.Components;
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
        private IEnumerable<CultureInfo> availableCultures;
        [Parameter]
        public IEnumerable<CultureInfo> AvailableCultures
        {
            set
            {
                availableCultures = value.Where(x => x.Name != CultureInfo.CurrentCulture.Name);
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
