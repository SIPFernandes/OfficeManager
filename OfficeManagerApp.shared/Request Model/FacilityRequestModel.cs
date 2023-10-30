using OfficeManager.Shared.Entities;
using System.ComponentModel.DataAnnotations;

namespace OfficeManager.Shared.Request_Model
{
    public class FacilityRequestModel
    {
        /// <example>Wi-Fi</example>
        [Required(ErrorMessage = "Name is required.")]
        public string Name { get; set; }

        /// <example>Some source link</example>
        [Required(ErrorMessage = "Image is required.")]
        public Image Image { get; set; }
    }
}