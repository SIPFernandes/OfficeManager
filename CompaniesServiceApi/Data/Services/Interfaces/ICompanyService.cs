using OfficeManager.Shared.Entities;

namespace CompaniesServiceApi.Data.Services.Interfaces
{
    public interface ICompanyService : IGenericService<Company>
    {
        Task<bool> CheckCompanyExist(string name, int id);
    }
}
