using Microsoft.AspNetCore.Components;
using OfficeManager.Shared.Entities;
using OfficeManagerApp.Areas.Services.CompanyServices;
using OfficeManagerApp.Areas.Services.Interfaces;
using OfficeManagerApp.Data.Constants;
using OfficeManagerApp.Data.Constants.ConstsEN;
using OfficeManagerApp.Shared.ModalDialog;

namespace OfficeManagerApp.Shared
{
    public partial class CardsRoom : ComponentBase
    {
        [CascadingParameter]
        public Loading LoadingComponent { get; set; }

        [Parameter]
        public IList<Room> RoomsList { get; set; }

        [Parameter]
        public Office Office { get; set; }

        [Parameter]
        public bool IsVertical { get; set; }

        [Inject]
        private ICompanyHttpService _companyHttpService { get; set; }

        [Inject]
        private IModalDialogService _modalDialogService { get; set; }

        [Inject]
        public IJSInteropService JSInteropService { get; set; }

        private string _principalImage;
        private bool isOpen = false;
        public bool DeleteDialogOpen { get; set; }
        private Room _room;

        private async Task OpenCloseDropdown(int id, bool isMouseOut)
        {
            isMouseOut = !isMouseOut;

            await JSInteropService.OpenCloseDropdown(id.ToString(), isMouseOut);
        }

        private async void OpenDeleteDialog(Room selectedRoom)
        {
            _room = selectedRoom;

            _modalDialogService.OpenModalDialog(EventCallback.Factory.Create(this, () => DeleteRoom()),
                string.Format(ConstEN.DeleteEntityFormat, ConstEN.RoomLowerCase), ModalDialogEnum.DeleteCancel,
                string.Format(ConstEN.DeleteTextFormat, ConstEN.RoomLowerCase));

            await OpenCloseDropdown(_room.Id, false);
        }

        private async void DeleteRoom()
        {
            LoadingComponent.OpenLoadingComponent();

            await _companyHttpService.OfficeHttpService
                .RoomHttpService.Delete(_room.Id);

            RoomsList.Remove(_room);

            StateHasChanged();

            LoadingComponent.CloseLoadingComponent();
        }

        private string GetImage(Room room)
        {
            _principalImage = room.Images.Count == 0 
                ? AssetsConst.ImageConst.StylePageImageConst.DefaultImage 
                : room.Images[0].File;

            return _principalImage;
        }
    }
}
