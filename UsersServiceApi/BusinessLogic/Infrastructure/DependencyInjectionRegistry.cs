using UsersServiceApi.BusinessLogic.Implementations;
using UsersServiceApi.BusinessLogic.Interfaces;

namespace UsersServiceApi.BusinessLogic.Infrastructure
{
    public static class DependencyInjectionRegistry
    {
        public static IServiceCollection AddBusinessLogicDependencies(this IServiceCollection businesses)
        {
            businesses.AddScoped<IUserBusiness, UserBusiness>();
            businesses.AddScoped<IRoleBusiness, RoleBusiness>();

            return businesses;
        }
    }
}
