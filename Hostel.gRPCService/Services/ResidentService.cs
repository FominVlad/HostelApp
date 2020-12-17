using Grpc.Core;
using HostelDB.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hostel.gRPCService.Services
{
    public class ResidentService : ResidentProvider.ResidentProviderBase
    {
        private HostelUnitOfWork UnitOfWork { get; set; }
        public ResidentService(HostelUnitOfWork unitOfWork)
        {
            this.UnitOfWork = unitOfWork;
        }

        public override Task<ResidentGetReply> GetResidents(ResidentGetRequest request, ServerCallContext context)
        {
            ResidentGetReply residentReply = new ResidentGetReply();

            foreach (HostelDB.Database.Models.Resident resident in UnitOfWork.Residents.GetObjectList())
            {
                residentReply.Residents.Add(new ResidentGet()
                {
                    Id = resident.Id,
                    Surname = resident.Surname,
                    Name = resident.Name,
                    Patronymic = resident.Patronymic,
                    Birthday = resident.Birthday.ToString()
                });
            }

            return Task.FromResult(residentReply);
        }
    }
}
