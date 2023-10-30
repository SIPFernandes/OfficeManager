using Microsoft.Identity.Client;

namespace OfficeManagerApp.Areas.Services.Interfaces
{
    internal interface IPlatformService
    {
        public IPublicClientApplication IdentityClient { get; set; }
        public Task<AuthenticationResult> GetAuthenticationResult();
    }
}
