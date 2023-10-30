using OfficeManager.Shared.Entities;
using System.ComponentModel.DataAnnotations;

namespace OfficeManager.Shared.Request_Model
{
    /// <example>Office Request Model</example>
    public class OfficeRequestModel
    {
        /// <example>Faro Office</example>
        [Required(ErrorMessage = "Name is required.")]
        public string? Name { get; set; }

        /// <example>Some source link</example>
        public string? Image { get; set; }

        /// <example>4</example>
        [Required(ErrorMessage = "CompanyId is required.")]
        public int CompanyId { get; set; }

        /// <example>4</example>
        [Required(ErrorMessage = "Location is required.")]
        public int LocationId { get; set; }
        /// <example>Location Model</example>
        public LocationModel? Location { get; set; }
    }
}