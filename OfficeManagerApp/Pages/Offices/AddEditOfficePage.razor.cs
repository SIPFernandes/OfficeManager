using Microsoft.AspNetCore.Components;
using OfficeManager.Shared.Entities;
using OfficeManager.Shared.Request_Model;
using OfficeManagerApp.Areas.Services.CompanyServices;
using OfficeManagerApp.Data.Constants;
using OfficeManagerApp.Data.Constants.ConstsEN;
using OfficeManagerApp.Shared;

namespace OfficeManagerApp.Pages.Offices
{
    public partial class AddEditOfficePage : ComponentBase
    {
        [CascadingParameter]
        public Loading LoadingComponent { get; set; }

        [Parameter]
        public int CompanyId { get; set; }

        [Parameter]
        public int OfficeId { get; set; }

        [Parameter]
        public EventCallback<bool> AddLocationChanged { get; set; }

        [Inject]
        private ICompanyHttpService CompanyHttpService { get; set; }

        [Inject]
        private NavigationManager NavigationManager { get; set; }

        private List<string> messages;
        private OfficeRequestModel officeRequestModel;
        private IList<LocationModel> _allLocations = new List<LocationModel>();
        private IList<string> _allSteps;
        private string msg = null;
        private int _stepperPage = 1;
        private bool _isValid = false;                
        private string _companyName;

        protected async override Task OnInitializedAsync()
        {
            LoadingComponent.OpenLoadingComponent();

            if (OfficeId != 0)
            {
                var office = await CompanyHttpService.OfficeHttpService.Get(OfficeId);

                officeRequestModel = new OfficeRequestModel()
                {
                    Name = office.Name,
                    CompanyId = office.CompanyId,
                    LocationId = office.LocationId,
                    Image = office.Image?.File
                };
            }
            else
            {
                officeRequestModel = new OfficeRequestModel()
                {
                    CompanyId = CompanyId
                };
            }

            var company = await CompanyHttpService.Get(CompanyId);

            _companyName = company.Name;
            
            _allLocations = await CompanyHttpService.OfficeHttpService
                .LocationHttpService.GetAll();

            _allSteps = new List<string>() 
            { 
                ConstEN.OfficeDetails, 
                ConstEN.SelectLocation, 
                ConstEN.ConfirmDetails 
            };

            LoadingComponent.CloseLoadingComponent();
        }

        protected async Task CreateOrEditOffice(bool isToAddRoom)
        {
            LoadingComponent.OpenLoadingComponent();

            msg = null;
            messages = new List<string>();

            try
            {
                if (OfficeId == 0)
                {
                    OfficeId = await CompanyHttpService.OfficeHttpService.Insert(officeRequestModel);
                }
                else
                {
                    await CompanyHttpService.OfficeHttpService.Update(officeRequestModel, OfficeId);
                }
            }
            catch (Exception ex)
            {
                msg = ex.Message;
            }

            LoadingComponent.CloseLoadingComponent();

            if (msg != null)
            {
                messages = msg.Split(',').ToList();
            }

            _stepperPage++;

            if(isToAddRoom)
            {
                NavigationManager.NavigateTo(string.Format(AppConst.RoomUrlConst.RoomIdPageFormat, OfficeId, officeRequestModel.Name, 0));
            }
            else
            {
                Cancel();
            }

        }

        protected void Cancel()
        {
            NavigationManager.NavigateTo(string.Format(AppConst.OfficeUrlConst.OfficesPageFormat, CompanyId));
        }

        protected async Task DeleteOffice()
        {
            LoadingComponent.OpenLoadingComponent();

            await CompanyHttpService.OfficeHttpService.Delete(OfficeId);

            NavigationManager.NavigateTo(string.Format(AppConst.OfficeUrlConst.OfficesPageFormat, CompanyId));
        }

        protected void ChangeStepperPage(bool IsNext)
        {
            if (IsNext)
            {
                _stepperPage++;
            }
            else
            {
                _stepperPage--;
            }
        }

        protected void OnStepperPageChanged(int stepperPage)
        {
            _stepperPage = stepperPage;
        }

        private void OnOfficeLocationChanged(LocationModel location)
        {
            officeRequestModel.LocationId = location.Id;     
            
            officeRequestModel.Location = location;           

            _stepperPage++;
        }

        private void OnIsValidChanged(bool isValid)
        {
            _isValid = isValid;
        }

        private void OnAddLocationModalDialog(LocationModel location)
        {
            _allLocations.Add(location);

            StateHasChanged();
        }
    }
}