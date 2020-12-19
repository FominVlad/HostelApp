using Hostel.gRPCService;
using Hostel.gRPCService.Models;
using HostelDB.Repositories;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RabbitMQ.Producer;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace RabbitMQ.Consumer
{
    public static class FanoutExchangeConsumer
    {
        //public static void Consume(IModel channel)
        //{
        //    channel.ExchangeDeclare("demo-fanout-exchange", ExchangeType.Fanout);
        //    channel.QueueDeclare("demo-fanout-queue",
        //        durable: true,
        //        exclusive: false,
        //        autoDelete: false,
        //        arguments: null);


        //    channel.QueueBind("demo-fanout-queue", "demo-fanout-exchange", string.Empty);
        //    channel.BasicQos(0, 10, false);

        //    var consumer = new EventingBasicConsumer(channel);
        //    consumer.Received += (sender, e) => {
        //        var body = e.Body.ToArray();
        //        var message = Encoding.UTF8.GetString(body);
        //        Console.WriteLine(message);

        //        RabbitMQJsonModel jsonObj = JsonSerializer.Deserialize<RabbitMQJsonModel>(message);

        //        HostelUnitOfWork unitOfWork = new HostelUnitOfWork();
        //        switch (jsonObj.Method)
        //        {
        //            case "CreateRoomResident":
        //                {
        //                    CreateRoomResidentRequest request = JsonSerializer.Deserialize<CreateRoomResidentRequest>(jsonObj.ObjectJSON);

        //                    int createdId = unitOfWork.RoomResidents.Create(new HostelDB.Database.Models.RoomResident() 
        //                    { 
        //                        RoomId = request.RoomId,
        //                        ResidentId = request.ResidentId,
        //                        SettleDate = DateTime.Now,
        //                        EvictDate = DateTime.Now
        //                    }).Id;

        //                    channel.BasicAck(e.DeliveryTag, false);


        //                    var factory = new ConnectionFactory
        //                    {
        //                        Uri = new Uri("amqp://guest:guest@localhost:5672")
        //                    };
        //                    using var connection = factory.CreateConnection();
        //                    using var channel2 = connection.CreateModel();

        //                    //FanoutExchangePublisher.Publish(channel2, createdId.ToString());
        //                    break;
        //                }
        //            default:
        //                {
        //                    break;
        //                }
        //        }

        //        unitOfWork.Save();









        //    };

        //    channel.BasicConsume("demo-fanout-queue", true, consumer);
        //    Console.WriteLine("Consumer started");
        //    Console.ReadLine();
        //}
        public static void Consume(IModel channelConsumer, IModel channelProducer)
        {
            var consumer = new EventingBasicConsumer(channelConsumer);
            consumer.Received += (sender, e) => {
                var body = e.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                Console.WriteLine(message);

                RabbitMQJsonModel jsonObj = JsonSerializer.Deserialize<RabbitMQJsonModel>(message);

                HostelUnitOfWork unitOfWork = new HostelUnitOfWork();
                switch (jsonObj.Method)
                {
                    case "CreateRoomResident":
                        {
                            CreateRoomResidentRequest request = JsonSerializer.Deserialize<CreateRoomResidentRequest>(jsonObj.ObjectJSON);

                            int createdId = unitOfWork.RoomResidents.Create(new HostelDB.Database.Models.RoomResident()
                            {
                                RoomId = request.RoomId,
                                ResidentId = request.ResidentId,
                                SettleDate = DateTime.Now,
                                EvictDate = DateTime.Now
                            }).Id;

                            FanoutExchangePublisher.Publish(channelProducer, createdId.ToString());

                            break;
                        }
                    default:
                        {
                            break;
                        }
                }

                unitOfWork.Save();
            };

            channelConsumer.BasicConsume("demo-direct-queue", true, consumer);
            Console.WriteLine("Consumer started");
            Console.ReadLine();
        }
    }
}
