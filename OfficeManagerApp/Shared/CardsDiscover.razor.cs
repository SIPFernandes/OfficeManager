using Microsoft.AspNetCore.Components;
using OfficeManagerApp.Data.Models;

namespace OfficeManagerApp.Shared
{
    public partial class CardsDiscover : ComponentBase
    {
        [Parameter]
        public IList<DiscoverCardModel> DiscoverDataList { get; set; }

        [Parameter]
        public string UrlGoTo { get; set; }

        [Inject]
        private NavigationManager _navigationManager { get; set; }

        private void GoTo(int id)
        {
            _navigationManager.NavigateTo(string.Format(UrlGoTo, id));
        }
    }
}
