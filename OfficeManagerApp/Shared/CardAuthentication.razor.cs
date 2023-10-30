using Microsoft.AspNetCore.Components;

namespace OfficeManagerApp.Shared
{
    public partial class CardAuthentication : ComponentBase
    {
        [Parameter]
        public string Title { get; set; }

        [Parameter]
        public string Subtitle { get; set; }

        [Parameter]
        public RenderFragment ContentPage { get; set; }

        [Parameter]
        public string CustomCssClass { get; set; } = string.Empty;

        [Parameter]
        public string LastWord { get; set; }
    }
}