using RabbitMQ.Configuration;
using RabbitMQ.Enums;
using RabbitMQ.RabbitMQ;
using System.Text.Json;
using WebStoreApi.Domain.Models;

var rabbitMQFactorySettings = new RabbitMQFactorySettings
{
    HostName = "rabbitmq",
    Port = 5672,
    UserName = "user1",
    Password = "12345",
};
var rabbitMQReceiveSettings = new RabbitMQReceiveSettings
{
    ExchangeName = "webstore-exchange",
    RoutingKey = "webstore.usercart.#",
    QueueName = "webstore.usercart",
    ExchangeType = ExchangeTypeEnum.Topic,
};
var rabbitMQProducer = new RabitMQProducer(rabbitMQFactorySettings);
rabbitMQProducer.ReceiveMessage(rabbitMQReceiveSettings, (message) =>
{
    var jsonDeserialized = JsonSerializer.Deserialize<UserCartChange>(message);
    Console.WriteLine($"UserCartChange: {message}");
});
Console.WriteLine($"Receiver is working...");
Console.ReadLine();
rabbitMQProducer.Dispose();
