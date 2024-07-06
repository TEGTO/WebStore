using RabbitMQ.Enums;

namespace RabbitMQ.Configuration
{
    public class RabbitMQReceiveSettings
    {
        public required string ExchangeName { get; set; }
        public required string RoutingKey { get; set; }
        public required string QueueName { get; set; }
        public ExchangeTypeEnum ExchangeType { get; set; } = ExchangeTypeEnum.Direct;
        public bool IsExchangeDurable { get; set; }
        public bool IsExchangeAutoDelete { get; set; }
        public IDictionary<string, object>? ExchangeArguments { get; set; }
        public uint PrefetchSize { get; set; }
        public ushort PrefetchCount { get; set; } = 1;
        public bool Global { get; set; }
    }
}
