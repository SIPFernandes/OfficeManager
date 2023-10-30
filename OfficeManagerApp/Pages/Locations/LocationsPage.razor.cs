using Microsoft.AspNetCore.Components;
using OfficeManagerApp.Areas.Helpers;
using OfficeManagerApp.Areas.Services.CompanyServices;
using OfficeManagerApp.Data.Constants.ConstsEN;
using OfficeManagerApp.Shared;
using LocationModel = OfficeManager.Shared.Entities.LocationModel;

namespace OfficeManagerApp.Pages.Locations
{
    public partial class LocationsPage : ComponentBase
    {
        [CascadingParameter]
        public Loading LoadingComponent { get; set; }

        [Parameter]
        public static string FilterSelected { get; set; }

        [Inject]
        private ICompanyHttpService CompanyHttpService { get; set; }

        private IList<LocationModel> allLocations;     

        private IList<string> allCountries;

        private IList<LocationModel> filteredLocationsList;

        private string[] allSortBy = { @ConstEN.SortBy_AZ,
                                       @ConstEN.SortBy_ZA };

        private string chosenFilter = string.Empty;

        private string chosenSort = string.Empty;

        private string searchExpression = string.Empty;

        protected override async Task OnInitializedAsync()
        {
            LoadingComponent.OpenLoadingComponent();

            allLocations = await CompanyHttpService.OfficeHttpService
                .LocationHttpService.GetAll();

            filteredLocationsList = allLocations;

            allCountries = allLocations.Select(x => x.Country).Distinct().ToList();

            LoadingComponent.CloseLoadingComponent();
        }

        //FILTER
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
                filteredLocationsList = filteredLocationsList.OrderBy(x => x.City).ToList();
            }
            else if (FilterSelected == @ConstEN.SortBy_ZA)
            {
                filteredLocationsList = filteredLocationsList.OrderByDescending(x => x.City).ToList();
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
            Func<LocationModel, bool> tempFilter = null;

            if (!string.IsNullOrEmpty(searchExpression))
            {
                FilterBaseHelper.AddFilter(ref tempFilter, SearchFunc(searchExpression));
            }
            if (!string.IsNullOrEmpty(chosenFilter) && !chosenFilter.Equals(ConstEN.All))
            {
                FilterBaseHelper.AddFilter(ref tempFilter, FilterFunc(chosenFilter));
            }

            var filterPredicate = tempFilter != null ? new Predicate<LocationModel>(tempFilter) : null;

            if (filterPredicate != null)
            {
                filteredLocationsList = allLocations.Where(x => filterPredicate(x)).ToList();
            }
            else
            {
                filteredLocationsList = allLocations.ToList();
            }

            if (chosenSort != string.Empty)
            {
                OnSortChanged(chosenSort);
            }
        }

        public static Func<LocationModel, bool> SearchFunc(string searchExpression)
        {
            return x => x.City.ToLower().Contains(searchExpression.ToLower())
            || x.Country.ToLower().Contains(searchExpression.ToLower());
        }

        public static Func<LocationModel, bool> FilterFunc(string chosenFilter)
        {
            return x => x.Country.ToLower().Contains(chosenFilter.ToLower());
        }
    }
}