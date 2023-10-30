using CompaniesServiceApi.BusinessLogic.Interfaces;
using CompaniesServiceApi.Data.Services.Interfaces;
using OfficeManager.Shared.Entities;
using OfficeManager.Shared.Exceptions;

namespace CompaniesServiceApi.BusinessLogic.Implementations
{
    public class FacilityBusiness : GenericBusiness<IFacilityService, Facility>, IFacilityBusiness
    {
        public FacilityBusiness(IFacilityService service, ILogger<FacilityBusiness> logger) : base(service, logger)
        {
        }

        public override async Task<bool> Validate(Facility facility)
        {
            if (await Service.CheckFacilityExist(facility.Name, facility.Id))
            {
                throw new EntityDuplicateException("facility");
            }
            return true;
        }
    }
}
