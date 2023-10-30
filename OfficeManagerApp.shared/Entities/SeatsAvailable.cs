namespace OfficeManager.Shared.Entities
{
    public class SeatsAvailable : BaseEntity
    {
        public DateTime Date { get; set; }
        public DateTime Hour { get; set; }
        public int RoomId { get; set; }
        public int AvailableSeatsNumber { get; set; }
    }
}
