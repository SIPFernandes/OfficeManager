using OfficeManager.Shared.Entities;
using OfficeManager.Shared.Request_Model;
using OfficeManagerApp.Areas.Services.Interfaces;

namespace OfficeManagerApp.Areas.Services.BookingService.SeatsAvailableService
{
    public interface ISeatsAvailableHttpService : IGenericHttpService<SeatsAvailable, SeatsAvailableRequestModel, SeatsAvailable>
    {
        Task<IEnumerable<SeatsAvailable>> GetSeatsUnavailableByDate(DateTime date);
        Task<SeatsAvailable> GetSeatsLeftByRoomIdDate(int roomId, DateTime date);
        Task UpdateRoomSeatsAvailability(SeatsAvailableRequestModel seatsAvailable, bool increaseSeatsAvailable);
    }
}
