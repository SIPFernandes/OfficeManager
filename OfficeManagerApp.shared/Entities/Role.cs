namespace OfficeManager.Shared.Entities
{
    public class Role : BaseEntity
    {
        public string Name { get; set; }

        public IList<User> Users { get; set; }
    }
}
