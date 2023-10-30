using System.ComponentModel.DataAnnotations;

namespace OfficeManager.Shared.Request_Model
{
    public class RoomFacilityRequestModel
    {
        public int Id { get; set; }

        /// <example>4</example>
        [Required(ErrorMessage = "RoomId is required.")]
        public int RoomId { get; set; }

        /// <example>2</example>
        [Required(ErrorMessage = "FacilityId is required.")]
        public int FacilityId { get; set; }
    }
}
