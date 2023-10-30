using Microsoft.AspNetCore.Components;
using OfficeManager.Shared.Entities;
using OfficeManagerApp.Areas.Services.CompanyServices;
using OfficeManagerApp.Areas.Services.Interfaces;
using OfficeManagerApp.Shared;

namespace OfficeManagerApp.Pages.Rooms
{
    public partial class RoomDetailsPage : ComponentBase
    {
        [CascadingParameter]
        public Loading LoadingComponent { get; set; }

        [Parameter]
        public int RoomId { get; set; }

        [Inject]
        private ICompanyHttpService CompanyHttpService { get; set; }

        [Inject]
        public IJSInteropService JSInteropService { get; set; }

        private Room room;

        private IList<Facility> _allRoomFacilities = new List<Facility>();

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {            
            ElementReference _buttonReference = HideRevealButton.getElement();

            await JSInteropService.HideRevealButton(_buttonReference);
        }

        protected async override Task OnInitializedAsync()
        {
            LoadingComponent.OpenLoadingComponent();

            room = await CompanyHttpService.OfficeHttpService
                .RoomHttpService.Get(RoomId);

            foreach(var roomFacility in room.RoomFacilities)
            {
                _allRoomFacilities.Add(roomFacility.Facility);
            }

            LoadingComponent.CloseLoadingComponent();
        }
    }
}
