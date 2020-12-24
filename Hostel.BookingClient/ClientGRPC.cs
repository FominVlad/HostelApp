using Grpc.Net.Client;
using Hostel.gRPCService;

namespace Hostel.BookingClient
{
    public class ClientGRPC
    {
        public GrpcChannel Channel { get; set; }
        public RoomProvider.RoomProviderClient RoomClient { get; set; }
        public ResidentProvider.ResidentProviderClient ResidentClient { get; set; }
        public RoomResidentProvider.RoomResidentProviderClient RoomResidentClient { get; set; }


        public ClientGRPC()
        {
            Channel = GrpcChannel.ForAddress("https://localhost:5001");
            RoomClient = new RoomProvider.RoomProviderClient(Channel);
            ResidentClient = new ResidentProvider.ResidentProviderClient(Channel);
            RoomResidentClient = new RoomResidentProvider.RoomResidentProviderClient(Channel);
        }
    }
}
