using Microsoft.AspNetCore.Components;

namespace OfficeManagerApp.Shared
{
    public partial class OtherButton : ComponentBase
    {
        [Parameter]
        public string Url { get; set; }
    }
}
