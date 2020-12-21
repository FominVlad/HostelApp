using RabbitMQ.Client;
using RabbitMQ.Consumer;
using System;
using System.Collections.Generic;

namespace Hostel.MessageListener
{
    class Program
    {
        static void Main(string[] args)
        {
            var factoryConsumer = new ConnectionFactory
            {
                Uri = new Uri("amqp://guest:guest@localhost:5672")
            };
            using var connectionConsumer = factoryConsumer.CreateConnection();
            using var channelConsumer = connectionConsumer.CreateModel();

            channelConsumer.ExchangeDeclare("demo-direct-exchange", ExchangeType.Direct);
            channelConsumer.QueueDeclare("demo-direct-queue",
                durable: true,
                exclusive: false,
                autoDelete: false,
                arguments: null);
            channelConsumer.QueueBind("demo-direct-queue", "demo-direct-exchange", "account.init");
            channelConsumer.BasicQos(0, 10, false);

            var factoryProducer = new ConnectionFactory
            {
                Uri = new Uri("amqp://guest:guest@localhost:5672")
            };
            using var connectionProducer = factoryProducer.CreateConnection();
            using var channelProducer = connectionProducer.CreateModel();

            var ttl = new Dictionary<string, object>
            {
                {"x-message-ttl", 30000 }
            };
            channelProducer.ExchangeDeclare("demo-direct-exchange2", ExchangeType.Direct, arguments: ttl);

            FanoutExchangeConsumer.Consume(channelConsumer, channelProducer);
        }
    }
}
