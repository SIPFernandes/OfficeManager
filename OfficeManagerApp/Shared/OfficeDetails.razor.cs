using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using OfficeManager.Shared.Request_Model;

namespace OfficeManagerApp.Shared
{
    public partial class OfficeDetails : ComponentBase
    {
        [Parameter]
        public OfficeRequestModel OfficeRequestModel { get; set; }

        [Parameter]
        public string CompanyName { get; set; }

        [Parameter]
        public EventCallback<bool> IsValidChanged { get; set; }

        private IList<string> _imageSources = new List<string>();

        private EditContext _officeEditContext;

        private bool _isValid = false;

        protected override void OnInitialized()
        {
            _officeEditContext = new EditContext(OfficeRequestModel);

            _officeEditContext.OnFieldChanged += (o, e) => EditContext_OnFieldChanged();

            if (!string.IsNullOrEmpty(OfficeRequestModel.Image))
            {
                _imageSources.Add(OfficeRequestModel.Image);
            }
        }

        protected void OnUploadImageChanged(IList<string> images)
        {
            OfficeRequestModel.Image = images.Count == 0 ? null : images.First();

            _imageSources = images;

            EditContext_OnFieldChanged();
        }

        private void EditContext_OnFieldChanged()
        {
            _isValid = _officeEditContext.Validate();

            IsValidChanged.InvokeAsync(_isValid);
        }
    }
}
