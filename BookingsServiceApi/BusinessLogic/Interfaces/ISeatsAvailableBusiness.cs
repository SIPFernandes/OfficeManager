using OfficeManager.Shared.Entities;

namespace BookingsServiceApi.BusinessLogic.Interfaces
{
    public interface ISeatsAvailableBusiness : IGenericBusiness<SeatsAvailable>
    {
        Task<IEnumerable<SeatsAvailable>> GetSeatsUnavailableByDate(DateTime date);
        Task<SeatsAvailable> GetSeatsLeftByRoomIdDate(int roomId, DateTime date);
        public Task UpdateRoomSeatsAvailability(SeatsAvailable seatsAvailable, bool increaseSeatsAvailable);
    }
}
