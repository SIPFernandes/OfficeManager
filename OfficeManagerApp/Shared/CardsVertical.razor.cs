using Microsoft.AspNetCore.Components;
using OfficeManagerApp.Areas.Services.Interfaces;
using OfficeManagerApp.Data.Constants.ConstsEN;
using OfficeManagerApp.Data.Models;
using OfficeManagerApp.Shared.ModalDialog;

namespace OfficeManagerApp.Shared
{
    public partial class CardsVertical: ComponentBase
    {
        [Parameter]
        public IEnumerable<EntityCardModel> EntityDataList { get; set; }

        [Parameter]
        public string UrlGet { get; set; }

        [Parameter]
        public string UrlGoTo { get; set; }

        [Parameter]
        public EventCallback<EntityCardModel> OnDeleteEvent { get; set; }

        [Parameter]
        public string Entity { get; set; }

        [Inject]
        private IModalDialogService _modalDialogService { get; set; }

        [Inject]
        private NavigationManager _navigationManager { get; set; }

        [Inject]
        public IJSInteropService JSInteropService { get; set; }

        private EntityCardModel _entity;

        private async void OpenDeleteDialog(EntityCardModel selectedEntity)
        {
            _entity = selectedEntity;

            _modalDialogService.OpenModalDialog(EventCallback.Factory.Create(this, () => OnDeleteEvent.InvokeAsync(_entity)),
                string.Format(ConstEN.DeleteEntityFormat, Entity), ModalDialogEnum.DeleteCancel,
                string.Format(ConstEN.DeleteTextFormat, Entity));

            await OpenCloseDropdown(_entity.Id, false);
        }

        private async Task OpenCloseDropdown(int id, bool isMouseOut)
        {
            isMouseOut = !isMouseOut;
            
            await JSInteropService.OpenCloseDropdown(id.ToString(), isMouseOut);
        }

        private void GoToEntity(EntityCardModel entity)
        {
            if(entity.ParentEntityId == 0)
            {
                _navigationManager.NavigateTo(string.Format(UrlGet, entity.Id));
            }
            else
            {
                _navigationManager.NavigateTo(string.Format(UrlGet, entity.ParentEntityId, entity.Id));
            }
        }
    }
}