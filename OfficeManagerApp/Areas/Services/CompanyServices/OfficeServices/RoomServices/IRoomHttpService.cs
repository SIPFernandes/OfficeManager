using OfficeManager.Shared.Entities;
using OfficeManager.Shared.Request_Model;
using OfficeManagerApp.Areas.Services.CompanyServices.OfficeServices.RoomServices.FacilityServices;
using OfficeManagerApp.Areas.Services.CompanyServices.OfficeServices.RoomServices.SeatServices;
using OfficeManagerApp.Areas.Services.Interfaces;

namespace OfficeManagerApp.Areas.Services.CompanyServices.OfficeServices.RoomServices
{
    public interface IRoomHttpService : IGenericHttpService<Room, RoomRequestModel, Room>
    {
        public IFacilityHttpService FacilityHttpService { get; }
        public ISeatHttpService SeatHttpService { get; }
        Task<IList<Room>> GetRoomsByOfficeId(int officeId);
        Task<IList<Room>> GetAvailableRooms(IList<int> roomsIds);
        Task<IList<Room>> GetAvailableRoomsByOfficeId(int officeId, IList<int> unavailableRoomsIds);
        Task<IList<RoomFacility>> GetFacilities(int roomId);
        Task<IDictionary<int, Room>> GetRoomsByIds(IList<int> roomIds);
    }
}
