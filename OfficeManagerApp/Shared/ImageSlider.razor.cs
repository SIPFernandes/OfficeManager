using OfficeManager.Shared.Entities;
using Microsoft.AspNetCore.Components;
using OfficeManagerApp.Areas.Services.Interfaces;

namespace OfficeManagerApp.Shared
{
    public partial class ImageSlider : ComponentBase
    {
        [Parameter]
        public Room Room { get; set; }

        [Inject]
        private IPageHistoryState PageHistoryState { get; set; }

        [Inject]
        public IJSInteropService JSInteropService { get; set; }

        private IList<string> _roomImages = new List<string>();

        protected override async Task OnInitializedAsync()
        {
            foreach(var image in Room.Images)
            {
                _roomImages.Add(image.File);
            }
        }

        private void GoBack()
        {
            PageHistoryState.NavigateBack();
        }
    }
}
