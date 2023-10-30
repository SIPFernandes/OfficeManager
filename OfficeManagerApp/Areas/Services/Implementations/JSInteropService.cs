using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using OfficeManagerApp.Areas.Services.Interfaces;

namespace OfficeManagerApp.Areas.Services.Implementations
{
    public class JSInteropService : IJSInteropService
    {
        private IJSRuntime _jSRuntime;

        public JSInteropService(IJSRuntime JSRuntime)
        {
            _jSRuntime = JSRuntime;
        }

        public async Task SetSlide(string elementId)
        {
            await _jSRuntime.InvokeVoidAsync("sliderImage", elementId);
        }

        public async Task HideRevealButton(ElementReference buttonReference)
        {
            await _jSRuntime.InvokeVoidAsync("hideRevealButton", buttonReference);
        }

        public async Task OpenCloseDropdown(string elementId, bool isOpen)
        {
            await _jSRuntime.InvokeVoidAsync("openCloseDropdown", elementId, isOpen);
        }
    }
}
