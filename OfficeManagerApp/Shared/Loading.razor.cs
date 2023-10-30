using Microsoft.AspNetCore.Components;

namespace OfficeManagerApp.Shared
{
    public partial class Loading : ComponentBase
    {
        private bool _isLoaded = true;

        public void OpenLoadingComponent()
        {
            _isLoaded = false;

            StateHasChanged();
        }

        public void CloseLoadingComponent()
        {
            _isLoaded = true;

            StateHasChanged();
        }
    }
}
