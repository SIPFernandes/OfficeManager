using OfficeManager.Shared.Entities;

namespace UsersServiceApi.Data.Services.Interfaces
{
    public interface IUserService : IGenericService<User>
    {
        Task<bool> CheckEmailExist(string email, int id);
        Task<IDictionary<int, string>> GetUsersByIds(IList<int> usersId);

        Task<User?> GetByEmail(string email);
    }
}
