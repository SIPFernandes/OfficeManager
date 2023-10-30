using OfficeManager.Shared.Entities;
using OfficeManager.Shared.Request_Model;
using OfficeManagerApp.Areas.Services.BookingService.SeatsAvailableService;
using OfficeManagerApp.Areas.Services.Interfaces;

namespace OfficeManagerApp.Areas.Services.BookingService
{
    public interface IBookingHttpService : IGenericHttpService<Booking, BookingRequestModel, Booking>
    {
        public ISeatsAvailableHttpService SeatsAvailableHttpService { get; }

        Task<IList<Booking>> GetBookingsByDateRoomId(int roomId, DateTime date);

        Task<IList<Booking>> GetBookingsByDate(DateTime date);
    }
}
