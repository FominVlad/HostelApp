using RabbitMQ.Client;
using RabbitMQ.Producer;
using System;
using System.IO;
using System.Net;

namespace Hostel.MessageBroker
{
    class Program
    {
        static void Main(string[] args)
        {
            var factory = new ConnectionFactory
            {
                Uri = new Uri("amqp://guest:guest@localhost:5672")
            };
            using var connection = factory.CreateConnection();
            using var channel = connection.CreateModel();
            //FanoutExchangePublisher.Publish(channel);

            HttpListener listener = new HttpListener();
            listener.Prefixes.Add("http://localhost:7777/");
            listener.Start();
            Console.WriteLine("Ожидание подключений...");

            while (true)
            {
                HttpListenerContext context = listener.GetContext();/*await listener.GetContextAsync();*/
                HttpListenerRequest request = context.Request;
                var a = context.Request.Headers.GetValues("json").GetValue(0);
                HttpListenerResponse response = context.Response;

                string responseString = "<html><head><meta charset='utf8'></head><body>Привет мир!</body></html>";
                byte[] buffer = System.Text.Encoding.UTF8.GetBytes(responseString);
                response.ContentLength64 = buffer.Length;
                Stream output = response.OutputStream;
                output.Write(buffer, 0, buffer.Length);
                output.Close();
            }
        }
    }
}
