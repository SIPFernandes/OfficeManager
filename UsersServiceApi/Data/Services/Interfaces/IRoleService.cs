using OfficeManager.Shared.Entities;

namespace UsersServiceApi.Data.Services.Interfaces
{
    public interface IRoleService : IGenericService<Role>
    {
        Task<bool> CheckRoleExist(string name, int id);
    }
}
