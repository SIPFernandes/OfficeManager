namespace OfficeManager.Shared.Response_Model
{
    public class BookingResponseModel
    {
        ///<example>1</example>
        public int Id { get; set; }

        /// <example>01-01-2001</example>
        public DateTime Date { get; set; }

        /// <example>08:00</example>
        public DateTime Hour { get; set; }

        /// <example>1</example>
        public int RoomId { get; set; }

        /// <example>1</example>
        public int RoomSize { get; set; }

        /// <example>1</example>
        public int UserId { get; set; }

        /// <example>1</example>
        public int SeatId { get; set; }
    }
}
