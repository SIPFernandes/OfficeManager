using System.ComponentModel.DataAnnotations;

namespace OfficeManager.Shared.Request_Model
{
    public class ImageRequestModel
    {
        /// <example>Some source link</example>
        [Required(ErrorMessage = "Image is required.")]
        [FileExtensions(Extensions = "jpg, gif, png, jpeg", ErrorMessage = "Extension file not supported.")]
        public string File { get; set; }

        /// <example>1</example>
        public int? RoomId { get; set; }
    }
}
