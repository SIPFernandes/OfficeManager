using OfficeManager.Shared.Entities;

namespace CompaniesServiceApi.Data.Services.Interfaces
{
    public interface IRoomFacilityService : IGenericService<RoomFacility>
    {
        Task<IEnumerable<RoomFacility>> GetFacilities(int roomId);
    }
}
