using CompaniesServiceApi.BusinessLogic.Interfaces;
using CompaniesServiceApi.Data.Services.Interfaces;
using OfficeManager.Shared.Entities;
using OfficeManager.Shared.Exceptions;

namespace CompaniesServiceApi.BusinessLogic.Implementations
{
    public class OfficeBusiness : GenericBusiness<IOfficeService, Office>, IOfficeBusiness
    {
        public OfficeBusiness(IOfficeService service, ILogger<OfficeBusiness> logger) : base(service, logger)
        {
        }

        public async Task<IEnumerable<Office>> GetOfficesByCompanyId(int companyId)
        {
            return await Service.GetOfficesByCompanyId(companyId);
        }

        public override async Task<bool> Validate(Office office)
        {
            if (await Service.CheckOfficeExist(office.Id, office.Name, office.CompanyId))
            {
                throw new EntityDuplicateException("office in this company");
            }
            return true;
        }
    }
}
