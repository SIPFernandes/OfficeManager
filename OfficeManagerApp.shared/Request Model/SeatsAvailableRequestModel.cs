using System.ComponentModel.DataAnnotations;

namespace OfficeManager.Shared.Request_Model
{
    public class SeatsAvailableRequestModel
    {
        /// <example>01-01-2001</example>
        [Required(ErrorMessage = "Date is required.")]
        public DateTime Date { get; set; }

        /// <example>08:00</example>
        [Required(ErrorMessage = "Hour is required.")]
        public DateTime Hour { get; set; }

        /// <example>1</example>
        [Required(ErrorMessage = "RoomId is required.")]
        public int RoomId { get; set; }

        public int AvailableSeatsNumber { get; set; }
    }
}
