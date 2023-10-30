using Microsoft.AspNetCore.Components;
using OfficeManager.Shared.Entities;

namespace OfficeManagerApp.Shared
{
    public partial class CardsOffice : ComponentBase
    {
        [Parameter]
        public IList<Office> OfficesList { get; set; }
    }
}
