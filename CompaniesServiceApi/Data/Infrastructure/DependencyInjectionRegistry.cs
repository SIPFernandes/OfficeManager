using CompaniesServiceApi.Data.Services.Implementations;
using CompaniesServiceApi.Data.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CompaniesServiceApi.Data.Infrastructure
{
    public static class DependencyInjectionRegistry
    {
        public static IServiceCollection AddDataAccessDependencies(this IServiceCollection services, string dbConnectionString)
        {
            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(dbConnectionString));
            services.AddScoped<ICompanyService, CompanyService>();
            services.AddScoped<IOfficeService, OfficeService>();
            services.AddScoped<IRoomService, RoomService>();
            services.AddScoped<IRoomFacilityService, RoomFacilityService>();
            services.AddScoped<IFacilityService, FacilityService>();
            services.AddScoped<IReviewService, ReviewService>();
            services.AddScoped<ISeatService, SeatService>();
            services.AddScoped<ILocationService, LocationService>();
            services.AddScoped<IImageService, ImageService>();

            return services;
        }
    }
}
