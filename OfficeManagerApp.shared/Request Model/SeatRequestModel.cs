using System.ComponentModel.DataAnnotations;

namespace OfficeManager.Shared.Request_Model
{
    public class SeatRequestModel
    {
        public int Id { get; set; }

        /// <example>Table</example>
        [Required(ErrorMessage = "Name is required.")]
        public string Name { get; set; }

        /// <example>1</example>
        [Required(ErrorMessage = "CoordinateX is required.")]
        public int CoordinateX { get; set; }

        /// <example>1</example>
        [Required(ErrorMessage = "CoordinateY is required.")]
        public int CoordinateY { get; set; }

        /// <example>1</example>
        [Required(ErrorMessage = "RoomId is required.")]
        public int RoomId { get; set; }
    }
}
