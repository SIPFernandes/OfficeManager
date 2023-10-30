namespace OfficeManager.Shared.Entities
{
    public class Image : BaseEntity
    {
        public string File { get; set; }
        public int? RoomId { get; set; }
        public Room? Room { get; set; }
        public Company Company { get; set; }
        public Facility Facility { get; set; }
        public Office Office { get; set; }
    }
}
