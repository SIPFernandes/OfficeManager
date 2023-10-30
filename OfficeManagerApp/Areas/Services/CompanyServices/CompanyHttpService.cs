using Microsoft.AspNetCore.Components.Authorization;
using OfficeManager.Shared.Entities;
using OfficeManager.Shared.Request_Model;
using OfficeManagerApp.Areas.Services.CompanyServices.OfficeServices;
using OfficeManagerApp.Areas.Services.Implementations;
using OfficeManagerApp.Data.Constants;

namespace OfficeManagerApp.Areas.Services.CompanyServices
{
    public class CompanyHttpService : GenericHttpService<Company, CompanyRequestModel, Company>, ICompanyHttpService
    {
        public IOfficeHttpService OfficeHttpService { get => _officeHttpService; }
        private readonly IOfficeHttpService _officeHttpService;        

        public override string Url
        {
            get => @AppConst.ApiUrlConst.CompaniesApi.CompanyUrl;
            set => base.Url = value;
        }

        public CompanyHttpService(HttpClient httpClient, AuthenticationStateProvider authenticationStateProvider) 
            : base(httpClient, authenticationStateProvider)
        {            
            _officeHttpService = new OfficeHttpService(_httpClient);
        }        
    }
}