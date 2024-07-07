namespace MessageQueue.Configuration
{
    public class ReceiveSettings
    {
        public required ExchangeSettings ExchangeSettings { get; set; }
        public required QueueSettings QueueSettings { get; set; }
        public required string RoutingKey { get; set; }
        public uint PrefetchSize { get; set; }
        public ushort PrefetchCount { get; set; } = 1;
        public bool Global { get; set; }
    }
}