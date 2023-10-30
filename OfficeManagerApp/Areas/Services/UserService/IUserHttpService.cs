using OfficeManager.Shared.Entities;
using OfficeManager.Shared.Request_Model;
using OfficeManagerApp.Areas.Services.Interfaces;

namespace OfficeManagerApp.Areas.Services.UserService
{
    public interface IUserHttpService : IGenericHttpService<User, UserRequestModel, User>
    {
        Task<User> GetByEmail(string email);
        Task<IDictionary<int, string>> GetUsersByIds(IList<int> usersIds);
    }
}
