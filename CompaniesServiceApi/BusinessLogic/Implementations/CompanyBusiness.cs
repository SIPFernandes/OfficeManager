using CompaniesServiceApi.BusinessLogic.Interfaces;
using CompaniesServiceApi.Data.Services.Interfaces;
using OfficeManager.Shared.Entities;
using OfficeManager.Shared.Exceptions;

namespace CompaniesServiceApi.BusinessLogic.Implementations
{
    public class CompanyBusiness : GenericBusiness<ICompanyService, Company>, ICompanyBusiness
    {
        public CompanyBusiness(ICompanyService service, ILogger<CompanyBusiness> logger) : base(service, logger)
        {
        }

        public override async Task<bool> Validate(Company company)
        {
            if (await Service.CheckCompanyExist(company.Name, company.Id))
            {
                throw new EntityDuplicateException("company");
            }
            return true;
        }
    }
}
