using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RabbitMQ.Consumer;
using RabbitMQ.Producer;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Hostel.MessageBroker
{
    class Program
    {
        //static void Main(string[] args)
        //{
        //    var factory = new ConnectionFactory
        //    {
        //        Uri = new Uri("amqp://guest:guest@localhost:5672")
        //    };
        //    using var connection = factory.CreateConnection();
        //    using var channel = connection.CreateModel();



        //    //FanoutExchangePublisher.Publish(channel);

        //    HttpListener listener = new HttpListener();
        //    listener.Prefixes.Add("http://localhost:7771/");
        //    listener.Start();
        //    Console.WriteLine("Ожидание подключений...");

        //    var cf = new ConnectionFactory
        //    {
        //        Uri = new Uri("amqp://guest:guest@localhost:5672"),
        //        DispatchConsumersAsync = true
        //    };

        //    using var conn = cf.CreateConnection();
        //    using var ch = conn.CreateModel();


        //    Console.WriteLine("START");

        //    while (true)
        //    {
        //        HttpListenerContext context = listener.GetContext();
        //        HttpListenerRequest request = context.Request;
        //        string json = context.Request.Headers.GetValues("json")?.GetValue(0)?.ToString() ?? null;

        //        if(!string.IsNullOrEmpty(json))
        //        FanoutExchangePublisher.Publish(channel, json, ch);
        //        HttpListenerResponse response = context.Response;

        //        response.StatusCode = 200;

        //        string responseString = string.Empty;
        //        byte[] buffer = System.Text.Encoding.UTF8.GetBytes(responseString);
        //        response.ContentLength64 = buffer.Length;
        //        Stream output = response.OutputStream;
        //        output.Write(buffer, 0, buffer.Length);
        //        output.Close();
        //    }
        //}

        static void Main(string[] args)
        {
            var factoryConsumer = new ConnectionFactory
            {
                Uri = new Uri("amqp://guest:guest@localhost:5672")
            };
            using var connectionConsumer = factoryConsumer.CreateConnection();
            using var channelConsumer = connectionConsumer.CreateModel();
            channelConsumer.ExchangeDeclare("demo-direct-exchange2", ExchangeType.Direct);
            channelConsumer.QueueDeclare("demo-direct-queue2",
                durable: true,
                exclusive: false,
                autoDelete: false,
                arguments: null);
            channelConsumer.QueueBind("demo-direct-queue2", "demo-direct-exchange2", "account.init");
            channelConsumer.BasicQos(0, 10, false);

            var factoryPublisher = new ConnectionFactory
            {
                Uri = new Uri("amqp://guest:guest@localhost:5672")
            };
            using var connectionPublisher = factoryPublisher.CreateConnection();
            using var channelPublisher = connectionPublisher.CreateModel();
            var ttl = new Dictionary<string, object>
            {
                {"x-message-ttl", 30000 }
            };
            //channelPublisher.ExchangeDeclare("demo-fanout-exchange", ExchangeType.Fanout, arguments: ttl);
            channelPublisher.ExchangeDeclare("demo-direct-exchange", ExchangeType.Direct, arguments: ttl);

            HttpListener listener = new HttpListener();
            listener.Prefixes.Add("http://localhost:7771/");
            listener.Start();
            Console.WriteLine("Ожидание подключений...");

            while (true)
            {
                HttpListenerContext context = listener.GetContext();
                HttpListenerRequest request = context.Request;
                string json = context.Request.Headers.GetValues("json")?.GetValue(0)?.ToString() ?? null;
                HttpListenerResponse response = context.Response;
                response.StatusCode = 200;
                string responseString = "";

                if (!string.IsNullOrEmpty(json))
                    responseString = FanoutExchangePublisher.Publish(channelPublisher, channelConsumer, json);

                byte[] buffer = System.Text.Encoding.UTF8.GetBytes(responseString);
                response.ContentLength64 = buffer.Length;
                Stream output = response.OutputStream;
                output.Write(buffer, 0, buffer.Length);
                output.Close();
            }
        }
    }
}
