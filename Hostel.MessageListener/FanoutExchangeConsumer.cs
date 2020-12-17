using Hostel.gRPCService;
using Hostel.gRPCService.Models;
using HostelDB.Repositories;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace RabbitMQ.Consumer
{
    public static class FanoutExchangeConsumer
    {
        public static void Consume(IModel channel)
        {
            channel.ExchangeDeclare("demo-fanout-exchange", ExchangeType.Fanout);
            channel.QueueDeclare("demo-fanout-queue",
                durable: true,
                exclusive: false,
                autoDelete: false,
                arguments: null);


            channel.QueueBind("demo-fanout-queue", "demo-fanout-exchange", string.Empty);
            channel.BasicQos(0, 10, false);

            var consumer = new EventingBasicConsumer(channel);
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

                            unitOfWork.RoomResidents.Create(new HostelDB.Database.Models.RoomResident() 
                            { 
                                RoomId = request.RoomId,
                                ResidentId = request.ResidentId,
                                SettleDate = DateTime.Now,
                                EvictDate = DateTime.Now
                            });
                            break;
                        }
                    default:
                        {
                            break;
                        }
                }

                unitOfWork.Save();




                

                



            };

            channel.BasicConsume("demo-fanout-queue", true, consumer);
            Console.WriteLine("Consumer started");
            Console.ReadLine();
        }
    }
}
