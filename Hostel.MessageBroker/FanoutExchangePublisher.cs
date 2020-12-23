using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RabbitMQ.Consumer;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

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
