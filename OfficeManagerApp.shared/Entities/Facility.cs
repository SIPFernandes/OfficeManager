namespace OfficeManager.Shared.Entities
{
    public class Facility : BaseEntity
    {
        public string Name { get; set; }
        public IList<RoomFacility> RoomFacilities { get; set; }
        public int? ImageId { get; set; }
        public Image Image { get; set; }
    }
}
