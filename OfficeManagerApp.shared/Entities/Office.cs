namespace OfficeManager.Shared.Entities
{
    public class Office : BaseEntity
    {
        public string Name { get; set; }
        public int CompanyId { get; set; }
        public Company Company { get; set; }
        public int LocationId { get; set; }
        public LocationModel Location { get; set; }
        public int? ImageId { get; set; }
        public Image? Image { get; set; }
        public IList<Room> Rooms { get; set; }
    }
}
