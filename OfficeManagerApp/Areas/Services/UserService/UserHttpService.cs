using OfficeManager.Shared.Entities;
using OfficeManager.Shared.Request_Model;
using OfficeManagerApp.Data.Constants;
using OfficeManagerApp.Areas.Services.Implementations;
using System.Net.Http.Json;

namespace OfficeManagerApp.Areas.Services.UserService
{
    public class UserHttpService : GenericHttpService<User, UserRequestModel, User>, IUserHttpService
    {
        public override string Url { get => @AppConst.ApiUrlConst.UserApi.UserUrl; set => base.Url = value; }

        public UserHttpService(HttpClient httpClient) : base(httpClient)
        {
        }

        public async Task<User> GetByEmail(string email)
        {
            if (email == null)
            {
                throw new ArgumentNullException("entity");
            }

            var entity = await _httpClient.GetFromJsonAsync<User>(Url + "/Email/" + email);

            return entity;
        }

        public async Task<IDictionary<int, string>> GetUsersByIds(IList<int> usersIds)
        {
            string userIdsString = string.Join(",", usersIds);

            if (string.IsNullOrEmpty(userIdsString))
            {
                return null;
            }

            return await _httpClient.GetFromJsonAsync<IDictionary<int, string>>(Url + "/Ids/" + userIdsString);
        }
    }
}
