using RabbitMQClient.Shared.Data.Models;
using System.Text.Json;

namespace RabbitMQClient.Shared.Areas.Helpers
{
    public static class MessageHelper
    {
        public static string CreateMessage(string publisherName,
            object content, string? action, string? triggeredBy)
        {
            var msg = new MessageModel
            {
                PublisherService = publisherName,
                Content = JsonSerializer.Serialize(content),
                Action = action,
                TriggeredBy = triggeredBy
            };

            return JsonSerializer.Serialize(msg);
        }

        public static T? GetMessage<T>(string message)
        {                       
            return JsonSerializer.Deserialize<T>(message);
        }
    }
}
