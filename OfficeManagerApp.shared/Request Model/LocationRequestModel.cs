using System.ComponentModel.DataAnnotations;

namespace OfficeManager.Shared.Request_Model
{
    public class LocationRequestModel
    {
        ///<example>Portugal</example>
        [Required(ErrorMessage = "Country is required.")]
        public string Country { get; set; }

        /// <example>Faro</example>
        [Required(ErrorMessage = "City is required.")]
        public string City { get; set; }
    }
}
