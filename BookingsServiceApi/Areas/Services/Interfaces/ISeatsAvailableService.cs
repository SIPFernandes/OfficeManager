using BookingsServiceApi.Data.Services.Interfaces;
using OfficeManager.Shared.Entities;

namespace BookingsServiceApi.Areas.Services.Interfaces
{
    public interface ISeatsAvailableService : IGenericService<SeatsAvailable>
    {
        Task<SeatsAvailable> CheckBookingSeatExist(int roomId, DateTime date);
        Task<IEnumerable<SeatsAvailable>> GetSeatsUnavailableByDate(DateTime date);
        Task<SeatsAvailable> GetSeatsLeftByRoomIdDate(int roomId, DateTime date);
    }
}
