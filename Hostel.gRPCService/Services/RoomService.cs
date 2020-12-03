using Grpc.Core;
using HostelDB.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hostel.gRPCService.Services
{
    public class RoomService : RoomProvider.RoomProviderBase
    {
        private HostelUnitOfWork UnitOfWork { get; set; }
        public RoomService(HostelUnitOfWork unitOfWork)
        {
            this.UnitOfWork = unitOfWork;
        }

        public override Task<RoomReply> GetRooms(RoomRequest request, ServerCallContext context)
        {
            RoomReply roomReply = new RoomReply();

            foreach (HostelDB.Database.Models.Room room in UnitOfWork.Rooms.GetObjectList())
            {
                roomReply.Rooms.Add(new Room()
                {
                    Id = room.Id,
                    MaxResidentsCount = room.MaxResidentsCount,
                    Floor = room.Floor,
                    Number = room.Number
                });
            }
            
            return Task.FromResult(roomReply);
        }
    }
}
