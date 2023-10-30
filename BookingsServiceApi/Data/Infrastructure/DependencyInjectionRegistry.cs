using Microsoft.EntityFrameworkCore;
using BookingsServiceApi.Data.Services.Implementations;
using BookingsServiceApi.Data.Services.Interfaces;
using BookingsServiceApi.Areas.Services.Interfaces;
using BookingsServiceApi.Areas.Services.Implementations;

namespace BookingsServiceApi.Data.Infrastructure
{
    public static class DependencyInjectionRegistry
    {
        public static IServiceCollection AddDataAccessDependencies(this IServiceCollection services, string dbConnectionString)
        {
            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(dbConnectionString));
            services.AddScoped<IBookingService, BookingService>();
            services.AddScoped<ISeatsAvailableService, SeatsAvailableService>();

            return services;
        }
    }
}
