using OfficeManager.Shared.Entities;
using OfficeManager.Shared.Request_Model;
using OfficeManagerApp.Areas.Services.CompanyServices.OfficeServices;
using OfficeManagerApp.Areas.Services.Interfaces;

namespace OfficeManagerApp.Areas.Services.CompanyServices
{
    public interface ICompanyHttpService : IGenericHttpService<Company, CompanyRequestModel, Company>
    {
        public IOfficeHttpService OfficeHttpService { get; }
    }
}
