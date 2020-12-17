using Newtonsoft.Json;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace RabbitMQ.Producer
{
    static class FanoutExchangePublisher
    {
        public static void Publish(IModel channel, string json)
        {
            var ttl = new Dictionary<string, object>
            {
                {"x-message-ttl", 30000 }
            };
            channel.ExchangeDeclare("demo-fanout-exchange", ExchangeType.Fanout, arguments: ttl);

            var body = Encoding.UTF8.GetBytes(json);

            var properties = channel.CreateBasicProperties();
            properties.Headers = new Dictionary<string, object> { { "rabbitmq", "json_message" } };

            channel.BasicPublish("demo-fanout-exchange", "account.new", properties, body);
        }
    }
}
