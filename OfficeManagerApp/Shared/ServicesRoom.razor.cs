using OfficeManager.Shared.Entities;
using Microsoft.AspNetCore.Components;

namespace OfficeManagerApp.Shared
{
    public partial class ServicesRoom : ComponentBase
    {
        [Parameter]
        public IList<Facility> RoomFacilities { get; set; }
    }
}
