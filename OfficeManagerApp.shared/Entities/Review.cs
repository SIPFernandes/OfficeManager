namespace OfficeManager.Shared.Entities
{
    public class Review : BaseEntity
    {
        public float Classification { get; set; }
        public string Text { get; set; }
        public int RoomId { get; set; }
        public Room Room { get; set; }
        public int UserId { get; set; }
    }
}
