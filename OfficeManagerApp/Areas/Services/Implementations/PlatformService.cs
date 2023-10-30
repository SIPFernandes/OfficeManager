using Microsoft.Identity.Client;
using OfficeManagerApp.Areas.Services.Interfaces;
using OfficeManagerApp.Data.Constants;
using System.Diagnostics;

namespace OfficeManagerApp.Areas.Services.Implementations
{
    internal class PlatformService : IPlatformService
    {
        public IPublicClientApplication IdentityClient { get; set; }

        public async Task<AuthenticationResult> GetAuthenticationResult()
        {
            if (IdentityClient == null)
            {
                object parentWindow = null;
#if ANDROID
                parentWindow = Platform.CurrentActivity;
#endif
                IdentityClient = GetIdentityClient(parentWindow);
            }

            var accounts = await IdentityClient.GetAccountsAsync();
            AuthenticationResult result = null;
            bool tryInteractiveLogin = false;

            try
            {
                result = await IdentityClient
                    .AcquireTokenSilent(AppConst.Identity.CompanyApiScopes, accounts.FirstOrDefault())
                    .ExecuteAsync();
            }
            catch (MsalUiRequiredException)
            {
                tryInteractiveLogin = true;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"MSAL Silent Error: {ex.Message}");
            }

            if (tryInteractiveLogin)
            {
                try
                {                    
                    result = await IdentityClient
                        .AcquireTokenInteractive(AppConst.Identity.CompanyApiScopes)
                        .WithAccount(accounts.FirstOrDefault())
                        .WithExtraScopesToConsent(AppConst.Identity.BookingApiScopes) //Use this or build gateway API with and expose only that API
                        .ExecuteAsync()
                        .ConfigureAwait(false);
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"MSAL Interactive Error: {ex.Message}");
                }
            }
            
            return result;
        }
        
        private static IPublicClientApplication GetIdentityClient(object parentWindow)
        {
            var clientBuilder = PublicClientApplicationBuilder
                .Create(AppConst.Identity.ApplicationId)
                .WithAuthority(AzureCloudInstance.AzurePublic, AppConst.Identity.TenantId);

#if ANDROID
            clientBuilder = clientBuilder
                .WithRedirectUri($"msal{AppConst.Identity.ApplicationId}://auth")
                .WithParentActivityOrWindow(() => parentWindow);
#endif

#if WINDOWS
            clientBuilder = clientBuilder
                .WithRedirectUri("https://login.microsoftonline.com/common/oauth2/nativeclient");
#endif

            return clientBuilder.Build();
        }
    }
}