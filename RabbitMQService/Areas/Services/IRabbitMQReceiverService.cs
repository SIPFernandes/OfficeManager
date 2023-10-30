namespace RabbitMQClient.Shared.Areas.Services
{
    public interface IRabbitMQReceiverService
    {
        public void Send(string queue, string data);
        public void Receive(string queue, Action<object?, string> action);
    }
}
