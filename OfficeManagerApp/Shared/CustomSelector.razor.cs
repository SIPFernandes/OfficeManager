using Microsoft.AspNetCore.Components;
using OfficeManagerApp.Data.Constants.ConstsEN;
using System.ComponentModel.DataAnnotations;

namespace OfficeManagerApp.Shared
{
    public partial class CustomSelector : ComponentBase
    {
        [Parameter]
        public string CustomSelectorPlaceholder { get; set; }

        [Parameter]
        public EventCallback<string> CustomSelectorChanged { get; set; }

        [Parameter]
        public IList<string> CustomSelectorOptionsList { get; set; }

        [Parameter]
        public bool IsDatePicker { get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; }

        private bool isDropdownOpen = false;

        private string filterNameChanged = string.Empty;
        private DateTime? dateSelected = null;

        private void OpenCloseDropdown()
        {
            isDropdownOpen = !isDropdownOpen;
        }

        private void OptionSelected(string filter)
        {
            if(filterNameChanged != filter)
            {
                filterNameChanged = filter;

                CustomSelectorChanged.InvokeAsync(filterNameChanged);

                isDropdownOpen = false;

                StateHasChanged();
            }            
        }

        private void DateSelected(ChangeEventArgs args)
        {
            var dateString = args.Value.ToString();

            dateSelected = String.IsNullOrEmpty(dateString)
                ? null
                : DateTime.Parse(dateString);

            CustomSelectorChanged.InvokeAsync(dateSelected?.ToShortDateString());

            StateHasChanged();
        }
    }
}
