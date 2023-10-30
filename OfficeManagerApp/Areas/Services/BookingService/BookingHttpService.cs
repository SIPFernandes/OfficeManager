using OfficeManager.Shared.Entities;
using OfficeManager.Shared.Request_Model;
using OfficeManagerApp.Data.Constants;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Components.Authorization;
using OfficeManagerApp.Areas.Services.Implementations;
using OfficeManagerApp.Areas.Services.BookingService.SeatsAvailableService;

namespace OfficeManagerApp.Areas.Services.BookingService
{
    public class BookingHttpService : GenericHttpService<Booking, BookingRequestModel, Booking>, IBookingHttpService
    {
        public ISeatsAvailableHttpService SeatsAvailableHttpService { get => _seatsAvailableHttpService; }
        private readonly ISeatsAvailableHttpService _seatsAvailableHttpService;

        public override string Url
        {
            get => @AppConst.ApiUrlConst.BookingApi.BookingUrl;
            set => base.Url = value;
        }

        public BookingHttpService(HttpClient httpClient, AuthenticationStateProvider authenticationStateProvider)
            : base(httpClient, authenticationStateProvider)
        {
            _seatsAvailableHttpService = new SeatsAvailableHttpService(_httpClient);
        }

        public async Task<IList<Booking>> GetBookingsByDateRoomId(int roomId, DateTime date)
        {
            var dateConverted = date.ToString("yyyy-MM-ddThh:mm:ss").Replace(":", "%3A");

            var bookings = await _httpClient.GetFromJsonAsync<List<Booking>>(Url + "/" + roomId + "/" + dateConverted);

            return bookings;
        }

        public async Task<IList<Booking>> GetBookingsByDate(DateTime date)
        {
            var dateConverted = date.ToString("yyyy-MM-ddThh:mm:ss").Replace(":", "%3A");

            var bookings = await _httpClient.GetFromJsonAsync<List<Booking>>(Url + "/Date/" + dateConverted);

            return bookings;
        }
    }
}
