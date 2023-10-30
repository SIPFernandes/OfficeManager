using Microsoft.AspNetCore.Components;
using OfficeManager.Shared.Entities;
using OfficeManager.Shared.Request_Model;
using OfficeManagerApp.Areas.Services.CompanyServices;
using OfficeManagerApp.Data.Constants;
using OfficeManagerApp.Shared;

namespace OfficeManagerApp.Pages.Locations
{
    public partial class AddEditLocationPage : ComponentBase
    {
        [CascadingParameter]
        public Loading LoadingComponent { get; set; }

        [Parameter]
        public int LocationId { get; set; }
        [Parameter]
        public bool IsModalDialog { get; set; } = false;
        [Parameter]
        public EventCallback<LocationModel> AddLocationChanged { get; set; }

        [Inject]
        private ICompanyHttpService CompanyHttpService { get; set; }

        [Inject]
        private NavigationManager _navigationManager { get; set; }

        private LocationRequestModel locationRequestModel;

        private LocationModel locationModel;

        private string msg = null;
        private List<string> messages { get; set; }

        protected async override Task OnInitializedAsync()
        {
            LoadingComponent.OpenLoadingComponent();

            if (LocationId != 0)
            {
                var location = await CompanyHttpService.OfficeHttpService
                    .LocationHttpService.Get(LocationId);

                locationRequestModel = new LocationRequestModel()
                {
                    Country = location.Country,
                    City = location.City
                };
            }
            else
            {
                locationRequestModel = new LocationRequestModel();
            }

            LoadingComponent.CloseLoadingComponent();
        }

        protected async Task CreateOrEditLocation()
        {
            LoadingComponent.OpenLoadingComponent();

            msg = null;
            messages = new List<string>();
            try
            {
                if (LocationId == 0)
                {
                    locationModel = await CompanyHttpService.OfficeHttpService
                        .LocationHttpService.Insert(locationRequestModel);
                }
                else
                {
                    await CompanyHttpService.OfficeHttpService
                        .LocationHttpService.Update(locationRequestModel, LocationId);
                }
            }
            catch (Exception ex)
            {
                msg = ex.Message;
            }

            if (msg != null)
            {
                messages = msg.Split(',').ToList();
            }
            else
            {
                if(!IsModalDialog)
                {
                    Cancel();
                }
            }

            await AddLocationChanged.InvokeAsync(locationModel);

            LoadingComponent.CloseLoadingComponent();

            StateHasChanged();
        }

        protected void Cancel()
        {
            _navigationManager?.NavigateTo(@AppConst.LocationUrlConst.LocationsPage);
        }

        protected async Task DeleteLocation()
        {
            LoadingComponent.OpenLoadingComponent();

            await CompanyHttpService.OfficeHttpService
                .LocationHttpService.Delete(LocationId);

            _navigationManager?.NavigateTo(@AppConst.LocationUrlConst.LocationsPage);
        }
    }
}