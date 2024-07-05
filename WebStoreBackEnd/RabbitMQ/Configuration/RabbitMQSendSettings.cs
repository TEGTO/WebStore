using RabbitMQ.Enums;

namespace RabbitMQ.Configuration
{
    public class RabbitMQSendSettings
    {
        public string ExchangeName { get; set; }
        public string RoutingKey { get; set; }
        public string QueueName { get; set; }
        public ExchangeTypeEnum ExchangeType { get; set; }
    }
}