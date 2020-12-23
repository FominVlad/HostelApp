using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;
using System.Threading.Tasks;

namespace RabbitMQ.Consumer
{
    public static class FanoutExchangeConsumer
    {

        public static string Consume(IModel channelConsumer)
        {
            var consumer = new EventingBasicConsumer(channelConsumer);
            BasicGetResult res = null;

            do
            {
                res = channelConsumer.BasicGet("demo-direct-queue2", true);
                if (res != null)
                {
                    var body = res.Body.ToArray();
                    return Encoding.UTF8.GetString(body);
                }

            }
            while (res == null);
            
            return null;
        }
    }
}
