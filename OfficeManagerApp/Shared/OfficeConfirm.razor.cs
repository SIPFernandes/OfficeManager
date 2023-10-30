using Microsoft.AspNetCore.Components;
using OfficeManager.Shared.Entities;
using OfficeManager.Shared.Request_Model;

namespace OfficeManagerApp.Shared
{
    public partial class OfficeConfirm : ComponentBase
    {
        [Parameter]
        public OfficeRequestModel OfficeRequestModel { get; set; }
        [Parameter]
        public string CompanyName { get; set; }
        [Parameter]
        public EventCallback<bool> IsConfirmChanged { get; set; }        
        private string _locationName;

        protected override void OnInitialized()
        {            
            _locationName = OfficeRequestModel.Location.City +
                ", " + OfficeRequestModel.Location.Country;
        }

    }
}
