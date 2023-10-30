using Microsoft.AspNetCore.Components;

namespace OfficeManagerApp.Shared
{
    public partial class HideRevealButton : ComponentBase
    {
        [Parameter]
        public int RoomId { get; set; }

        private static ElementReference _buttonReference;

        public static ElementReference getElement()
        {
            return _buttonReference;
        }
    }
}
