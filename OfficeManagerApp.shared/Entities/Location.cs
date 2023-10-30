namespace OfficeManager.Shared.Entities
{
    public class LocationModel : BaseEntity
    {
        public string Country { get; set; }
        public string City { get; set; }
        public IList<Office> Offices { get; set; }
    }
}
