using RabbitMQ.Client;
using RabbitMQ.Consumer;
using System.Text;

namespace RabbitMQ.Producer
{
    static class FanoutExchangePublisher
    {
        public static string Publish(IModel channelPublisher, IModel channelConsumer, string json)
        {
            var body = Encoding.UTF8.GetBytes(json);

            channelPublisher.BasicPublish("demo-direct-exchange", "account.init", null, body);
            return FanoutExchangeConsumer.Consume(channelConsumer);
        }
    }
}
