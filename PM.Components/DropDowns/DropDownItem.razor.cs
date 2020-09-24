using Microsoft.AspNetCore.Components;

namespace PM.Components.DropDowns
{
    public partial class DropDownItem<T>
    {
        [CascadingParameter(Name = "DropDown")]
        public DropDown<T> DropDown { get; set; }

        [Parameter]
        public T Item { get; set; }
        [Parameter]
        public RenderFragment<T> ChildContent { get; set; }

    }
}
