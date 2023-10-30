namespace OfficeManager.Shared.Entities
{
    public class Seat : BaseEntity
    {
        public string Name { get; set; }
        public int CoordinateX { get; set; }
        public int CoordinateY { get; set; }
        public int RoomId { get; set; }
        public Room Room { get; set; }
    }
}
