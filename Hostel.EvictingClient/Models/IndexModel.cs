using Hostel.gRPCService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hostel.EvictingClient.Models
{
    public class IndexModel
    {
        public List<Room> Rooms { get; set; }
        public List<ResidentGet> Residents { get; set; }
        public List<RoomResident> RoomResidents { get; set; }
    }
}
