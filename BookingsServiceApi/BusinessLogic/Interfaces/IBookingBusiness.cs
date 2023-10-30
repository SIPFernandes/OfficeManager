using OfficeManager.Shared.Entities;

namespace BookingsServiceApi.BusinessLogic.Interfaces
{
    public interface IBookingBusiness : IGenericBusiness<Booking>
    {
        Task<IEnumerable<Booking>> GetBookingsByDateRoomId(int roomId, DateTime date);
        Task<IEnumerable<Booking>> GetBookingsByDate(DateTime date);
    }
}
