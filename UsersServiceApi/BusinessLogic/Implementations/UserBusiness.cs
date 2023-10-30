using OfficeManager.Shared.Entities;
using OfficeManager.Shared.Exceptions;
using UsersServiceApi.BusinessLogic.Interfaces;
using UsersServiceApi.Data.Services.Interfaces;

namespace UsersServiceApi.BusinessLogic.Implementations
{
    public class UserBusiness : GenericBusiness<IUserService, User>, IUserBusiness
    {
        public UserBusiness(IUserService service, ILogger<UserBusiness> logger) : base(service, logger)
        {
        }

        public override async Task<bool> Validate(User user)
        {
            if (await Service.CheckEmailExist(user.Email, user.Id))
            {
                throw new EntityDuplicateException("user");
            }
            return true;
        }

        public virtual async Task<User?> GetByEmail(string email)
        {
            return await Service.GetByEmail(email);
        }

        public async Task<IDictionary<int, string>> GetUsersByIds(IList<int> usersId)
        {
            return await Service.GetUsersByIds(usersId);
        }
    }
}
