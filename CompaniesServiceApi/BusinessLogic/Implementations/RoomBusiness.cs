using CompaniesServiceApi.BusinessLogic.Interfaces;
using CompaniesServiceApi.Data.Services.Interfaces;
using OfficeManager.Shared.Entities;
using OfficeManager.Shared.Exceptions;

namespace CompaniesServiceApi.BusinessLogic.Implementations
{
    public class RoomBusiness : GenericBusiness<IRoomService, Room>, IRoomBusiness
    {
        public RoomBusiness(IRoomService service, ILogger<RoomBusiness> logger) : base(service, logger)
        {
        }

        public async Task<IEnumerable<Room>> GetRoomsByOfficeId(int officeId)
        {
            return await Service.GetRoomsByOfficeId(officeId);
        }
        
        public async Task<IEnumerable<Room>> GetAvailableRooms(IList<int> unavailableRoomsId)
        {
            return await Service.GetAvailableRooms(unavailableRoomsId);
        }
        
        public async Task<IEnumerable<Room>> GetAvailableRoomsByOfficeId(int officeId, IList<int> unavailableRoomsId)
        {
            return await Service.GetAvailableRoomsByOfficeId(officeId, unavailableRoomsId);
        }

        public override async Task<bool> Validate(Room room)
        {
            if (await Service.CheckRoomExist(room.Id, room.Name, room.OfficeId))
            {
                throw new EntityDuplicateException("room in this office");
            }
            return true;
        }

        public async Task<IDictionary<int, Room>> GetRoomsByIds(IList<int> roomIds)
        {
            return await Service.GetRoomsByIds(roomIds);
        }
    }
}
