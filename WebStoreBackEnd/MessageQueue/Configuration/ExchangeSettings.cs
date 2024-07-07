using MessageQueue.Enums;

namespace MessageQueue.Configuration
{
    public class ExchangeSettings
    {
        public required string Name { get; set; }
        public ExchangeType Type { get; set; } = ExchangeType.Direct;
        public bool IsDurable { get; set; }
        public bool IsAutoDelete { get; set; }
        public IDictionary<string, object>? Arguments { get; set; }
    }
}
