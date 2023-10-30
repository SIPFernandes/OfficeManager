using Microsoft.AspNetCore.Components;
using OfficeManager.Shared.Entities;
using OfficeManagerApp.Areas.Helpers;
using OfficeManagerApp.Areas.Services.BookingService.SeatsAvailableService;
using OfficeManagerApp.Areas.Services.CompanyServices;
using OfficeManagerApp.Areas.Services.Interfaces;
using OfficeManagerApp.Data.Constants;
using OfficeManagerApp.Data.Constants.ConstsEN;
using OfficeManagerApp.Data.Models;
using OfficeManagerApp.Shared;

namespace OfficeManagerApp.Pages.Bookings
{
    public partial class DiscoverRoomsPage : ComponentBase
    {
        [CascadingParameter]
        public Loading LoadingComponent { get; set; }

        [Parameter]
        public int OfficeId { get; set; }

        [Inject]
        private ICompanyHttpService CompanyHttpService { get; set; }

        [Inject]
        private ISeatsAvailableHttpService SeatAvailableHttpService { get; set; }

        [Inject]
        private IDateSelectedService DateSelectedService { get; set; }

        private string searchExpression = string.Empty;
        private DateTime dateSelected;
        private IList<DiscoverCardModel> discoverCardModelList = new List<DiscoverCardModel>();
        private IList<DiscoverCardModel> filteredDiscoverCardModelList = new List<DiscoverCardModel>();
        private IEnumerable<SeatsAvailable> seatsUnavailablesByDate = new List<SeatsAvailable>();

        protected override async Task OnInitializedAsync()
        {
            LoadingComponent.OpenLoadingComponent();

            await OnDateSelectedChanged(dateSelected);
        }

        public static Func<DiscoverCardModel, bool> SearchFunc(string searchExpression)
        {
            return x => x.Title.ToLower().Contains(searchExpression.ToLower());
        }

        private void OnSearchbarChanged(string searchbarInput)
        {
            searchExpression = searchbarInput;

            CreateFilter();
        }

        private void CreateFilter()
        {
            Func<DiscoverCardModel, bool> tempFilter = null;

            if (!string.IsNullOrEmpty(searchExpression))
            {
                FilterBaseHelper.AddFilter(ref tempFilter, SearchFunc(searchExpression));
            }

            var filterPredicate = tempFilter != null ? new Predicate<DiscoverCardModel>(tempFilter) : null;

            if (filterPredicate != null)
            {
                filteredDiscoverCardModelList = discoverCardModelList.Where(x => filterPredicate(x)).ToList();
            }
            else
            {
                filteredDiscoverCardModelList = discoverCardModelList.ToList();
            }
        }

        private async Task OnDateSelectedChanged(DateTime date)
        {
            LoadingComponent.OpenLoadingComponent();

            dateSelected = date;

            if (dateSelected == DateTime.MinValue)
            {
                var getDate = DateSelectedService.GetDate();

                dateSelected = getDate == DateTime.MinValue ? DateTime.Now : getDate;
            }

            DateSelectedService.SelectDate(dateSelected);

            await CheckRoomsAvailability();

            LoadingComponent.CloseLoadingComponent();
        }

        private async Task CheckRoomsAvailability()
        {
            seatsUnavailablesByDate = await SeatAvailableHttpService.GetSeatsUnavailableByDate(dateSelected);

            var roomsUnavailablesIds = seatsUnavailablesByDate.Select(x => x.RoomId).ToList();

            var roomsAvailable = await CompanyHttpService.OfficeHttpService.RoomHttpService.GetAvailableRooms(roomsUnavailablesIds);

            discoverCardModelList.Clear();

            foreach(var roomAvailable in roomsAvailable)
            {
                if(OfficeId == 0 || roomAvailable.OfficeId == OfficeId)
                {
                    var discoverCard = new DiscoverCardModel()
                    {
                        Id = roomAvailable.Id,
                        Title = roomAvailable.Name,
                        FirstInfo = roomAvailable.Office.Location.City + ", " + roomAvailable.Office.Location.Country,
                        SecondInfo = await GetSeatsLeft(roomAvailable),
                        Image = roomAvailable.Images == null
                        ? AssetsConst.ImageConst.StylePageImageConst.DefaultImage
                        : roomAvailable.Images[0].File
                    };

                    discoverCardModelList.Add(discoverCard);

                    filteredDiscoverCardModelList.Add(discoverCard);
                }
            }

            CreateFilter();

            StateHasChanged();
        }

        private async Task<string> GetSeatsLeft(Room room)
        {
            var seatsLeft = await SeatAvailableHttpService.GetSeatsLeftByRoomIdDate(room.Id, dateSelected);

            if(seatsLeft == null) 
            {
                var seats = room.Seats.Where(x => x.Name == ConstEN.Chair);              

                var seatsUnavailables = seatsUnavailablesByDate.Where(x => x.RoomId == room.Id);

                var seatsAvailable = seats.Count() - seatsUnavailables.Count();

                return seatsAvailable.ToString();
            }

            return seatsLeft.AvailableSeatsNumber.ToString();
        }
    }
}
