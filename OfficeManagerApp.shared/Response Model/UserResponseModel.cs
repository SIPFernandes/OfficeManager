using OfficeManager.Shared.Entities;

namespace OfficeManager.Shared.Response_Model
{
    public class UserResponseModel
    {
        ///<example>1</example>
        public int Id { get; set; }

        /// <example>Carolina Dias</example>
        public string Name { get; set; }

        /// <example>carolina.dias@metyis.com</example>
        public string Email { get; set; }

        /// <example>912345678</example>
        public string PhoneNumber { get; set; }

        /// <example>Software developer</example>
        public string Position { get; set; }

        /// <example>1</example>
        public int RoleId { get; set; }

        /// <example>1</example>
        public int CompanyId { get; set; }

        /// <example>2</example>
        public int OfficeId { get; set; }

        /// <example>Some source link</example>
        public Image Image { get; set; }
    }
}
