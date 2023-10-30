using UsersServiceApi.Data.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using OfficeManager.Shared.Entities;
using OfficeManager.Shared.Exceptions;

namespace UsersServiceApi.Data.Services.Implementations
{
    public class UserService : GenericService<User>, IUserService
    {
        public UserService(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<bool> CheckEmailExist(string email, int id)
        {
            return await entities
                .AnyAsync(b => b.Email == email && b.Id != id && b.IsDeleted == false);
        }

        public virtual async Task<User?> GetByEmail(string email)
        {
            var entity = await entities.SingleAsync(b => b.Email == email && b.IsDeleted == false);

            if (entity != null && !entity.IsDeleted)
            {
                return entity;
            }
            else
            {
                throw new EntityDoesNotExistException();
            }

        }

        public async Task<IDictionary<int, string>> GetUsersByIds(IList<int> usersId)
        {
            var users = await entities.Where(x => x.IsDeleted != true &&usersId.Contains(x.Id)).ToListAsync();

            IDictionary<int, string> _userDictionary = new Dictionary<int, string>();

            if (users != null)
            {
                foreach (var user in users)
                {
                    var userName = user.Name.Split(' ');

                    var firstInitial = userName[0].Substring(0, 1).ToUpper();

                    if (userName.Length > 1) 
                    {
                        _userDictionary.Add(user.Id, firstInitial + userName[1].Substring(0, 1).ToUpper());
                    }
                    else
                    {
                        _userDictionary.Add(user.Id, firstInitial + firstInitial);
                    }
                    
                }
            }

            return _userDictionary;
        }
    }
}