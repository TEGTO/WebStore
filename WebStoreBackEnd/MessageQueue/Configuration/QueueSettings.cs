namespace MessageQueue.Configuration
{
    public class QueueSettings
    {
        public required string Name { get; set; }
        public bool IsDurable { get; set; }
        public bool IsAutoDelete { get; set; }
        public bool IsExclusive { get; set; }
        public IDictionary<string, object>? Arguments { get; set; }
    }
}
