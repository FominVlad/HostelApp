using Grpc.Core;
using HostelDB.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
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
    }
}
