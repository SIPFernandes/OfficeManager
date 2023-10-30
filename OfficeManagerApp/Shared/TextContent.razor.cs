using OfficeManager.Shared.Entities;
using Microsoft.AspNetCore.Components;

namespace OfficeManagerApp.Shared
{
    public partial class TextContent : ComponentBase
    {
        [Parameter]
        public Room Room { get; set; }
    }
}
