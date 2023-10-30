using CompaniesServiceApi.Data.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using OfficeManager.Shared.Entities;
using OfficeManager.Shared.Exceptions;

namespace CompaniesServiceApi.Data.Services.Implementations
{
    public class LocationService : GenericService<LocationModel>, ILocationService
    {
        public LocationService(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        public override async Task<IEnumerable<LocationModel>> GetAll()
        {
            return await entities
                .Where(x => x.IsDeleted != true)
                .ToListAsync();
        }

        public override async Task<LocationModel?> Get(int id)
        {
            var location = await entities.SingleAsync(x => x.Id == id);

            if (location != null && !location.IsDeleted)
            {
                return location;
            }
            else
            {
                throw new EntityDoesNotExistException();
            }

        }

        public async Task<bool> CheckLocationExist(int id, string country, string city)
        {
            return await entities
                .AnyAsync(b => b.Country == country && b.City == city && b.Id != id && b.IsDeleted == false);
        }
    }
}
