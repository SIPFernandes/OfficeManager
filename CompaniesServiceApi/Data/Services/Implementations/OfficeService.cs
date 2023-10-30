using CompaniesServiceApi.Data.Services.Interfaces;
using OfficeManager.Shared.Entities;
using OfficeManager.Shared.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace CompaniesServiceApi.Data.Services.Implementations
{
    public class OfficeService : GenericService<Office>, IOfficeService
    {
        private readonly IRoomService _roomService;

        public OfficeService(IRoomService RoomService, ApplicationDbContext dbContext) : base(dbContext)
        {
            _roomService = RoomService;
        }

        public override async Task<IEnumerable<Office>> GetAll()
        {
            return await entities
                .Where(x => x.IsDeleted != true)
                .Include(x => x.Image)
                .Include(x => x.Location)
                .Include(x => x.Rooms)
                //.ThenInclude(x => x.Seats)
                .ToListAsync();
        }

        public override async Task<Office?> Get(int id)
        {
            var office = await entities.Include(x => x.Image).SingleAsync(x => x.Id == id);

            if (office != null && !office.IsDeleted)
            {
                return office;
            }
            else
            {
                throw new EntityDoesNotExistException();
            }

        }

        public async Task<IEnumerable<Office>> GetOfficesByCompanyId(int companyId)
        {
            return await entities
                .Where(x => x.CompanyId == companyId && x.IsDeleted != true)
                .Include(x => x.Rooms.Where(x => x.IsDeleted != true))
                .Include(x => x.Location)
                .Include(x => x.Image)
                .ToListAsync();
        }

        public async Task<bool> CheckOfficeExist(int id, string name, int companyId)
        {
            return await entities
                .AnyAsync(b => b.Name == name && b.CompanyId == companyId && b.Id != id && b.IsDeleted == false);
        }

        public override async Task Delete(Office office, bool isToSaveChanges)
        {
            await _roomService.DeleteByOfficeId(office.Id);

            await base.Delete(office, isToSaveChanges);
        }

        public async Task DeleteByCompanyId(int companyId)
        {
            var offices = entities.Where(x => x.IsDeleted != true && x.CompanyId == companyId);

            foreach (var office in offices)
            {
                await Delete(office, false);
            }

            await _dbContext.SaveChangesAsync();
        }
    }
}
