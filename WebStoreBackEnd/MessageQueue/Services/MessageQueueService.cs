using MessageQueue.Configuration;
using MessageQueue.Enums;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Text.Json;

namespace MessageQueue.Services
{
    public class MessageQueueService : IMessageQueueService, IDisposable
    {
        private readonly FactorySettings rabbitMQFactorySettings;
        private IConnection connection;
        private bool disposed = false;

        public MessageQueueService(FactorySettings rabbitMQFactorySettings)
        {
            this.rabbitMQFactorySettings = rabbitMQFactorySettings;
            connection = CreateConnection();
        }

        public void SendMessage<T>(T message, SendSettings sendSettings)
        {
            if (disposed)
            {
                throw new ObjectDisposedException(nameof(MessageQueueService));
            }
            using (var channel = connection.CreateModel())
            {
                var exchangeSettings = sendSettings.ExchangeSettings;
                var exchangeType = ExchangeTypeMethods.GetExchangeTypeFromEnum(exchangeSettings.Type);

                channel.ExchangeDeclare(exchangeSettings.Name, exchangeType, exchangeSettings.IsDurable,
                    exchangeSettings.IsAutoDelete, exchangeSettings.Arguments);
                if (sendSettings.QueueSettings != null)
                {
                    var queueSettings = sendSettings.QueueSettings;
                    channel.QueueDeclare(queueSettings.Name, queueSettings.IsDurable,
                        queueSettings.IsExclusive, queueSettings.IsAutoDelete, queueSettings.Arguments);
                    channel.QueueBind(queueSettings.Name, exchangeSettings.Name, sendSettings.RoutingKey);
                }

                var json = JsonSerializer.Serialize(message);
                var body = Encoding.UTF8.GetBytes(json);
                channel.BasicPublish(exchangeSettings.Name, sendSettings.RoutingKey, body: body);
            }
        }
        public IModel ReceiveMessage(ReceiveSettings receiveSettings, Action<string> handleMessage)
        {
            if (disposed)
            {
                throw new ObjectDisposedException(nameof(MessageQueueService));
            }
            var channel = connection.CreateModel();
            var exchangeSettings = receiveSettings.ExchangeSettings;
            var queueSettings = receiveSettings.QueueSettings;
            var exchangeType = ExchangeTypeMethods.GetExchangeTypeFromEnum(exchangeSettings.Type);

            channel.ExchangeDeclare(exchangeSettings.Name, exchangeType, exchangeSettings.IsDurable,
                exchangeSettings.IsAutoDelete, exchangeSettings.Arguments);
            channel.QueueDeclare(queueSettings.Name, queueSettings.IsDurable,
                         queueSettings.IsExclusive, queueSettings.IsAutoDelete, queueSettings.Arguments);
            channel.QueueBind(queueSettings.Name, exchangeSettings.Name, receiveSettings.RoutingKey);
            channel.BasicQos(receiveSettings.PrefetchSize, receiveSettings.PrefetchCount, receiveSettings.Global);

            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                handleMessage?.Invoke(message);
                channel.BasicAck(ea.DeliveryTag, multiple: false);
            };

            channel.BasicConsume(queueSettings.Name, autoAck: false, consumer: consumer);
            return channel;
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