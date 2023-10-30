using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components;
using OfficeManagerApp.Areas.Services.UserService;
using Microsoft.AspNetCore.Authorization;
using User = OfficeManager.Shared.Entities.User;

namespace OfficeManagerApp.Shared
{
    [Authorize]
    public partial class MainLayout : LayoutComponentBase
    {
        [CascadingParameter]
        private Task<AuthenticationState> AuthenticationStateTask { get; set; }

        [Inject]
        private IUserHttpService _userHttpService { get; set; }

        private User User { get; set; }
        private Loading LoadingComponent { set; get; }
        private bool IsUserDefined;


        protected override async void OnInitialized()
        {
            var authState = await AuthenticationStateTask;

            var user = authState.User.Identity;

            if (user.IsAuthenticated && !IsUserDefined)
            {
                IsUserDefined = true;

                User = await _userHttpService.GetByEmail(authState.User.Claims.ElementAt(7).Value.ToString());

                StateHasChanged();
            }
        }
    }
}
