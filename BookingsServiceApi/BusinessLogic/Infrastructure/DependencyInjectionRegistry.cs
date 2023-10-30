using BookingsServiceApi.BusinessLogic.Implementations;
using BookingsServiceApi.BusinessLogic.Interfaces;

namespace BookingsServiceApi.BusinessLogic.Infrastructure
{
    public static class DependencyInjectionRegistry
    {
        public static IServiceCollection AddBusinessLogicDependencies(this IServiceCollection businesses)
        {
            businesses.AddScoped<IBookingBusiness, BookingBusiness>();
            businesses.AddScoped<ISeatsAvailableBusiness, SeatsAvailableBusiness>();

            return businesses;
        }
    }
}
