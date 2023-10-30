using OfficeManager.Shared.Entities;

namespace CompaniesServiceApi.Data.Services.Interfaces
{
    public interface IRoomService : IGenericService<Room>
    {
        Task<IEnumerable<Room>> GetRoomsByOfficeId(int officeId);
        Task<IEnumerable<Room>> GetAvailableRooms(IList<int> unavailableRoomsId);
        Task<IEnumerable<Room>> GetAvailableRoomsByOfficeId(int officeId, IList<int> unavailableRoomsId);
        Task<bool> CheckRoomExist(int id, string name, int officeId);
        Task DeleteByOfficeId(int officeId);
        Task<IDictionary<int, Room>> GetRoomsByIds(IList<int> roomIds);
    }
}
