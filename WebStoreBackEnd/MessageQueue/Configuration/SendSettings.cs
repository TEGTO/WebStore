namespace MessageQueue.Configuration
{
    public class SendSettings
    {
        public required ExchangeSettings ExchangeSettings { get; set; }
        public QueueSettings? QueueSettings { get; set; }
        public required string RoutingKey { get; set; }
    }
}