using OfficeManager.Shared.Entities;

namespace CompaniesServiceApi.Data.Services.Interfaces
{
    public interface ISeatService : IGenericService<Seat>
    {
        Task<IEnumerable<Seat>> GetSeatsByRoomId(int roomId);
        Task<bool> CheckSeatExist(int roomId, int tableNumber, int chairNumber);

        Task<Seat> GetSeatByCoordinates(int roomId, int coordinateX, int coordinateY);
    }
}
