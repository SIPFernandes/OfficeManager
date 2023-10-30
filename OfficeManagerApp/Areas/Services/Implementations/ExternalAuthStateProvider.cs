using Microsoft.AspNetCore.Components.Authorization;
using OfficeManager.Shared.Request_Model;
using OfficeManagerApp.Areas.Services.Interfaces;
using OfficeManagerApp.Areas.Services.UserService;
using System.Security.Claims;
using System.Text.Json;

namespace OfficeManagerApp.Areas.Services.Implementations
{
    internal class ExternalAuthStateProvider : AuthenticationStateProvider
    {
        public string AccessToken { get => _accessToken; }
        private string _accessToken { get; set; }
        private readonly IPlatformService _platformService;
        private ClaimsPrincipal currentUser = new(new ClaimsIdentity());
        private readonly IUserHttpService _userHttpService;

        public ExternalAuthStateProvider(IPlatformService platformService, IUserHttpService userHttpService)
        {
            _platformService = platformService;

            _userHttpService = userHttpService;
        }

        public override Task<AuthenticationState> GetAuthenticationStateAsync() =>
            Task.FromResult(new AuthenticationState(currentUser));

        public Task LogInAsync()
        {
            var loginTask = LogInAsyncCore();            
            NotifyAuthenticationStateChanged(loginTask);

            return loginTask;

            async Task<AuthenticationState> LogInAsyncCore()
            {
                var user = await LoginWithExternalProviderAsync();
                currentUser = user;

                UserRequestModel userRequestModel = new UserRequestModel()
                {
                    Name = currentUser.Claims.ElementAt(5).Value.ToString(),
                    Email = currentUser.Claims.ElementAt(7).Value.ToString(),
                    PhoneNumber = "912345678",
                    Position = "Full Stack",
                    RoleId = 1,
                    CompanyId = 1,
                    OfficeId = 1,
                    Image = "some image"
                };

                await _userHttpService.Insert(userRequestModel);

                return new AuthenticationState(currentUser);
            }
        }

        private async Task<ClaimsPrincipal> LoginWithExternalProviderAsync()
        {
            var authenticationResult = await _platformService.GetAuthenticationResult();

            var authenticatedUser = new ClaimsPrincipal(new ClaimsIdentity(
                authenticationResult.ClaimsPrincipal.Claims, "Basic"));

            _accessToken = authenticationResult.AccessToken;

            var claims = new List<Claim>
            {
                new Claim("Token", _accessToken)
            };

            authenticatedUser.AddIdentity(new ClaimsIdentity(claims));
            //Extra Claims
            //var authenticatedUser = new ClaimsPrincipal(new ClaimsIdentity(ParseClaimsFromJwt(authenticationResult.AccessToken), "jwt"));        
            //authenticatedUser.AddIdentity(new ClaimsIdentity(ParseClaimsFromJwt(authenticationResult.AccessToken), "jwt"));

            return await Task.FromResult(authenticatedUser);            
        }

        public void Logout()
        {
            currentUser = new ClaimsPrincipal(new ClaimsIdentity());
            NotifyAuthenticationStateChanged(
                Task.FromResult(new AuthenticationState(currentUser)));
        }

        private IEnumerable<Claim> ParseClaimsFromJwt(string jwt)
        {
            var claims = new List<Claim>();
            var payload = jwt.Split('.')[1];
            var jsonBytes = ParseBase64WithoutPadding(payload);
            var keyValuePairs = JsonSerializer.Deserialize<Dictionary<string, object>>(jsonBytes);

            keyValuePairs.TryGetValue(ClaimTypes.Role, out object roles);

            if (roles != null)
            {
                if (roles.ToString().Trim().StartsWith("["))
                {
                    var parsedRoles = JsonSerializer.Deserialize<string[]>(roles.ToString());

                    foreach (var parsedRole in parsedRoles)
                    {
                        claims.Add(new Claim(ClaimTypes.Role, parsedRole));
                    }
                }
                else
                {
                    claims.Add(new Claim(ClaimTypes.Role, roles.ToString()));
                }

                keyValuePairs.Remove(ClaimTypes.Role);
            }

            claims.AddRange(keyValuePairs.Select(kvp => new Claim(kvp.Key, kvp.Value.ToString())));

            return claims;
        }

        private byte[] ParseBase64WithoutPadding(string base64)
        {
            switch (base64.Length % 4)
            {
                case 2: base64 += "=="; break;
                case 3: base64 += "="; break;
            }
            return Convert.FromBase64String(base64);
        }
    }
}
