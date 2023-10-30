using OfficeManager.Shared.Entities;
using OfficeManager.Shared.Request_Model;
using OfficeManager.Shared.Response_Model;
using OfficeManagerApp.Areas.Services.CompanyServices.OfficeServices.LocationServices;
using OfficeManagerApp.Areas.Services.CompanyServices.OfficeServices.RoomServices;
using OfficeManagerApp.Areas.Services.Implementations;
using OfficeManagerApp.Data.Constants;
using System.Net;
using System.Net.Http.Json;

namespace OfficeManagerApp.Areas.Services.CompanyServices.OfficeServices
{
    public class OfficeHttpService : GenericHttpService<Office, OfficeRequestModel, Office>, IOfficeHttpService
    {
        public IRoomHttpService RoomHttpService { get => _roomHttpService; }
        public ILocationHttpService LocationHttpService { get => _locationHttpService; }
        private readonly IRoomHttpService _roomHttpService;
        private readonly ILocationHttpService _locationHttpService;        

        public override string Url { get => @AppConst.ApiUrlConst.CompaniesApi.OfficeUrl; set => base.Url = value; }
        public string UrlOffice { get => @AppConst.ApiUrlConst.CompaniesApi.OfficeCompanyUrl; set => base.Url = value; }

        public OfficeHttpService(HttpClient httpClient) : base(httpClient)
        {            
            _roomHttpService = new RoomHttpService(_httpClient);
            _locationHttpService = new LocationHttpService(_httpClient);
        }

        public async Task<IList<Office>> GetOfficesByCompanyId(int companyId)
        {
            var offices = await _httpClient.GetFromJsonAsync<List<Office>>(UrlOffice + "/" + companyId);

            return offices;
        }

        public async Task<int> Insert(OfficeRequestModel office)
        {
            if (office == null)
            {
                throw new ArgumentNullException("office");
            }

            string urlInsert = Url + "/Insert";

            office.Location = null;

            var response = await _httpClient.PostAsJsonAsync(urlInsert, office);

            int officeId;

            if (response.StatusCode.Equals(HttpStatusCode.BadRequest))
            {
                var content = await response.Content.ReadFromJsonAsync<BadResponseModel>();

                throw new Exception(string.Join(", ", content.Errors));
            }
            else
            {
                officeId = await response.Content.ReadFromJsonAsync<int>();
            }

            return officeId;
        }
    }
}
