using System.ComponentModel.DataAnnotations;

namespace OfficeManager.Shared.Request_Model
{
    public class CompanyRequestModel
    {
        /// <example>Faro Office</example>
        [Required(ErrorMessage = "Name is required.")]
        public string Name { get; set; }

        /// <example>In today’s era of disruption, setting direction is more critical than ever. At Metyis we believe that powerful strategies are driven by integrated sets of choices and a firm commitment to creating acceptance and change across all organisational levels.</example>
        [Required(ErrorMessage = "Description is required.")]
        public string Description { get; set; }

        /// <example>Some source link</example>
        public string? Image { get; set; }
    }
}