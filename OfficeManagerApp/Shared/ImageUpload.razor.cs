using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;

namespace OfficeManagerApp.Shared
{
    public partial class ImageUpload : ComponentBase
    {
        [Parameter]
        public int MaxAllowedFiles { get; set; }

        [Parameter]
        public IList<string> ImageSources { get; set; }

        [Parameter]
        public EventCallback<IList<string>> ImageSourcesChanged { get; set; }

        [Parameter]
        public bool IsMultiple { get; set; }

        private string ErrorMessage;

        private int MaxFileSizeMb = 5;

        private async Task OnUploadChanged(InputFileChangeEventArgs e)
        {
            ErrorMessage = string.Empty;

            if (ImageSources.Count + e.FileCount > MaxAllowedFiles)
            {
                ErrorMessage = $"Only {MaxAllowedFiles} files can be uploaded";

                if (ImageSources.Count == MaxAllowedFiles)
                {
                    return;
                }
            }

            foreach (var file in e.GetMultipleFiles(MaxAllowedFiles).Take(MaxAllowedFiles - ImageSources.Count))
            {
                if (file.Size > MaxFileSizeMb * 1024 * 1024)
                {
                    ErrorMessage = $"The size limit for images is {MaxFileSizeMb} Mb";
                }
                else
                {
                    using var stream = file.OpenReadStream();

                    using var ms = new MemoryStream();

                    await stream.CopyToAsync(ms);

                    ImageSources.Add($"data:{file.ContentType};base64,{Convert.ToBase64String(ms.ToArray())}");
                }
            }

            await ImageSourcesChanged.InvokeAsync(ImageSources);
        }

        private async Task DeleteImage(string image)
        {
            ImageSources.Remove(image);

            await ImageSourcesChanged.InvokeAsync(ImageSources);
        }
    }
}
