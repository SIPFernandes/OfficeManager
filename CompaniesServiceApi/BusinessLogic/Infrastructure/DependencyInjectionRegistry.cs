using CompaniesServiceApi.BusinessLogic.Implementations;
using CompaniesServiceApi.BusinessLogic.Interfaces;

namespace CompaniesServiceApi.BusinessLogic.Infrastructure
{
    public static class DependencyInjectionRegistry
    {
        public static IServiceCollection AddBusinessLogicDependencies(this IServiceCollection businesses)
        {
            businesses.AddScoped<ICompanyBusiness, CompanyBusiness>();
            businesses.AddScoped<IOfficeBusiness, OfficeBusiness>();
            businesses.AddScoped<IRoomBusiness, RoomBusiness>();
            businesses.AddScoped<IRoomFacilityBusiness, RoomFacilityBusiness>();
            businesses.AddScoped<IFacilityBusiness, FacilityBusiness>();
            businesses.AddScoped<IReviewBusiness, ReviewBusiness>();
            businesses.AddScoped<ISeatBusiness, SeatBusiness>();
            businesses.AddScoped<ILocationBusiness, LocationBusiness>();
            businesses.AddScoped<IImageBusiness, ImageBusiness>();

            return businesses;
        }
    }
}
