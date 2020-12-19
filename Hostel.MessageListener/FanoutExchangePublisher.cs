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
            var body = Encoding.UTF8.GetBytes(json);

            channel.BasicPublish("demo-direct-exchange2", "account.init", null, body);
        }
    }
}
