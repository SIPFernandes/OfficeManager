using Microsoft.AspNetCore.Components;
using OfficeManager.Shared.Entities;
using OfficeManagerApp.Areas.Helpers;
using OfficeManagerApp.Areas.Services.BookingService.SeatsAvailableService;
using OfficeManagerApp.Areas.Services.CompanyServices;
using OfficeManagerApp.Areas.Services.Interfaces;
using OfficeManagerApp.Data.Constants.ConstsEN;
using OfficeManagerApp.Shared;

namespace OfficeManagerApp.Pages.Rooms
{
    public partial class RoomsPage : ComponentBase
    {
        [CascadingParameter]
        public Loading LoadingComponent { get; set; }

        [Parameter]
        public int OfficeId { get; set; }

        [Parameter]
        public static string FilterSelected { get; set; }        

        [Inject]
        private ICompanyHttpService CompanyHttpService { get; set; }

        private string[] allSortBy = { @ConstEN.SortBy_AZ,
                                       @ConstEN.SortBy_ZA };

        private Office office = new Office();

        private IList<string> allRoomTypes;

        private IList<Room> allRooms;

        private IList<Room> filteredRoomsList;

        private string chosenFilter = string.Empty;

        private string chosenSort = string.Empty;

        private string searchExpression = string.Empty;

        protected override async Task OnInitializedAsync()
        {
            LoadingComponent.OpenLoadingComponent();

            office = await CompanyHttpService.OfficeHttpService.Get(OfficeId);

            var location = await CompanyHttpService.OfficeHttpService
                .LocationHttpService.Get(office.LocationId);

            office.Location = location;

            allRooms = await CompanyHttpService.OfficeHttpService
                    .RoomHttpService.GetRoomsByOfficeId(office.Id);

            filteredRoomsList = allRooms;

            allRoomTypes = allRooms.Select(x => x.Type).Distinct().ToList();

            LoadingComponent.CloseLoadingComponent();
        }

        private void OnFilterChanged(string filter)
        {
            chosenFilter = filter;

            CreateFilter();
        }

        private void OnSortChanged(string sort)
        {
            FilterSelected = sort;

            chosenSort = sort;

            if (FilterSelected == @ConstEN.SortBy_AZ)
            {
                filteredRoomsList = filteredRoomsList.OrderBy(x => x.Name).ToList();
            }
            else if (FilterSelected == @ConstEN.SortBy_ZA)
            {
                filteredRoomsList = filteredRoomsList.OrderByDescending(x => x.Name).ToList();
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
            Func<Room, bool> tempFilter = null;

            if (!string.IsNullOrEmpty(searchExpression))
            {
                FilterBaseHelper.AddFilter(ref tempFilter, SearchFunc(searchExpression));
            }
            if (!string.IsNullOrEmpty(chosenFilter) && !chosenFilter.Equals(ConstEN.All))
            {
                FilterBaseHelper.AddFilter(ref tempFilter, FilterFunc(chosenFilter));
            }

            var filterPredicate = tempFilter != null ? new Predicate<Room>(tempFilter) : null;

            if (filterPredicate != null)
            {
                filteredRoomsList = allRooms.Where(x => filterPredicate(x)).ToList();
            }
            else
            {
                filteredRoomsList = allRooms.ToList();
            }

            if (chosenSort != string.Empty)
            {
                OnSortChanged(chosenSort);
            }
        }

        public static Func<Room, bool> SearchFunc(string searchExpression)
        {
            return x => x.Type.ToLower().Contains(searchExpression.ToLower())
            || x.Name.ToLower().Contains(searchExpression.ToLower());
        }

        public static Func<Room, bool> FilterFunc(string chosenFilter)
        {
            return x => x.Type.ToLower().Contains(chosenFilter.ToLower());
        }
    }
}
