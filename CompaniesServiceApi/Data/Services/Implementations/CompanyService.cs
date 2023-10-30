using CompaniesServiceApi.Data.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using OfficeManager.Shared.Entities;
using OfficeManager.Shared.Exceptions;
namespace CompaniesServiceApi.Data.Services.Implementations
{
    public class CompanyService : GenericService<Company>, ICompanyService
    {
        private readonly IOfficeService _officeService;

        public CompanyService(IOfficeService OfficeService, ApplicationDbContext dbContext) : base(dbContext)
        {
            _officeService = OfficeService;
        }

        public override async Task<IEnumerable<Company>> GetAll()
        {
            return await entities
                .Where(x => x.IsDeleted != true)
                .Include(x => x.Image)
                .ToListAsync();
        }

        public override async Task<Company?> Get(int id)
        {
            var company = await entities.Include(x => x.Image).SingleAsync(x => x.Id == id);

            if (company != null && !company.IsDeleted)
            {
                return company;
            }
            else
            {
                throw new EntityDoesNotExistException();
            }

        }

        public async Task<bool> CheckCompanyExist(string name, int id)
        {
            return await entities
                .AnyAsync(b => b.Name == name && b.Id != id && b.IsDeleted == false);
        }

        public override async Task Delete(Company company, bool isToSaveChanges)
        {
            await _officeService.DeleteByCompanyId(company.Id);

            await base.Delete(company, isToSaveChanges);
        }
    }
}
