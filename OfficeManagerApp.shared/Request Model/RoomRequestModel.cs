using System.ComponentModel.DataAnnotations;

namespace OfficeManager.Shared.Request_Model
{
    public class RoomRequestModel
    {
        /// <example>Room 19</example>
        [Required(ErrorMessage = "Name is required.")]
        public string Name { get; set; }

        /// <example>Working room</example>
        [Required(ErrorMessage = "Type is required.")]
        public string Type { get; set; }

        /// <example>In today’s era of disruption, setting direction is more critical than ever. At Metyis we believe that powerful strategies are driven by integrated sets of choices and a firm commitment to creating acceptance and change across all organisational levels.</example>
        [Required(ErrorMessage = "Description is required.")]
        public string Description { get; set; }

        /// <example>08:30</example>
        [Required(ErrorMessage = "Opening hour is required.")]
        public DateTime OpeningHour { get; set; }

        /// <example>19:00</example>
        [Required(ErrorMessage = "Closing hour is required.")]
        public DateTime ClosingHour { get; set; }

        /// <example>Available</example>
        [Required(ErrorMessage = "Status is required.")]
        public string Status { get; set; }

        /// <example>true</example>
        public bool CanBook { get; set; }

        /// <example>1</example>
        [Required(ErrorMessage = "OfficeId is required.")]
        public int OfficeId { get; set; }
        public IList<RoomFacilityRequestModel> RoomFacilities { get; set; }
        
        /// <example>Some source link</example>
        public IList<string> Images { get; set; }

        public IList<SeatRequestModel> Seats { get; set; }
    }
}