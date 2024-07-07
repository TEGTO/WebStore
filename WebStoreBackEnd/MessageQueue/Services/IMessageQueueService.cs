using MessageQueue.Configuration;
using RabbitMQ.Client;

namespace MessageQueue.Services
{
    public interface IMessageQueueService
    {
        public void SendMessage<T>(T message, SendSettings sendSettings);
        public IModel ReceiveMessage(ReceiveSettings receiveSettings, Action<string> handleMessage);
    }
}