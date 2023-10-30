using OfficeManager.Shared.Entities;


namespace CompaniesServiceApi.BusinessLogic.Interfaces
{
    public interface IRoomBusiness : IGenericBusiness<Room>
    {
        Task<IEnumerable<Room>> GetRoomsByOfficeId(int officeId);
        Task<IEnumerable<Room>> GetAvailableRooms(IList<int> unavailableRoomsId);
        Task<IEnumerable<Room>> GetAvailableRoomsByOfficeId(int officeId, IList<int> unavailableRoomsId);
        Task<IDictionary<int, Room>> GetRoomsByIds(IList<int> roomIds);
    }
}
