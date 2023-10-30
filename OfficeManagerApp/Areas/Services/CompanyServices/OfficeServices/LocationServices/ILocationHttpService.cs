using OfficeManager.Shared.Request_Model;
using OfficeManagerApp.Areas.Services.Interfaces;
using LocationModel = OfficeManager.Shared.Entities.LocationModel;

namespace OfficeManagerApp.Areas.Services.CompanyServices.OfficeServices.LocationServices
{
    public interface ILocationHttpService : IGenericHttpService<LocationModel, LocationRequestModel, LocationModel>
    {
        Task<LocationModel> Insert(LocationRequestModel location);
    }
}
