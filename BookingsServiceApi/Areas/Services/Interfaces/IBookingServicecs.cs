using OfficeManager.Shared.Entities;

namespace BookingsServiceApi.Data.Services.Interfaces
{
    public interface IBookingService : IGenericService<Booking>
    {
        Task<bool> CheckBookingExist(int id, int userId, int roomId, DateTime date);
        Task<IEnumerable<Booking>> GetBookingsByDateRoomId(int roomId, DateTime date);
        Task<IEnumerable<Booking>> GetBookingsByDate(DateTime date);
    }
}
