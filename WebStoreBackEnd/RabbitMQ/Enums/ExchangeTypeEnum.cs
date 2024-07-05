using RabbitMQ.Client;

namespace RabbitMQ.Enums
{
    public enum ExchangeTypeEnum
    {
        Fanout, Direct, Topic, Headers
    }
    internal static class ExchangeTypeEnumMethods
    {
        public static string GetExchangeTypeFromEnum(ExchangeTypeEnum type)
        {
            switch (type)
            {
                case ExchangeTypeEnum.Fanout:
                    return ExchangeType.Fanout;
                case ExchangeTypeEnum.Direct:
                    return ExchangeType.Direct;
                case ExchangeTypeEnum.Topic:
                    return ExchangeType.Topic;
                case ExchangeTypeEnum.Headers:
                    return ExchangeType.Headers;
                default:
                    return string.Empty;
            }
        }
    }
}
