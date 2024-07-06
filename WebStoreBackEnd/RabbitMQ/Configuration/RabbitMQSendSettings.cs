﻿using RabbitMQ.Enums;

namespace RabbitMQ.Configuration
{
    public class RabbitMQSendSettings
    {
        public required string ExchangeName { get; set; }
        public required string RoutingKey { get; set; }
        public string QueueName { get; set; }
        public ExchangeTypeEnum ExchangeType { get; set; } = ExchangeTypeEnum.Direct;
        public bool DeclareQueue { get; set; } = true;
        public bool IsExchangeDurable { get; set; }
        public bool IsExchangeAutoDelete { get; set; }
        public IDictionary<string, object>? ExchangeArguments { get; set; }
    }
}