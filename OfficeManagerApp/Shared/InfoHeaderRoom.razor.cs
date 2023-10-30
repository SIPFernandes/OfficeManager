using OfficeManager.Shared.Entities;
using Microsoft.AspNetCore.Components;
using OfficeManagerApp.Areas.Services.CompanyServices;
using OfficeManagerApp.Areas.Services.CompanyServices.OfficeServices.LocationServices;

namespace OfficeManagerApp.Shared
{
    public partial class InfoHeaderRoom : ComponentBase
    {
        [Parameter]
        public Room Room { get; set; }
        [Inject]
        private ICompanyHttpService CompanyHttpService { get; set; }        
        private bool isFavorite = false;

        protected override async Task OnInitializedAsync()
        {
            Room.Office = await CompanyHttpService.OfficeHttpService.Get(Room.OfficeId);

            Room.Office.Location = await CompanyHttpService.OfficeHttpService
                .LocationHttpService.Get(Room.Office.LocationId);
        }

        private void manageFavorite()
        {
            isFavorite = !isFavorite;
        }
    }
}