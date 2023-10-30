using System.ComponentModel.DataAnnotations;

namespace OfficeManager.Shared.Request_Model
{
    public class UserRequestModel
    {
        /// <example>Carolina Dias</example>
        [Required(ErrorMessage = "Name is required.")]
        public string Name { get; set; }

        /// <example>carolina.dias@metyis.com</example>
        [Required(ErrorMessage = "Email is required.")]
        [RegularExpression(@"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$", ErrorMessage = "Please enter a valid email address.")]
        public string Email { get; set; }

        /// <example>912345678</example>
        [Required(ErrorMessage = "Phone number is required.")]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Please enter a valid phone number")]
        public string PhoneNumber { get; set; }

        /// <example>Software developer</example>
        [Required(ErrorMessage = "Position is required.")]
        public string Position { get; set; }

        /// <example>1</example>
        [Required(ErrorMessage = "Role is required.")]
        public int RoleId { get; set; }

        /// <example>1</example>
        [Required(ErrorMessage = "Company is required.")]
        public int CompanyId { get; set; }

        /// <example>2</example>
        [Required(ErrorMessage = "Office is required.")]
        public int OfficeId { get; set; }

        /// <example>Some source link</example>
        [Required(ErrorMessage = "Image is required.")]
        public string Image { get; set; }
    }
}
