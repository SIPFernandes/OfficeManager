using Microsoft.AspNetCore.Components;

namespace OfficeManagerApp.Areas.Services.Interfaces
{
    public interface IJSInteropService
    {
        public Task SetSlide(string elementId);

        public Task HideRevealButton(ElementReference buttonReference);

        public Task OpenCloseDropdown(string elementId, bool isOpen);
    }
}
