using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using OfficeManagerApp.Areas.Services.Implementations;

namespace OfficeManagerApp.Shared
{
    public partial class Menu : ComponentBase
    {
        [Inject]
        private AuthenticationStateProvider AuthenticationStateProvider { get; set; }

        private void Logout()
        {
            ((ExternalAuthStateProvider)AuthenticationStateProvider)
            .Logout();
        }
    }
}
