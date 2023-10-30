using CompaniesServiceApi.Data.Services.Interfaces;
using OfficeManager.Shared.Entities;
using Microsoft.EntityFrameworkCore;

namespace CompaniesServiceApi.Data.Services.Implementations
{
    public class FacilityService : GenericService<Facility>, IFacilityService
    {
        public FacilityService(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<bool> CheckFacilityExist(string name, int id)
        {
            return await entities
                .AnyAsync(b => b.Name == name && b.Id != id);
        }
    }
}
