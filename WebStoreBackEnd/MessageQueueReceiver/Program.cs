using MessageQueue.Configuration;
using MessageQueue.Enums;
using MessageQueue.Services;
using System.Text.Json;
using WebStoreApi.Domain.Models;

var rabbitMQFactorySettings = new FactorySettings
{
    HostName = "rabbitmq",
    Port = 5672,
    UserName = "user1",
    Password = "12345",
};
var exchangeSettings = new ExchangeSettings
{
    Name = "webstore-exchange",
    Type = ExchangeType.Topic,
};
var mqService = new MessageQueueService(rabbitMQFactorySettings);

//Queue 'Add product' 
var addingProductQueueSettings = new QueueSettings
{
    Name = "webstore.usercart.addproduct",
};
var addingProductReceiveSettings = new ReceiveSettings
{
    ExchangeSettings = exchangeSettings,
    QueueSettings = addingProductQueueSettings,
    RoutingKey = "webstore.usercart.addproduct",
};
var addingModel = mqService.ReceiveMessage(addingProductReceiveSettings, (message) =>
{
    var jsonDeserialized = JsonSerializer.Deserialize<UserCartChange>(message);
    Console.WriteLine($"Adding a new product to an user cart: {message}");
});
//Queue 'Remove product' 
var removingProductQueueSettings = new QueueSettings
{
    Name = "webstore.usercart.removeproduct",
};
var removingProductReceiveSettings = new ReceiveSettings
{
    ExchangeSettings = exchangeSettings,
    QueueSettings = removingProductQueueSettings,
    RoutingKey = "webstore.usercart.removeproduct",
};
var removingModel = mqService.ReceiveMessage(removingProductReceiveSettings, (message) =>
{
    var jsonDeserialized = JsonSerializer.Deserialize<UserCartChange>(message);
    Console.WriteLine($"Removing a product from an user cart: {message}");
});

//var receiveSettings = new ReceiveSettings
//{
//    ExchangeSettings = exchangeSettings,
//    QueueSettings = queueSettings,
//    RoutingKey = "webstore.usercart.#",
//};
//var mqService = new MessageQueueService(rabbitMQFactorySettings);
//var model = mqService.ReceiveMessage(receiveSettings, (message) =>
//{
//    var jsonDeserialized = JsonSerializer.Deserialize<UserCartChange>(message);
//    Console.WriteLine($"UserCartChange: {message}");
//});
//model.Dispose();

Console.WriteLine($"Receiver is working...");
Console.ReadLine();
addingModel.Dispose();
removingModel.Dispose();
mqService.Dispose();