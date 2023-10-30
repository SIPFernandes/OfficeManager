using System.ComponentModel.DataAnnotations;

namespace OfficeManager.Shared.Request_Model
{
    public class BookingRequestModel
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

        /// <example>1</example>
        [Required(ErrorMessage = "Must fill room size")]
        [Range(1, int.MaxValue, ErrorMessage = "Please enter a value bigger than {1}")]
        public int RoomSize { get; set; }

        /// <example>1</example>
        [Required(ErrorMessage = "UserId is required.")]
        public int UserId { get; set; }

        /// <example>1</example>
        [Required(ErrorMessage = "SeatId is required.")]
        public int SeatId { get; set; }
    }
}
