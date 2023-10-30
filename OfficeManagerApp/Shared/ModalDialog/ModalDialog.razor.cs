using Microsoft.AspNetCore.Components;
using OfficeManagerApp.Areas.Services.Interfaces;

namespace OfficeManagerApp.Shared.ModalDialog
{
    public partial class ModalDialog : ComponentBase, IDisposable
    {
        [Inject]
        private IModalDialogService ModalDialogService { get; set; }

        private bool _isModalOpen;
        private EventCallback _onOk { get; set; }
        private string _title;
        private ModalDialogEnum _modalType;
        private string _content;

        protected override void OnInitialized()
        {
            ModalDialogService.OnOpenModalDialog += OpenModalDialog;
        }

        public void Dispose()
        {
            ModalDialogService.OnOpenModalDialog -= OpenModalDialog;
        }

        private void OpenModalDialog(EventCallback onOk, string title, ModalDialogEnum modalType, string content)
        {
            _onOk = onOk;

            _title = title;

            _modalType = modalType;

            _content = content;

            _isModalOpen = true;

            StateHasChanged();
        }

        private void ModalCancel()
        {
            _isModalOpen = false;

            StateHasChanged();
        }

        private async Task ModalOk()
        {
            await _onOk.InvokeAsync(true);

            _isModalOpen = false;
        }
    }
}
