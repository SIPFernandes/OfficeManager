using OfficeManager.Shared.Entities;

namespace CompaniesServiceApi.BusinessLogic.Interfaces
{
    public interface IOfficeBusiness : IGenericBusiness<Office>
    {
        Task<IEnumerable<Office>> GetOfficesByCompanyId(int companyId);
    }
}
