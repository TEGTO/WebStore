using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RabbitMQ.Configuration;
using RabbitMQ.Enums;
using System.Text;
using System.Text.Json;

namespace RabbitMQ.RabbitMQ
{
    public class RabitMQProducer : IRabitMQProducer, IDisposable
    {
        private readonly RabbitMQFactorySettings rabbitMQFactorySettings;
        private IConnection connection;
        private bool disposed = false;

        public RabitMQProducer(RabbitMQFactorySettings rabbitMQFactorySettings)
        {
            this.rabbitMQFactorySettings = rabbitMQFactorySettings;
            connection = CreateConnection();
        }

        public void SendMessage<T>(T message, RabbitMQSendSettings sendSettings)
        {
            if (disposed)
            {
                throw new ObjectDisposedException(nameof(RabitMQProducer));
            }
            using (var channel = connection.CreateModel())
            {
                var exchangeType = ExchangeTypeEnumMethods.GetExchangeTypeFromEnum(sendSettings.ExchangeType);

                channel.ExchangeDeclare(sendSettings.ExchangeName, exchangeType, sendSettings.IsExchangeDurable,
                    sendSettings.IsExchangeAutoDelete, sendSettings.ExchangeArguments);
                if (sendSettings.DeclareQueue)
                {
                    channel.QueueDeclare(sendSettings.QueueName, exclusive: false);
                    channel.QueueBind(sendSettings.QueueName, sendSettings.ExchangeName, sendSettings.RoutingKey);
                }

                var json = JsonSerializer.Serialize(message);
                var body = Encoding.UTF8.GetBytes(json);
                channel.BasicPublish(sendSettings.ExchangeName, sendSettings.RoutingKey, body: body);
            }
        }
        public void ReceiveMessage(RabbitMQReceiveSettings receiveSettings, Action<string> handleMessage)
        {
            if (disposed)
            {
                throw new ObjectDisposedException(nameof(RabitMQProducer));
            }
            var channel = connection.CreateModel();
            var exchangeType = ExchangeTypeEnumMethods.GetExchangeTypeFromEnum(receiveSettings.ExchangeType);

            channel.ExchangeDeclare(receiveSettings.ExchangeName, exchangeType, receiveSettings.IsExchangeDurable,
                receiveSettings.IsExchangeAutoDelete, receiveSettings.ExchangeArguments);
            channel.QueueDeclare(receiveSettings.QueueName, exclusive: false);
            channel.QueueBind(receiveSettings.QueueName, receiveSettings.ExchangeName, receiveSettings.RoutingKey);
            channel.BasicQos(receiveSettings.PrefetchSize, receiveSettings.PrefetchCount, receiveSettings.Global);

            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                handleMessage?.Invoke(message);
                channel.BasicAck(ea.DeliveryTag, multiple: false);
            };

            channel.BasicConsume(queue: receiveSettings.QueueName, autoAck: false, consumer: consumer);
        }
        private IConnection CreateConnection()
        {
            var factory = new ConnectionFactory
            {
                HostName = rabbitMQFactorySettings.HostName,
                Port = rabbitMQFactorySettings.Port,
                UserName = rabbitMQFactorySettings.UserName,
                Password = rabbitMQFactorySettings.Password,
                AutomaticRecoveryEnabled = rabbitMQFactorySettings.AutomaticRecoveryEnabled
            };
            return factory.CreateConnection();
        }
        public void Dispose()
        {
            if (!disposed)
            {
                DisposeManagedResources();
                disposed = true;
            }
        }
        protected virtual void DisposeManagedResources()
        {
            connection?.Dispose();
        }
    }
}