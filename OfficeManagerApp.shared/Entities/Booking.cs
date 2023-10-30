namespace OfficeManager.Shared.Entities
{
    public class Booking : BaseEntity
    {
        public DateTime Date { get; set; }
        public DateTime Hour { get; set; }
        public int RoomId { get; set; }
        public int UserId { get; set; }
        public int SeatId { get; set; }

        //not in db
        public int RoomSize { get; set; }
    }
}
