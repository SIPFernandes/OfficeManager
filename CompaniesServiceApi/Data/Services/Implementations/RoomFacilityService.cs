using CompaniesServiceApi.Data.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using OfficeManager.Shared.Entities;

namespace CompaniesServiceApi.Data.Services.Implementations
{
    public class RoomFacilityService : GenericService<RoomFacility>, IRoomFacilityService
    {
        public RoomFacilityService(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<IEnumerable<RoomFacility>> GetFacilities(int roomId)
        {
            return await entities
                .Where(x => x.RoomId == roomId && x.IsDeleted != true)
                .Include(x => x.Facility)
                .ToListAsync();
        }
    }
}
