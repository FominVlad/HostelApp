﻿using Hostel.gRPCService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hostel.BookingClient.Models
{
    public class IndexModel
    {
        public List<Room> Rooms { get; set; }
        public List<Resident> Residents { get; set; }
        public List<RoomResident> RoomResidents { get; set; }
    }
}
