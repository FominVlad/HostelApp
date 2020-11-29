using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hostel.BookingClient.Models
{
    public class Test
    {
        public ClientGRPC ClientGRPC { get; set; }
        public string RequestMessage { get; set; }
        public string ReplyMessage { get; set; }

        public Test(ClientGRPC clientGRPC)
        {
            this.ClientGRPC = clientGRPC;
        }
    }
}
