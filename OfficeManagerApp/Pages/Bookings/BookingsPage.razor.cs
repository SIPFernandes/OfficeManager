using Microsoft.AspNetCore.Components;
using OfficeManager.Shared.Entities;
using OfficeManagerApp.Areas.Helpers;
using OfficeManagerApp.Areas.Services.BookingService;
using OfficeManagerApp.Data.Constants.ConstsEN;
using OfficeManagerApp.Shared;

namespace OfficeManagerApp.Pages.Bookings
{
    public partial class BookingsPage : ComponentBase
    {
        [CascadingParameter]
        public Loading LoadingComponent { get; set; }

        [Parameter]
        public static string FilterSelected { get; set; }

        [Inject]
        private IBookingHttpService BookingHttpService { get; set; }

        private IList<Booking> allBookings;

        private IList<Booking> filteredBookingsList;

        private string[] allSortBy = { @ConstEN.SortBy_DateAscending,
                                       @ConstEN.SortBy_DateDescending };

        private string chosenDate = string.Empty;

        private string chosenSort = string.Empty;

        private string searchExpression = string.Empty;

        protected override async Task OnInitializedAsync()
        {
            LoadingComponent.OpenLoadingComponent();

            allBookings = await BookingHttpService.GetAll();

            filteredBookingsList = allBookings;

            LoadingComponent.CloseLoadingComponent();
        }

        private void OnDateChanged(string date)
        {
            LoadingComponent.OpenLoadingComponent();

            chosenDate = date;

            CreateFilter();

            LoadingComponent.CloseLoadingComponent();
        }

        private void OnSortChanged(string sort)
        {
            FilterSelected = sort;

            chosenSort = sort;

            if (FilterSelected == @ConstEN.SortBy_DateAscending)
            {
                filteredBookingsList = filteredBookingsList.OrderBy(x => x.Date).ToList();
            }
            else if (FilterSelected == @ConstEN.SortBy_DateDescending)
            {
                filteredBookingsList = filteredBookingsList.OrderByDescending(x => x.Date).ToList();
            }

            StateHasChanged();
        }

        private void OnSearchbarChanged(string searchbarInput)
        {
            searchExpression = searchbarInput;

            CreateFilter();
        }

        private void CreateFilter()
        {
            Func<Booking, bool> tempFilter = null;

            if (!string.IsNullOrEmpty(searchExpression))
            {
                FilterBaseHelper.AddFilter(ref tempFilter, SearchFunc(searchExpression));
            }
            if (!string.IsNullOrEmpty(chosenDate) && !chosenDate.Equals(ConstEN.All))
            {
                FilterBaseHelper.AddFilter(ref tempFilter, FilterByDate(chosenDate));
            }

            var filterPredicate = tempFilter != null ? new Predicate<Booking>(tempFilter) : null;

            if (filterPredicate != null)
            {
                filteredBookingsList = allBookings.Where(x => filterPredicate(x)).ToList();
            }
            else
            {
                filteredBookingsList = allBookings.ToList();
            }

            if (chosenSort != string.Empty)
            {
                OnSortChanged(chosenSort);
            }
        }

        public static Func<Booking, bool> SearchFunc(string searchExpression) /*TODO: sort by user name*/
        {
            //return x => x.Name.ToLower().Contains(searchExpression.ToLower());

            //by user name
            return null;
        }

        public static Func<Booking, bool> FilterByDate(string chosenDate)
        {
            return x => x.Date.Date.ToString().Contains(chosenDate.ToLower());
        }
    }
}