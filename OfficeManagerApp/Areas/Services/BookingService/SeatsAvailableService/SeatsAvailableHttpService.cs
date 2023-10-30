using OfficeManager.Shared.Entities;
using OfficeManager.Shared.Request_Model;
using OfficeManager.Shared.Response_Model;
using OfficeManagerApp.Areas.Services.Implementations;
using OfficeManagerApp.Data.Constants;
using System.Net;
using System.Net.Http.Json;

namespace OfficeManagerApp.Areas.Services.BookingService.SeatsAvailableService
{
    public class SeatsAvailableHttpService : GenericHttpService<SeatsAvailable, SeatsAvailableRequestModel, SeatsAvailable>, ISeatsAvailableHttpService
    {
        public override string Url
        {
            get => @AppConst.ApiUrlConst.BookingApi.SeatsAvailableUrl;
            set => base.Url = value;
        }

        public SeatsAvailableHttpService(HttpClient httpClient)
            : base(httpClient)
        {
        }

        public async Task<IEnumerable<SeatsAvailable>> GetSeatsUnavailableByDate(DateTime date)
        {
            var dateConverted = date.ToString("yyyy-MM-ddThh:mm:ss").Replace(":", "%3A");

            var seatsUnavailables = await _httpClient.GetFromJsonAsync<List<SeatsAvailable>>(Url + "/Date/" + dateConverted);

            return seatsUnavailables;
        }
        
        public async Task<SeatsAvailable> GetSeatsLeftByRoomIdDate(int roomId, DateTime date)
        {
            var dateConverted = date.ToString("yyyy-MM-ddThh:mm:ss").Replace(":", "%3A");

            var seatsAvailable = await _httpClient.GetFromJsonAsync<SeatsAvailable>(Url + "/Room/" + roomId + "/Date/" + dateConverted);

            return seatsAvailable;
        }

        public async Task UpdateRoomSeatsAvailability(SeatsAvailableRequestModel seatsAvailable, bool increaseSeatsAvailable)
        {
            var response = await _httpClient.PutAsJsonAsync($"{Url}/{increaseSeatsAvailable}", seatsAvailable);

            if (response.StatusCode.Equals(HttpStatusCode.BadRequest))
            {
                var content = await response.Content.ReadFromJsonAsync<BadResponseModel>();

                throw new Exception(string.Join(",", content.Errors));
            }
        }
    }
}
