using RabbitMQ.Configuration;

namespace RabbitMQ.RabbitMQ
{
    public interface IRabitMQProducer
    {
        public void SendMessage<T>(T message, RabbitMQSendSettings sendSettings);
    }
}
