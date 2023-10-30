namespace OfficeManager.Shared.Entities
{
    public class Room : BaseEntity
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public string Description { get; set; }
        public DateTime OpeningHour { get; set; }
        public DateTime ClosingHour { get; set; }
        public string Status { get; set; }
        public bool CanBook { get; set; }
        public int OfficeId { get; set; }
        public Office Office { get; set; }
        public IList<RoomFacility> RoomFacilities { get; set; }
        public IList<Image> Images { get; set; }
        public IList<Review> Reviews { get; set; }
        public IList<Seat> Seats { get; set; }
    }
}
