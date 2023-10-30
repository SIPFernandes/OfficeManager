using Microsoft.AspNetCore.Components;
using OfficeManager.Shared.Entities;
using OfficeManagerApp.Areas.Helpers;
using OfficeManagerApp.Areas.Services.CompanyServices;
using OfficeManagerApp.Areas.Services.Interfaces;
using OfficeManagerApp.Data.Constants.ConstsEN;
using OfficeManagerApp.Data.Models;
using OfficeManagerApp.Shared;

namespace OfficeManagerApp.Pages.Companies
{
    public partial class CompaniesPage : ComponentBase
    {
        [CascadingParameter]
        public Loading LoadingComponent { get; set; }

        [Parameter]
        public static string FilterSelected { get; set; }

        [Inject]
        private ICompanyHttpService CompanyHttpService { get; set; }

        //TODO:Add filter - number of offices???

        private IList<Company> allCompanies;

        private IList<Company> filteredCompaniesList;

        private string[] allSortBy = { @ConstEN.SortBy_AZ,
                                       @ConstEN.SortBy_ZA };

        //private string chosenFilter = string.Empty;

        private string chosenSort = string.Empty;

        private string searchExpression = string.Empty;

        private IList<EntityCardModel> entityCardModelList = new List<EntityCardModel>();

        protected override async Task OnInitializedAsync()
        {
            LoadingComponent.OpenLoadingComponent();

            allCompanies = await CompanyHttpService.GetAll();

            filteredCompaniesList = allCompanies;

            PassData();

            LoadingComponent.CloseLoadingComponent();
        }

        public static Func<Company, bool> SearchFunc(string searchExpression)
        {
            return x => x.Name.ToLower().Contains(searchExpression.ToLower());
        }

        //public static Func<Company, bool> FilterFunc(string chosenFilter)
        //{
        //    return x => x.Name.ToLower().Contains(chosenFilter.ToLower());
        //}

        private async void DeleteCompany(EntityCardModel entity)
        {
            LoadingComponent.OpenLoadingComponent();

            await CompanyHttpService.Delete(entity.Id);

            entityCardModelList.Remove(entity);

            StateHasChanged();

            LoadingComponent.CloseLoadingComponent();
        }

        private void PassData()
        {
            entityCardModelList.Clear();

            if (filteredCompaniesList != null)
            {
                entityCardModelList = filteredCompaniesList.Select(x => new EntityCardModel()
                {
                    Id = x.Id,
                    LeftText = x.Name,
                    RightText = string.Empty,
                    Image = x.Image?.File
                }).ToList();
            }
        }

        //private void OnFilterChanged(string filter)
        //{
        //    chosenFilter = filter;

        //    CreateFilter();
        //}

        private void OnSortChanged(string sort)
        {
            FilterSelected = sort;

            chosenSort = sort;

            if (FilterSelected == @ConstEN.SortBy_AZ)
            {
                filteredCompaniesList = filteredCompaniesList.OrderBy(x => x.Name).ToList();
            }
            else if (FilterSelected == @ConstEN.SortBy_ZA)
            {
                filteredCompaniesList = filteredCompaniesList.OrderByDescending(x => x.Name).ToList();
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
            Func<Company, bool> tempFilter = null;

            if (!string.IsNullOrEmpty(searchExpression))
            {
                FilterBaseHelper.AddFilter(ref tempFilter, SearchFunc(searchExpression));
            }
            //if (!string.IsNullOrEmpty(chosenFilter) && !chosenFilter.Equals(ConstEN.All))
            //{
            //    FilterBaseHelper.AddFilter(ref tempFilter, FilterFunc(chosenFilter));
            //}

            var filterPredicate = tempFilter != null ? new Predicate<Company>(tempFilter) : null;

            if (filterPredicate != null)
            {
                filteredCompaniesList = allCompanies.Where(x => filterPredicate(x)).ToList();
            }
            else
            {
                filteredCompaniesList = allCompanies.ToList();
            }

            if (chosenSort != string.Empty)
            {
                OnSortChanged(chosenSort);
            }

            PassData();
        }
    }
}