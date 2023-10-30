using CompaniesServiceApi.BusinessLogic.Interfaces;
using CompaniesServiceApi.Data.Services.Interfaces;
using OfficeManager.Shared.Entities;
using OfficeManager.Shared.Exceptions;

namespace CompaniesServiceApi.BusinessLogic.Implementations
{
    public class LocationBusiness : GenericBusiness<ILocationService, LocationModel>, ILocationBusiness
    {

        public LocationBusiness(ILocationService service, ILogger<LocationBusiness> logger) : base(service, logger)
        {
        }

        public override async Task<bool> Validate(LocationModel location)
        {
            if (await Service.CheckLocationExist(location.Id, location.Country, location.City))
            {
                throw new EntityDuplicateException("location");
            }
            return true;
        }
    }
}
