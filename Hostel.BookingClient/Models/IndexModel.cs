using Hostel.gRPCService;
using System.Collections.Generic;

namespace Hostel.BookingClient.Models
{
    public class IndexModel
    {
        public List<Room> Rooms { get; set; }
        public List<ResidentGet> Residents { get; set; }
        public List<RoomResident> RoomResidents { get; set; }
    }
}
