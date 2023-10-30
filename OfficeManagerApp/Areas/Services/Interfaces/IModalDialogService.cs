using Microsoft.AspNetCore.Components;
using OfficeManagerApp.Shared.ModalDialog;

namespace OfficeManagerApp.Areas.Services.Interfaces
{
    public interface IModalDialogService
    {
        public event Action<EventCallback, string, ModalDialogEnum, string> OnOpenModalDialog;

        public void OpenModalDialog(EventCallback onOk, string title, ModalDialogEnum modalType, string content);
    }
}
