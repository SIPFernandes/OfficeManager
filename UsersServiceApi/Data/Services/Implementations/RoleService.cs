using OfficeManager.Shared.Entities;
using Microsoft.EntityFrameworkCore;
using UsersServiceApi.Data.Services.Interfaces;

namespace UsersServiceApi.Data.Services.Implementations
{
    public class RoleService : GenericService<Role>, IRoleService
    {
        public RoleService(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<bool> CheckRoleExist(string name, int id)
        {
            return await entities
                .AnyAsync(b => b.Name == name && b.Id != id);
        }
    }
}
