using Microsoft.AspNetCore.Components;
using OfficeManager.Shared.Entities;
using OfficeManagerApp.Areas.Services.BookingService;
using OfficeManagerApp.Areas.Services.CompanyServices;
using OfficeManagerApp.Areas.Services.Interfaces;
using OfficeManagerApp.Data.Constants.ConstsEN;
using OfficeManagerApp.Shared.ModalDialog;

namespace OfficeManagerApp.Shared
{
    public partial class CardsBooking : ComponentBase
    {
        [CascadingParameter]
        public Loading LoadingComponent { get; set; }

        [Parameter]
        public IList<Booking> BookingsList { get; set; }

        [Inject]
        private IBookingHttpService _bookingHttpService { get; set; }

        [Inject]
        private ICompanyHttpService _companyHttpService { get; set; }

        [Inject]
        private IModalDialogService _modalDialogService { get; set; }

        [Inject]
        public IJSInteropService JSInteropService { get; set; }

        private Booking _booking;
        private bool _informationLoaded;
        private IDictionary<int, Room> _roomDictionary = new Dictionary<int, Room>();

        protected override async Task OnParametersSetAsync()
        {
            if(!_informationLoaded && BookingsList != null)
            {
                var roomIdsList = BookingsList.Select(x => x.RoomId).Distinct().ToList();

                var test = BookingsList;

                _roomDictionary = await _companyHttpService.OfficeHttpService
                    .RoomHttpService.GetRoomsByIds(roomIdsList);

                _informationLoaded = true;
            }            
        }
        private async Task OpenCloseDropdown(int id, bool isMouseOut)
        {
            isMouseOut = !isMouseOut;

            await JSInteropService.OpenCloseDropdown(id.ToString(), isMouseOut);
        }

        private async void OpenDeleteDialog(Booking selectedBooking)
        {
            _booking = selectedBooking;

            _modalDialogService.OpenModalDialog(EventCallback.Factory.Create(this, () => DeleteBooking()),
                string.Format(ConstEN.DeleteEntityFormat, ConstEN.BookingLowerCase), ModalDialogEnum.DeleteCancel,
                string.Format(ConstEN.DeleteTextFormat, ConstEN.BookingLowerCase));

            await OpenCloseDropdown(_booking.Id, false);
        }

        private async void DeleteBooking()
        {
            LoadingComponent.OpenLoadingComponent();

            await _bookingHttpService.Delete(_booking.Id);

            BookingsList.Remove(_booking);

            StateHasChanged();

            LoadingComponent.CloseLoadingComponent();
        }
    }
}
