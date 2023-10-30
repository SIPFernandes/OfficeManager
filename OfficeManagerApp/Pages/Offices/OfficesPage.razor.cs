using Microsoft.AspNetCore.Components;
using OfficeManager.Shared.Entities;
using OfficeManagerApp.Areas.Helpers;
using OfficeManagerApp.Areas.Services.CompanyServices;
using OfficeManagerApp.Data.Constants.ConstsEN;
using OfficeManagerApp.Data.Models;
using OfficeManagerApp.Shared;

namespace OfficeManagerApp.Pages.Offices
{
    public partial class OfficesPage : ComponentBase
    {
        [CascadingParameter]
        public Loading LoadingComponent { get; set; }

        [Parameter]
        public static string FilterSelected { get; set; }

        [Parameter]
        public int CompanyId { get; set; }

        [Inject]
        private ICompanyHttpService CompanyHttpService { get; set; }

        private string[] allSortBy = { @ConstEN.SortBy_AZ,
                                       @ConstEN.SortBy_ZA };
        private Company company;
        private IList<string> allCountries;
        private IList<Office> filteredOfficesList;
        private IList<Office> allOffices;
        private string chosenFilter = string.Empty;
        private string chosenSort = string.Empty;
        private string searchExpression = string.Empty;
        private IList<EntityCardModel> entityCardModelList = new List<EntityCardModel>();

        protected override async Task OnInitializedAsync()
        {
            LoadingComponent.OpenLoadingComponent();

            company = await CompanyHttpService.Get(CompanyId);

            filteredOfficesList = await CompanyHttpService.OfficeHttpService
                .GetOfficesByCompanyId(CompanyId);

            allOffices = filteredOfficesList;

            PassData();

            allCountries = filteredOfficesList.Select(x => x.Location.Country).Distinct().ToList();

            LoadingComponent.CloseLoadingComponent();
        }

        private async void DeleteOffice(EntityCardModel entity)
        {
            LoadingComponent.OpenLoadingComponent();

            await CompanyHttpService.OfficeHttpService.Delete(entity.Id);

            entityCardModelList.Remove(entity);

            StateHasChanged();

            LoadingComponent.CloseLoadingComponent();
        }

        private void PassData()
        {
            entityCardModelList.Clear();

            if (filteredOfficesList != null)
            {
                entityCardModelList = filteredOfficesList.Select(x => new EntityCardModel()
                {
                    Id = x.Id,
                    ParentEntityId = x.CompanyId,
                    LeftText = x.Name,
                    RightText = x.Rooms.Count.ToString(),
                    Image = x.Image?.File
                }).ToList();
            }
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
                filteredOfficesList = filteredOfficesList.OrderBy(x => x.Location.City).ToList();
            }
            else if (FilterSelected == @ConstEN.SortBy_ZA)
            {
                filteredOfficesList = filteredOfficesList.OrderByDescending(x => x.Location.City).ToList();
            }

            PassData();

            StateHasChanged();
        }

        private void OnSearchbarChanged(string searchbarInput)
        {
            searchExpression = searchbarInput;

            CreateFilter();
        }

        private void CreateFilter()
        {
            Func<Office, bool> tempFilter = null;

            if (!string.IsNullOrEmpty(searchExpression))
            {
                FilterBaseHelper.AddFilter(ref tempFilter, SearchFunc(searchExpression));
            }
            if (!string.IsNullOrEmpty(chosenFilter) && !chosenFilter.Equals(ConstEN.All))
            {
                FilterBaseHelper.AddFilter(ref tempFilter, FilterFunc(chosenFilter));
            }

            var filterPredicate = tempFilter != null ? new Predicate<Office>(tempFilter) : null;

            if (filterPredicate != null)
            {
                filteredOfficesList = allOffices.Where(x => filterPredicate(x)).ToList();
            }
            else
            {
                filteredOfficesList = allOffices.ToList();
            }

            if (chosenSort != string.Empty)
            {
                OnSortChanged(chosenSort);
            }

            PassData();
        }

        public static Func<Office, bool> SearchFunc(string searchExpression)
        {
            return x => x.Location.City.ToLower().Contains(searchExpression.ToLower())
            || x.Location.Country.ToLower().Contains(searchExpression.ToLower());
        }

        public static Func<Office, bool> FilterFunc(string chosenFilter)
        {
            return x => x.Location.Country.ToLower().Contains(chosenFilter.ToLower());
        }
    }
}