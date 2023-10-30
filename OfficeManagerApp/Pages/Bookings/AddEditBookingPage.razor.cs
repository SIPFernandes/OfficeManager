using Microsoft.AspNetCore.Components;
using OfficeManager.Shared.Entities;
using OfficeManager.Shared.Request_Model;
using OfficeManagerApp.Areas.Services.BookingService;
using OfficeManagerApp.Areas.Services.CompanyServices;
using OfficeManagerApp.Areas.Services.Interfaces;
using OfficeManagerApp.Areas.Services.UserService;
using OfficeManagerApp.Data.Constants;
using OfficeManagerApp.Data.Constants.ConstsEN;
using OfficeManagerApp.Shared;
using static OfficeManagerApp.Shared.RoomPlanning;

namespace OfficeManagerApp.Pages.Bookings
{
    public partial class AddEditBookingPage : ComponentBase
    {
        [CascadingParameter]
        public User User { get; set; }

        [CascadingParameter]
        public Loading LoadingComponent { get; set; }

        [Parameter]
        public int RoomId { get; set; }

        [Parameter]
        public int BookingId { get; set; }

        [Inject]
        private ICompanyHttpService _companyHttpService { get; set; }

        [Inject]
        private IBookingHttpService _bookingHttpService { get; set; }

        [Inject]
        private IUserHttpService _userHttpService { get; set; }

        [Inject]
        private IDateSelectedService _dateSelectedService { get; set; }

        [Inject]
        private NavigationManager _navigationManager { get; set; }

        private DateTime _dateSelected;
        private bool _isDateDisabled;
        private bool _isOnInitializedFinished;
        private IEnumerable<Booking> _allBookings;
        private IEnumerable<Seat> _allSeatsRoom;
        private Booking _booking = null;

        private static int _rows = 7;
        private static int _columns = 12;
        private BookingRequestModel bookingRequestModel;
        private SeatMatrix[,] _matrix = new SeatMatrix[_rows, _columns];
        private IDictionary<int, string> _userDictionary = new Dictionary<int, string>();

        protected override async Task OnInitializedAsync()
        {
            LoadingComponent.OpenLoadingComponent();

            if (BookingId != 0)
            {
                _booking = await _bookingHttpService.Get(BookingId);

                _dateSelected = _booking.Date;

                RoomId = _booking.RoomId;

                _isDateDisabled = true;
            }
            else
            {
                _dateSelected = _dateSelectedService.GetDate();

                if (_dateSelected == DateTime.MinValue)
                {
                    _dateSelected = DateTime.Today;
                }
            }

            await OnDateSelectedChanged(_dateSelected);

            _allBookings = await _bookingHttpService.GetBookingsByDateRoomId(RoomId, _dateSelected);

            _isOnInitializedFinished = true;
        }

        protected async Task CreateOrEditBooking()
        {
            LoadingComponent.OpenLoadingComponent();

            if (BookingId == 0)
            {
                await _bookingHttpService.Insert(bookingRequestModel);
            }
            else
            {
                await _bookingHttpService.Update(bookingRequestModel, BookingId);
            }

            Cancel();
        }

        private void OrganizeRoomPlanning()
        {
            foreach (var seat in _allSeatsRoom)
            {
                _matrix[seat.CoordinateX, seat.CoordinateY] =
                    new SeatMatrix
                    {
                        SeatName = seat.Name,
                        SeatId = seat.Id,
                    };

                if (seat.Name == ConstEN.Chair)
                {
                    bookingRequestModel.RoomSize++;

                    if (bookingRequestModel.SeatId == seat.Id)
                    {
                        _matrix[seat.CoordinateX, seat.CoordinateY].SeatName = ConstEN.Selected;
                    }
                    else if (_allBookings.Any(x => x.SeatId == seat.Id))
                    {
                        _matrix[seat.CoordinateX, seat.CoordinateY].SeatName = ConstEN.Occupated;

                        var userId = _allBookings.First(x => x.SeatId == seat.Id).UserId;

                        _matrix[seat.CoordinateX, seat.CoordinateY].UserName = _userDictionary[userId];
                    }
                    else if (bookingRequestModel.SeatId == 0)
                    {
                        bookingRequestModel.SeatId = seat.Id;

                        _matrix[seat.CoordinateX, seat.CoordinateY].SeatName = ConstEN.Selected;
                    }
                }
            }

            LoadingComponent.CloseLoadingComponent();
        }

        private void Cancel()
        {
            _navigationManager.NavigateTo(AppConst.BookingUrlConst.BookingsPage);
        }

        private void OnSeatSelectedChanged(int seatId)
        {
            bookingRequestModel.SeatId = seatId;
        }

        private async Task OnDateSelectedChanged(DateTime date)
        {
            LoadingComponent.OpenLoadingComponent();

            _dateSelected = date;

            _dateSelectedService.SelectDate(_dateSelected);

            await CheckSeatsAvailability();
        }

        private async Task CheckSeatsAvailability()
        {
            if (_allSeatsRoom == null)
            {
                _allSeatsRoom = await _companyHttpService.OfficeHttpService.RoomHttpService.SeatHttpService.GetSeatsByRoomId(RoomId);
            }

            _allBookings = await _bookingHttpService.GetBookingsByDateRoomId(RoomId, _dateSelected);

            var selectedSeatId = 0;

            _booking = _allBookings.FirstOrDefault(x => x.UserId == User.Id);

            //TODO: if admin _allbookings.FirstOrDefault(x => x.Id == BookingId && x.Date == _dateSelected)

            if (_booking != null)
            {
                BookingId = _booking.Id;

                selectedSeatId = _booking.SeatId;
            }

            if (bookingRequestModel == null)
            {
                bookingRequestModel = new BookingRequestModel()
                {
                    RoomId = RoomId,
                    Date = _dateSelected.Date,
                    Hour = _dateSelected,
                    SeatId = selectedSeatId,
                    UserId = User.Id
                };

                var userIds = _allBookings.Select(x => x.UserId).ToList();

                _userDictionary = await _userHttpService.GetUsersByIds(userIds);
            }
            else
            {
                bookingRequestModel.Date = _dateSelected.Date;

                bookingRequestModel.SeatId = selectedSeatId;
            }

            OrganizeRoomPlanning();

            StateHasChanged();
        }
    }
}
