using Microsoft.AspNetCore.Components;
using OfficeManagerApp.Areas.Services.Interfaces;
using OfficeManagerApp.Shared.ModalDialog;

namespace OfficeManagerApp.Areas.Services.Implementations
{
    public class ModalDialogService : IModalDialogService
    {
        public event Action<EventCallback, string, ModalDialogEnum, string> OnOpenModalDialog;

        public void OpenModalDialog(EventCallback onOk, string title, ModalDialogEnum modalType, string content) 
        { 
            OnOpenModalDialog.Invoke(onOk, title, modalType, content);
        }
    }
}