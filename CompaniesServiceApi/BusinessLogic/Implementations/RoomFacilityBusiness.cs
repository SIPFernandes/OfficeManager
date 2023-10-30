using CompaniesServiceApi.BusinessLogic.Interfaces;
using CompaniesServiceApi.Data.Services.Interfaces;
using OfficeManager.Shared.Entities;

namespace CompaniesServiceApi.BusinessLogic.Implementations
{
    public class RoomFacilityBusiness : GenericBusiness<IRoomFacilityService, RoomFacility>, IRoomFacilityBusiness
    {
        public RoomFacilityBusiness(IRoomFacilityService service, ILogger<RoomFacilityBusiness> logger) : base(service, logger)
        {
        }

        public async Task<IEnumerable<RoomFacility>> GetFacilities(int roomId)
        {
            return await Service.GetFacilities(roomId);
        }
    }
}
