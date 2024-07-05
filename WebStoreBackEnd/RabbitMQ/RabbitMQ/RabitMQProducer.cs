using RabbitMQ.Client;
using RabbitMQ.Configuration;
using RabbitMQ.Enums;
using System.Text;
using System.Text.Json;

namespace RabbitMQ.RabbitMQ
{
    public class RabitMQProducer : IRabitMQProducer
    {
        private readonly RabbitMQFactorySettings rabbigMQFactorySettings;

        public RabitMQProducer(RabbitMQFactorySettings rabbigMQFactorySettings)
        {
            this.rabbigMQFactorySettings = rabbigMQFactorySettings;
        }

        public void SendMessage<T>(T message, RabbitMQSendSettings sendSettings)
        {
            var factory = new ConnectionFactory
            {
                HostName = rabbigMQFactorySettings.HostName,
                Port = rabbigMQFactorySettings.Port,
                UserName = rabbigMQFactorySettings.UserName,
                Password = rabbigMQFactorySettings.Password,
            };
            var connection = factory.CreateConnection();
            using (var channel = connection.CreateModel())
            {
                var exchangeType = ExchangeTypeEnumMethods.GetExchangeTypeFromEnum(sendSettings.ExchangeType);
                channel.ExchangeDeclare(sendSettings.ExchangeName, exchangeType);
                channel.QueueDeclare(sendSettings.QueueName, exclusive: false);
                channel.QueueBind(sendSettings.QueueName, sendSettings.ExchangeName, sendSettings.RoutingKey);

                var json = JsonSerializer.Serialize(message);
                var body = Encoding.UTF8.GetBytes(json);
                channel.BasicPublish(sendSettings.ExchangeName, sendSettings.RoutingKey, body: body);
            }
        }
    }
}