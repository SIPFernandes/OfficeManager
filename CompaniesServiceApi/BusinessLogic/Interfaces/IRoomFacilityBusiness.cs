using OfficeManager.Shared.Entities;


namespace CompaniesServiceApi.BusinessLogic.Interfaces
{
    public interface IRoomFacilityBusiness : IGenericBusiness<RoomFacility>
    {
        Task<IEnumerable<RoomFacility>> GetFacilities(int roomId);
    }
}
