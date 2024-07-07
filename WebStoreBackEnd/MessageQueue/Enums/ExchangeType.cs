namespace MessageQueue.Enums
{
    public enum ExchangeType
    {
        Fanout, Direct, Topic, Headers
    }
    internal static class ExchangeTypeMethods
    {
        public static string GetExchangeTypeFromEnum(ExchangeType type)
        {
            switch (type)
            {
                case ExchangeType.Fanout:
                    return RabbitMQ.Client.ExchangeType.Fanout;
                case ExchangeType.Direct:
                    return RabbitMQ.Client.ExchangeType.Direct;
                case ExchangeType.Topic:
                    return RabbitMQ.Client.ExchangeType.Topic;
                case ExchangeType.Headers:
                    return RabbitMQ.Client.ExchangeType.Headers;
                default:
                    return string.Empty;
            }
        }
    }
}
