using Microsoft.AspNetCore.Components;

namespace OfficeManagerApp.Shared
{
    public partial class Searchbar : ComponentBase
    {
        [Parameter]
        public EventCallback<string> SearchbarChanged { get; set; }

        [Parameter]
        public bool IsFlow { get; set; }

        private string officeSearchInput;

        protected void KeyPressed(ChangeEventArgs e)
        {
            officeSearchInput = e.Value.ToString();

            SearchbarChanged.InvokeAsync(officeSearchInput);
        }
    }
}
