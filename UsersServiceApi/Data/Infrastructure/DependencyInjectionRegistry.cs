using UsersServiceApi.Data.Services.Implementations;
using UsersServiceApi.Data.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace UsersServiceApi.Data.Infrastructure
{
    public static class DependencyInjectionRegistry
    {
        public static IServiceCollection AddDataAccessDependencies(this IServiceCollection services, string dbConnectionString)
        {
            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(dbConnectionString));
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IRoleService, RoleService>();

            return services;
        }
    }
}
