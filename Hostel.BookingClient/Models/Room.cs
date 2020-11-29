using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hostel.BookingClient.Models
{
    public class Room
    {
        public int Id { get; set; }
        public int MaxResidentsCount { get; set; }
        public int Floor { get; set; }
        public int Number { get; set; }
    }
}
