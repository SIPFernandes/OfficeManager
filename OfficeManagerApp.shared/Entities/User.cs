namespace OfficeManager.Shared.Entities
{
    public class User : BaseEntity
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Position { get; set; }
        public int CompanyId { get; set; }
        public int? OfficeId { get; set; }
        public int RoleId { get; set; }
        public string Image { get; set; }
        public Role Role { get; set; }

    }
}
