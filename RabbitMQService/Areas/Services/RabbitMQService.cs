using Microsoft.Extensions.Configuration;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RabbitMQClient.Shared.Data.Consts;
using System.Text;

namespace RabbitMQClient.Shared.Areas.Services
{
    public class RabbitMQService : IRabbitMQSenderService, IRabbitMQReceiverService
    {
        private readonly IConfiguration _configuration;
        private readonly IConnection _connection;

        public RabbitMQService(IConfiguration configuration)
        {
            _configuration = configuration;

            _connection = GetConnection();            
        }

        public void Send(string queue, string data)
        {
            var channel = _connection.CreateModel();

            channel.QueueDeclare(queue, true, false, false, null);

            channel.BasicPublish(string.Empty, queue, null, Encoding.UTF8.GetBytes(data));
        }

        public void Receive(string queue, Action<object?, string> action)
        {
            var channel = _connection.CreateModel();

            channel.QueueDeclare(queue, true, false, false, null);

            var consumer = new EventingBasicConsumer(channel);

            consumer.Received += (model, ea) =>
            {
                var body = ea.Body.ToArray();

                var message = Encoding.UTF8.GetString(body);

                action(model, message);
            };

            channel.BasicConsume(queue: queue,
                autoAck: true, consumer: consumer);
        }

        private IConnection GetConnection()
        {
            var section = _configuration.GetSection(RabbitMQConst.Section);
            var useCredentials = section.GetValue<bool>(RabbitMQConst.Configuration.HostName);

            ConnectionFactory connectionFactory;

            if (useCredentials)
            {
                connectionFactory = new()
                {                    
                    UserName = section.GetValue<string>(RabbitMQConst.Configuration.UserName),
                    Password = section.GetValue<string>(RabbitMQConst.Configuration.Password),
                };
            }
            else
            {
                connectionFactory = new()
                {
                    HostName = section.GetValue<string>(RabbitMQConst.Configuration.HostName),                    
                };
            }

            return connectionFactory.CreateConnection();
        }
    }
}
