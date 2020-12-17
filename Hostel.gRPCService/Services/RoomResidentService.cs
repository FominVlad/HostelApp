using Grpc.Core;
using Hostel.gRPCService.Models;
using HostelDB.Repositories;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace Hostel.gRPCService.Services
{
    public class RoomResidentService : RoomResidentProvider.RoomResidentProviderBase
    {
        private HostelUnitOfWork UnitOfWork { get; set; }
        public RoomResidentService(HostelUnitOfWork unitOfWork)
        {
            this.UnitOfWork = unitOfWork;
        }

        public override Task<RoomResidentReply> GetRoomResidents(RoomResidentRequest request, ServerCallContext context)
        {
            RoomResidentReply roomResidentReply = new RoomResidentReply();

            foreach (HostelDB.Database.Models.RoomResident roomResident in UnitOfWork.RoomResidents.GetObjectList())
            {
                roomResidentReply.RoomResidents.Add(new RoomResident()
                {
                    Id = roomResident.Id,
                    RoomId = roomResident.RoomId,
                    ResidentId = roomResident.ResidentId,
                    SettleDate = roomResident.SettleDate.ToString(),
                    EvictDate = roomResident.EvictDate.ToString()
                });
            }

            return Task.FromResult(roomResidentReply);
        }

        public override Task<CreateRoomResidentReply> CreateRoomResident(CreateRoomResidentRequest createRoomResident, ServerCallContext context)
        {
            CreateRoomResidentReply createRoomResidentReply = new CreateRoomResidentReply();

            var client = new RestClient("http://localhost:7771/");
            // client.Authenticator = new HttpBasicAuthenticator(username, password);
            var httpRequest = new RestRequest("");
            string roomResidentJSON = JsonSerializer.Serialize<CreateRoomResidentRequest>(createRoomResident);
            string requestJSON = JsonSerializer.Serialize<RabbitMQJsonModel>(new RabbitMQJsonModel() { Method = "CreateRoomResident", ObjectJSON = roomResidentJSON });
            httpRequest.AddHeader("json", requestJSON);
            var response = client.Post(httpRequest);

            return Task.FromResult(createRoomResidentReply);
        }
    }
}
