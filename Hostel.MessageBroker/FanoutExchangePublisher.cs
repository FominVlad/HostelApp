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
        //public static void Publish(IModel channel, string json, IModel ch)
        //{
        //    var ttl = new Dictionary<string, object>
        //    {
        //        {"x-message-ttl", 30000 }
        //    };
        //    channel.ExchangeDeclare("demo-fanout-exchange", ExchangeType.Fanout, arguments: ttl);

        //    var body = Encoding.UTF8.GetBytes(json);

        //    channel.ConfirmSelect();
        //    channel.BasicAcks += (sender, e) =>
        //    {
        //        //FanoutExchangeConsumer.Consume(ch);
        //        //ch.BasicGet

        //        Console.Write("ACK received");
        //        //var factory2 = new ConnectionFactory
        //        //{
        //        //    Uri = new Uri("amqp://guest:guest@localhost:5672")
        //        //};
        //        //using var connection2 = factory2.CreateConnection();
        //        //using var channel2 = connection2.CreateModel();
        //        //FanoutExchangeConsumer.Consume(channel2);


        //    };

        //    var properties = channel.CreateBasicProperties();
        //    properties.Headers = new Dictionary<string, object> { { "rabbitmq", "json_message" } };

        //    channel.BasicPublish("demo-fanout-exchange", "account.new", properties, body);
        //    //FanoutExchangeConsumer.Consume(channel);
        //}
        public static string Publish(IModel channelPublisher, IModel channelConsumer, string json)
        {
            var body = Encoding.UTF8.GetBytes(json);

            //var properties = channelPublisher.CreateBasicProperties();
            //properties.Headers = new Dictionary<string, object> { { "rabbitmq", "json_message" } };

            channelPublisher.BasicPublish("demo-direct-exchange", "account.init", null, body);
            return FanoutExchangeConsumer.Consume(channelConsumer);
        }
    }
}
