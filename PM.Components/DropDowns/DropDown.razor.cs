using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PM.Components.DropDowns
{
    public partial class DropDown<T>
    {
        [Parameter]
        public RenderFragment InitialTip { get; set; }
        [Parameter]
        public EventCallback<T> OnSelected { get; set; }
        [Parameter]
        public IEnumerable<T> Items { get; set; }
        [Parameter]
        public RenderFragment<T> ItemTemplate { get; set; }
        [Parameter]
        public int ExtraContentWidth { get; set; }

        private bool show;
        private RenderFragment tip;

        protected override void OnInitialized() { tip = InitialTip; }
        public async Task HandleSelectAsync(T item, RenderFragment<T> contentFragment)
        {
            tip = contentFragment.Invoke(item);
            show = false;
            await OnSelected.InvokeAsync(item);
        }
    }
}
