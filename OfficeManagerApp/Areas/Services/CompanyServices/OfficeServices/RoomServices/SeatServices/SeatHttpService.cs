using OfficeManager.Shared.Entities;
using OfficeManager.Shared.Request_Model;
using OfficeManagerApp.Areas.Services.Implementations;
using OfficeManagerApp.Data.Constants;
using System.Net.Http.Json;

namespace OfficeManagerApp.Areas.Services.CompanyServices.OfficeServices.RoomServices.SeatServices
{
    public class SeatHttpService : GenericHttpService<Seat, SeatRequestModel, Seat>, ISeatHttpService
    {
        public override string Url { get => @AppConst.ApiUrlConst.CompaniesApi.SeatUrl; set => base.Url = value; }
        public string UrlSeat { get => @AppConst.ApiUrlConst.CompaniesApi.SeatRoomUrl; set => base.Url = value; }

        public SeatHttpService(HttpClient httpClient) : base(httpClient)
        {
        }

        public async Task<Seat> GetSeatByCoordinates(int roomId, int x, int y)
        {
            var seat = await _httpClient.GetFromJsonAsync<Seat>(UrlSeat + "/" + roomId 
                + "/" + Convert.ToInt64(x) + "/" + Convert.ToInt64(y));

            return seat;
        }

        public async Task<IEnumerable<Seat>> GetSeatsByRoomId(int roomId)
        {
            var seats = await _httpClient.GetFromJsonAsync<IEnumerable<Seat>>(UrlSeat + "/" + roomId);

            return seats;
        }
    }
}
