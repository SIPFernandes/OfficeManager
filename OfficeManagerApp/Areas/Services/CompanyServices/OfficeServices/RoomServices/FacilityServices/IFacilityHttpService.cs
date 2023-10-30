using OfficeManager.Shared.Entities;
using OfficeManager.Shared.Request_Model;
using OfficeManagerApp.Areas.Services.Interfaces;

namespace OfficeManagerApp.Areas.Services.CompanyServices.OfficeServices.RoomServices.FacilityServices
{
    public interface IFacilityHttpService : IGenericHttpService<Facility, FacilityRequestModel, Facility>
    {
    }
}
