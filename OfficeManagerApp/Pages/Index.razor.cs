using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using OfficeManagerApp.Areas.Services.Implementations;
using OfficeManagerApp.Data.Constants;

namespace OfficeManagerApp.Pages
{
    public partial class Index : ComponentBase
    {
        [Inject]
        AuthenticationStateProvider AuthenticationStateProvider { get; set; }

        [Inject]
        NavigationManager NavigationManager { get; set; }

        public async Task Login()
        {
            await ((ExternalAuthStateProvider)AuthenticationStateProvider)
                .LogInAsync();

            NavigationManager.NavigateTo(AppConst.OtherPagesUrlConst.HomePage);
        }
    }
}
