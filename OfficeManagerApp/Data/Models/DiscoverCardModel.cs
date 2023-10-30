namespace OfficeManagerApp.Data.Models
{
    public class DiscoverCardModel
    {
        public int Id { get; set; }
        public int ParentEntityId { get; set; }
        public string Title { get; set; }
        public string FirstInfo { get; set; }
        public string SecondInfo { get; set; }
        public string Image { get; set; }
    }
}

