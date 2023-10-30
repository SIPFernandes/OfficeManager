using Microsoft.AspNetCore.Components;
using OfficeManagerApp.Areas.Helpers;
using OfficeManagerApp.Areas.Services.CompanyServices;
using OfficeManagerApp.Areas.Services.Interfaces;
using OfficeManagerApp.Data.Constants.ConstsEN;
using OfficeManagerApp.Shared.ModalDialog;
using LocationModel = OfficeManager.Shared.Entities.LocationModel;

namespace OfficeManagerApp.Shared
{
    public partial class LocationList : ComponentBase
    {
        [CascadingParameter]
        public Loading LoadingComponent { get; set; }

        [Parameter]
        public IList<LocationModel> ContentList { get; set; }

        [Parameter]
        public bool IsFlow { get; set; }

        [Parameter]
        public static string FilterSelected { get; set; }

        [Parameter]
        public int SelectedLocationId { get; set; }

        [Parameter]
        public EventCallback<LocationModel> SelectedLocationChanged { get; set; }

        [Inject]
        private ICompanyHttpService CompanyHttpService { get; set; }

        [Inject]
        private IModalDialogService ModalDialogService {  get; set; }
        
        private IList<LocationModel> filteredLocationsList;

        private string searchExpression = string.Empty;
        private LocationModel location; 

        protected override void OnParametersSet()
        {
            if (ContentList != null)
            {
                filteredLocationsList = ContentList;
            }
            else if(!IsFlow)
            {
                filteredLocationsList = ContentList;
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

        private void SelectLocation(LocationModel location)
        {
            SelectedLocationId = location.Id;

            SelectedLocationChanged.InvokeAsync(location);
        }

        //Filter
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

            var filterPredicate = tempFilter != null ? new Predicate<LocationModel>(tempFilter) : null;

            if (filterPredicate != null)
            {
                filteredLocationsList = ContentList.Where(x => filterPredicate(x)).ToList();
            }
            else
            {
                filteredLocationsList = ContentList.ToList();
            }
        }       

        private void OpenDeleteDialog(LocationModel selectedLocation)
        {
            location = selectedLocation;

            ModalDialogService.OpenModalDialog(EventCallback.Factory.Create(this, () => DeleteLocation()),
                string.Format(ConstEN.DeleteEntityFormat, ConstEN.LocationLowerCase), ModalDialogEnum.DeleteCancel,
                string.Format(ConstEN.DeleteTextFormat, ConstEN.LocationLowerCase));
        }

        private async void DeleteLocation()
        {
            LoadingComponent.OpenLoadingComponent();

            await CompanyHttpService.OfficeHttpService
                .LocationHttpService.Delete(location.Id);

            filteredLocationsList.Remove(location);

            StateHasChanged();

            LoadingComponent.CloseLoadingComponent();
        }
    }
}