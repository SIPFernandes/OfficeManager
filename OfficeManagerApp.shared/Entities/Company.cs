namespace OfficeManager.Shared.Entities
{
    public class Company : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public IList<Office> Offices { get; set; }
        public int? ImageId { get; set; }
        public Image? Image { get; set; }

    }
}
