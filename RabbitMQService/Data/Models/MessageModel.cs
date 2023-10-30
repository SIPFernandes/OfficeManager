namespace RabbitMQClient.Shared.Data.Models
{
    public class MessageModel
    {        
        public string? PublisherService { get; set; }
        public string? Content { get; set; }
        public string? Action { get; set; }
        public string? TriggeredBy { get; set; }
    }   
}
