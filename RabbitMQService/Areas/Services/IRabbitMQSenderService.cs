namespace RabbitMQClient.Shared.Areas.Services
{
    public interface IRabbitMQSenderService
    {
        public void Send(string queue, string data);
    }
}
