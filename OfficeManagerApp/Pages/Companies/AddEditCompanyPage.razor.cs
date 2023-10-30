using Microsoft.AspNetCore.Components;
using OfficeManager.Shared.Request_Model;
using OfficeManagerApp.Areas.Services.CompanyServices;
using OfficeManagerApp.Areas.Services.Interfaces;
using OfficeManagerApp.Data.Constants;
using OfficeManagerApp.Shared;

namespace OfficeManagerApp.Pages.Companies
{
    public partial class AddEditCompanyPage : ComponentBase
    {
        [CascadingParameter]
        public Loading LoadingComponent { get; set; }

        [Parameter]
        public int CompanyId { get; set; }

        [Inject]
        private ICompanyHttpService CompanyHttpService { get; set; }

        [Inject]
        private NavigationManager _navigationManager { get; set; }

        private CompanyRequestModel companyRequestModel;

        private string msg = null;

        private List<string> messages { get; set; }

        private IList<string> imageSources = new List<string>();

        protected async override Task OnInitializedAsync()
        {
            LoadingComponent.OpenLoadingComponent();

            if (CompanyId != 0)
            {
                var company = await CompanyHttpService.Get(CompanyId);

                companyRequestModel = new CompanyRequestModel()
                {
                    Name = company.Name,
                    Description = company.Description,
                    Image = company.Image?.File
                };
            }
            else
            {
                companyRequestModel = new CompanyRequestModel(){};
            }

            if(!string.IsNullOrEmpty(companyRequestModel.Image))
            {
                imageSources.Add(companyRequestModel.Image);
            }

            LoadingComponent.CloseLoadingComponent();
        }

        protected async Task CreateOrEditCompany()
        {
            LoadingComponent.OpenLoadingComponent();

            msg = null;

            messages = new List<string>();

            try
            {
                if (CompanyId == 0)
                {
                    await CompanyHttpService.Insert(companyRequestModel);
                }
                else
                {
                    await CompanyHttpService.Update(companyRequestModel, CompanyId);
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
            else
            {
                Cancel();
            }
        }

        private void Cancel()
        {
            _navigationManager?.NavigateTo(@AppConst.CompanyUrlConst.CompaniesPage);
        }

        private async Task DeleteCompany()
        {
            LoadingComponent.OpenLoadingComponent();

            await CompanyHttpService.Delete(CompanyId);

            _navigationManager?.NavigateTo(@AppConst.CompanyUrlConst.CompaniesPage);
        }

        private void OnUploadImageChanged(IList<string> images)
        {
            companyRequestModel.Image = images.Count == 0 ? null : images.First();

            imageSources = images;
        }
    }
}