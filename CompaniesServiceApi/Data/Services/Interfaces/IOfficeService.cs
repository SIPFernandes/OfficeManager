using OfficeManager.Shared.Entities;

namespace CompaniesServiceApi.Data.Services.Interfaces
{
    public interface IOfficeService : IGenericService<Office>
    {
        Task<IEnumerable<Office>> GetOfficesByCompanyId(int companyId);

        Task<bool> CheckOfficeExist(int id, string name, int companyId);

        Task DeleteByCompanyId(int companyId);
    }
}
