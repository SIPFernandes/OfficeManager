using OfficeManager.Shared.Request_Model;
using OfficeManager.Shared.Response_Model;
using OfficeManagerApp.Areas.Services.Implementations;
using OfficeManagerApp.Data.Constants;
using System.Net.Http.Json;
using System.Net;
using LocationModel = OfficeManager.Shared.Entities.LocationModel;

namespace OfficeManagerApp.Areas.Services.CompanyServices.OfficeServices.LocationServices
{
    public class LocationHttpService : GenericHttpService<LocationModel, LocationRequestModel, LocationModel>, ILocationHttpService
    {
        public override string Url { get => @AppConst.ApiUrlConst.CompaniesApi.LocationUrl; set => base.Url = value; }
        public LocationHttpService(HttpClient httpClient) : base(httpClient)
        {
        }

        public async Task<LocationModel> Insert(LocationRequestModel location)
        {
            if (location == null)
            {
                throw new ArgumentNullException("location");
            }

            var response = await _httpClient.PostAsJsonAsync(Url, location);

            LocationModel locationCreated;

            if (response.StatusCode.Equals(HttpStatusCode.BadRequest))
            {
                var content = await response.Content.ReadFromJsonAsync<BadResponseModel>();

                throw new Exception(string.Join(", ", content.Errors));
            }
            else
            {
                locationCreated = await response.Content.ReadFromJsonAsync<LocationModel>();
            }

            return locationCreated;
        }
    }
}