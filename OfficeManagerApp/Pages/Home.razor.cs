using Microsoft.AspNetCore.Components;
using OfficeManager.Shared.Entities;
using OfficeManagerApp.Areas.Services.CompanyServices;
using OfficeManagerApp.Shared;

namespace OfficeManagerApp.Pages
{
    public partial class Home : ComponentBase
    {
        [CascadingParameter]
        public User User { get; set; }

        [CascadingParameter]
        public Loading LoadingComponent { get; set; }

        [Inject]
        private ICompanyHttpService CompanyHttpService { get; set; }

        private IList<Room> allRoomsList;

        private IList<Office> allOfficesList;

        protected override async Task OnInitializedAsync()
        {
            LoadingComponent.OpenLoadingComponent();

            allRoomsList = await CompanyHttpService.OfficeHttpService
                .RoomHttpService.GetAll();

            allOfficesList = await CompanyHttpService.OfficeHttpService.GetAll();

            LoadingComponent.CloseLoadingComponent();
        }

    }
}