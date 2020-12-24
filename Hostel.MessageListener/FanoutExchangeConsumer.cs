using Hostel.gRPCService;
using Hostel.gRPCService.Models;
using HostelDB.Database.Models;
using HostelDB.Repositories;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RabbitMQ.Producer;
using System;
using System.Text;
using System.Text.Json;

namespace RabbitMQ.Consumer
{
    public static class FanoutExchangeConsumer
    {
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

                            CreateRoomResidentReply reply = new CreateRoomResidentReply()
                            {
                                Id = createdId
                            };

                            string requestJSON = JsonSerializer.Serialize<CreateRoomResidentReply>(reply);

                            FanoutExchangePublisher.Publish(channelProducer, requestJSON);

                            break;
                        }
                    case "CreateRoomCreateResidentRequest":
                        {
                            CreateRoomCreateResidentRequest request = JsonSerializer.Deserialize<CreateRoomCreateResidentRequest>(jsonObj.ObjectJSON);

                            int createdResidentId = unitOfWork.Residents.Create(new Resident() 
                            {
                                Birthday = Convert.ToDateTime(request.Birthday),
                                Name = request.Name,
                                Surname = request.Surname,
                                Patronymic = request.Patronymic
                            }).Id;

                            int createdId = unitOfWork.RoomResidents.Create(new HostelDB.Database.Models.RoomResident()
                            {
                                RoomId = request.RoomId,
                                ResidentId = createdResidentId,
                                SettleDate = DateTime.Now,
                                EvictDate = DateTime.Now
                            }).Id;

                            CreateRoomCreateResidentReply reply = new CreateRoomCreateResidentReply() 
                            { 
                                Id = createdId,
                                ResidentId = createdResidentId
                            };

                            string requestJSON = JsonSerializer.Serialize<CreateRoomCreateResidentReply>(reply);

                            FanoutExchangePublisher.Publish(channelProducer, requestJSON);

                            break;
                        }
                    case "DeleteRoomResident":
                        {
                            DeleteRoomResidentRequest request = JsonSerializer.Deserialize<DeleteRoomResidentRequest>(jsonObj.ObjectJSON);

                            bool result = unitOfWork.RoomResidents.Delete(request.Id);

                            DeleteRoomResidentReply reply = new DeleteRoomResidentReply()
                            {
                                Result = result
                            };

                            string requestJSON = JsonSerializer.Serialize<DeleteRoomResidentReply>(reply);

                            FanoutExchangePublisher.Publish(channelProducer, requestJSON);

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
