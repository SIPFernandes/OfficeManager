namespace OfficeManagerApp.Data.Models
{
    public class EntityCardModel
    {
        public int Id { get; set; }
        public int ParentEntityId { get; set; }
        public string LeftText { get; set; }
        public string RightText { get; set; }
        public string Image { get; set; }
    }
}
