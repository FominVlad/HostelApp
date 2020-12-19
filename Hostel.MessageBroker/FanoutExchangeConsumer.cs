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



        /*private static async Task Consumer_Received(object sender, BasicDeliverEventArgs @event)
        {
            //var message = Encoding.UTF8.GetString(@event.Body);

            var body = @event.Body.ToArray();

            var message = Encoding.UTF8.GetString(body);
            Console.WriteLine("Message: " + message);

            Console.WriteLine($"Begin processing {message}");

            await Task.Delay(250);

            Console.WriteLine($"End processing {message}");
        }

        public async static void Consume(IModel channel)
        {
            
            await Task.Run(() => 
            {
            //channel.ExchangeDeclare("demo-fanout-exchange2", ExchangeType.Fanout);
            //channel.QueueDeclare("demo-fanout-queue2",
            //    durable: true,
            //    exclusive: false,
            //    autoDelete: false,
            //    arguments: null);

            //channel.QueueBind("demo-fanout-queue2", "demo-fanout-exchange2", string.Empty);
            //channel.BasicQos(0, 10, false);
            //var cf = new ConnectionFactory
            //{
            //    Uri = new Uri("amqp://guest:guest@localhost:5672"),
            //    DispatchConsumersAsync = true
            //};

                        channel.ExchangeDeclare("demo-fanout-exchange2", ExchangeType.Fanout);
                channel.QueueDeclare("demo-fanout-queue2",
                            durable: true,
                            exclusive: false,
                            autoDelete: false,
                            arguments: null);

                channel.QueueBind("demo-fanout-queue2", "demo-fanout-exchange2", string.Empty);
                channel.BasicQos(0, 10, false);

                //var res = channel.BasicGet("demo-fanout-queue2", true);
                //var a = 1;
                        var consumer = new AsyncEventingBasicConsumer(channel);

                        //consumer.Received += async (o, a) =>
                        //{
                        //    var body = a.Body.ToArray();
                        //    var message = Encoding.UTF8.GetString(body);
                        //    Console.WriteLine("Message: " + message);
                        //    Console.WriteLine("Message Get" + a.DeliveryTag);
                        //    await Task.Yield();
                        //    //consumer.Received += null;
                        //};

                consumer.Received += Consumer_Received;

                 //var consumer = new EventingBasicConsumer(channel);
                 //string consumedMessage
                 //consumer.Received += (sender, e) => {
                 //    var body = e.Body.ToArray();
                 //    var message = Encoding.UTF8.GetString(body);

                //    Console.WriteLine(message);
                //    channel.BasicAck(e.DeliveryTag, false);
                //    //consumer.Received -= this;
                //    //consumer.Received += () => { };
                //    //consumedMessage = message;
                //};

                //consumer.Received += (ch, ea) =>
                //{
                //    //var body = ea.Body.ToArray();
                //    // copy or deserialise the payload
                //    // and process the message
                //    // ...

                //    var body = ea.Body.ToArray();
                //    var message = Encoding.UTF8.GetString(body);
                //    Console.WriteLine(message);

                //    //channel.BasicAck(ea.DeliveryTag, false);
                //    //await Task.Yield();
                //};

                //var consumer = new AsyncEventingBasicConsumer(channel);
                //consumer.Received += Consumer_Received;

                //consumer.Received += (o, a) =>
                //{
                //    var body = a.Body.ToArray();
                //    var message = Encoding.UTF8.GetString(body);
                //    Console.WriteLine(message);
                //    Console.WriteLine("Delivery: " + a.DeliveryTag);
                //    await Task.Yield();
                //};


                //consumer.Received += new Func<string>(() => { return "test"; })();

                //channel.BasicConsume("demo-fanout-queue2", true, consumer);
                Console.WriteLine("Consumer started");
                //Console.ReadLine();

            });
            
        }*/
    }
}
