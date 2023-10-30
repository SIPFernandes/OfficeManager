using System.ComponentModel.DataAnnotations;

namespace OfficeManager.Shared.Request_Model
{
    public class RoleRequestModel
    {
        /// <example>Administrator</example>
        [Required(ErrorMessage = "Role is required.")]
        public string Name { get; set; }
    }
}
