using OfficeManager.Shared.Entities;
using OfficeManager.Shared.Request_Model;
using OfficeManagerApp.Areas.Services.CompanyServices.OfficeServices.LocationServices;
using OfficeManagerApp.Areas.Services.CompanyServices.OfficeServices.RoomServices;
using OfficeManagerApp.Areas.Services.Interfaces;

namespace OfficeManagerApp.Areas.Services.CompanyServices.OfficeServices
{
    public interface IOfficeHttpService : IGenericHttpService<Office, OfficeRequestModel, Office>
    {
        public IRoomHttpService RoomHttpService { get; }
        public ILocationHttpService LocationHttpService { get; }
        Task<IList<Office>> GetOfficesByCompanyId(int companyId);
        Task<int> Insert(OfficeRequestModel office);
    }
}
