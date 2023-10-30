using OfficeManager.Shared.Entities;

namespace UsersServiceApi.BusinessLogic.Interfaces
{
    public interface IUserBusiness : IGenericBusiness<User>
    {
        Task<IDictionary<int, string>> GetUsersByIds(IList<int> usersId);
        Task<User?> GetByEmail(string email);
    }
}
