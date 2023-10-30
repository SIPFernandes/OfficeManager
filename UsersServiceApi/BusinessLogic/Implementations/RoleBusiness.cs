using OfficeManager.Shared.Entities;
using OfficeManager.Shared.Exceptions;
using UsersServiceApi.BusinessLogic.Interfaces;
using UsersServiceApi.Data.Services.Interfaces;

namespace UsersServiceApi.BusinessLogic.Implementations
{
    public class RoleBusiness : GenericBusiness<IRoleService, Role>, IRoleBusiness
    {
        public RoleBusiness(IRoleService service, ILogger<RoleBusiness> logger) : base(service, logger)
        {
        }

        public override async Task<bool> Validate(Role role)
        {
            if (await Service.CheckRoleExist(role.Name, role.Id))
            {
                throw new EntityDuplicateException("role");
            }
            return true;
        }
    }
}
