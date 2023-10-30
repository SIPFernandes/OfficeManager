using OfficeManager.Shared.Entities;
using OfficeManager.Shared.Request_Model;
using OfficeManagerApp.Areas.Services.Implementations;
using OfficeManagerApp.Data.Constants;

namespace OfficeManagerApp.Areas.Services.CompanyServices.OfficeServices.RoomServices.FacilityServices
{
    public class FacilityHttpService : GenericHttpService<Facility, FacilityRequestModel, Facility>, IFacilityHttpService
    {
        public override string Url { get => @AppConst.ApiUrlConst.CompaniesApi.FacilityUrl; set => base.Url = value; }

        public FacilityHttpService(HttpClient httpClient) : base(httpClient)
        {
        }
    }
}
