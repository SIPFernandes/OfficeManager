using Microsoft.AspNetCore.Components;
using OfficeManager.Shared.Entities;
using OfficeManagerApp.Areas.Services.Interfaces;

namespace OfficeManagerApp.Shared
{
    public partial class Header : ComponentBase
    {
        [Parameter]
        public string Title { get; set; } = string.Empty;
        
        [Parameter]
        public string UrlPage { get; set; } = string.Empty;

        [Parameter]
        public bool AddLocationModalDialog { get; set; } = false;

        [Parameter]
        public EventCallback<LocationModel> OnLocationAdded { get; set; }

        [Inject]
        private IPageHistoryState PageHistoryState { get; set; }

        public static bool AddLocationModalShow { get; set; }

        private void GoBack()
        {
            PageHistoryState.NavigateBack();
        }

        protected async Task AddLocation(LocationModel location)
        {
            OnDialogClose();

            await OnLocationAdded.InvokeAsync(location);
        }

        //Modal
        private void OnDialogClose()
        {
            AddLocationModalShow = false;

            StateHasChanged();
        }

        private void OpenAddLocationDialog()
        {
            AddLocationModalShow = true;

            StateHasChanged();
        }
    }
}
