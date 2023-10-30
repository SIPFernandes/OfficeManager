namespace OfficeManager.Shared.Entities
{
    public class RoomFacility : BaseEntity
    {
        public int RoomId { get; set; }
        public int FacilityId { get; set; }
        public Room Room { get; set; }
        public Facility Facility { get; set; }
    }
}
