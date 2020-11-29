using Grpc.Net.Client;
using Hostel.gRPCService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hostel.BookingClient
{
    public class ClientGRPC
    {
        public GrpcChannel Channel { get; set; }
        public RoomProvider.RoomProviderClient Client { get; set; }


        public ClientGRPC()
        {
            Channel = GrpcChannel.ForAddress("https://localhost:5001");
            Client = new RoomProvider.RoomProviderClient(Channel);
        }
    }
}
