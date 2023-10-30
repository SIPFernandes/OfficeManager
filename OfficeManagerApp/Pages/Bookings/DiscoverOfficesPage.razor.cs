using Microsoft.AspNetCore.Components;
using OfficeManager.Shared.Entities;
using OfficeManagerApp.Areas.Helpers;
using OfficeManagerApp.Areas.Services.BookingService.SeatsAvailableService;
using OfficeManagerApp.Areas.Services.CompanyServices;
using OfficeManagerApp.Areas.Services.Interfaces;
using OfficeManagerApp.Data.Constants;
using OfficeManagerApp.Data.Models;
using OfficeManagerApp.Shared;

namespace OfficeManagerApp.Pages.Bookings
{
    public partial class DiscoverOfficesPage : ComponentBase
    {
        [CascadingParameter]
        public User User { get; set; }

        [CascadingParameter]
        public Loading LoadingComponent { get; set; }

        [Inject]
        private ICompanyHttpService CompanyHttpService { get; set; }

        [Inject]
        private ISeatsAvailableHttpService SeatAvailableHttpService { get; set; }

        [Inject]
        private IDateSelectedService DateSelectedService { get; set; }

        private string searchExpression = string.Empty;
        private DateTime dateSelected;
        private IList<Office> officesAvailables;
        private IList<Office> filteredOfficesList = new List<Office>();
        private IList<DiscoverCardModel> discoverCardModelList = new List<DiscoverCardModel>();

        protected override async Task OnInitializedAsync()
        {
            LoadingComponent.OpenLoadingComponent();

            await OnDateSelectedChanged(dateSelected);

            PassData();
        }

        public static Func<Office, bool> SearchFunc(string searchExpression)
        {
            return x => x.Name.ToLower().Contains(searchExpression.ToLower())
            || x.Location.Country.ToLower().Contains(searchExpression.ToLower())
            || x.Location.City.ToLower().Contains(searchExpression.ToLower());
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

            var filterPredicate = tempFilter != null ? new Predicate<Office>(tempFilter) : null;

            if (filterPredicate != null)
            {
                filteredOfficesList = officesAvailables.Where(x => filterPredicate(x)).ToList();
            }
            else
            {
                filteredOfficesList = officesAvailables.ToList();
            }

            PassData();
        }

        private async Task OnDateSelectedChanged(DateTime date)
        {
            LoadingComponent.OpenLoadingComponent();

            dateSelected = date;

            if(dateSelected == DateTime.MinValue)
            {
                var getDate = DateSelectedService.GetDate();

                dateSelected = getDate == DateTime.MinValue ? DateTime.Today : getDate;
            }

            DateSelectedService.SelectDate(dateSelected);

            await CheckOfficesAvailability();

            LoadingComponent.CloseLoadingComponent();
        }

        private async Task CheckOfficesAvailability()
        {
            var seatsUnavailables = await SeatAvailableHttpService.GetSeatsUnavailableByDate(dateSelected);

            var roomsUnavailablesIds = seatsUnavailables.Select(x => x.RoomId).ToList();

            var roomsAvailables = await CompanyHttpService.OfficeHttpService.RoomHttpService.GetAvailableRooms(roomsUnavailablesIds);

            officesAvailables = roomsAvailables.Select(x => x.Office).DistinctBy(x => x.Id).ToList();

            filteredOfficesList = officesAvailables;
            
            CreateFilter();

            StateHasChanged();
        }

        private void PassData()
        {
            discoverCardModelList.Clear();

            if (filteredOfficesList != null)
            {
                discoverCardModelList = filteredOfficesList.Select(x => new DiscoverCardModel()
                {
                    Id = x.Id,
                    Title = x.Name,
                    FirstInfo = x.Location.City + ", " + x.Location.Country,
                    SecondInfo = string.Empty,
                    Image = x.Image == null || string.IsNullOrEmpty(x.Image.File)
                        ? AssetsConst.ImageConst.StylePageImageConst.DefaultImage
                        : x.Image.File
                }).ToList();
            }
        }
    }
}
