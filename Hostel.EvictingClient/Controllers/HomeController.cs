using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Hostel.EvictingClient.Models;
using Hostel.gRPCService;

namespace Hostel.EvictingClient.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private ClientGRPC ClientGRPC { get; set; }
        private IndexModel IndexModel { get; set; }

        public HomeController(ILogger<HomeController> logger, ClientGRPC clientGRPC)
        {
            _logger = logger;
            this.ClientGRPC = clientGRPC;
            IndexModel = new IndexModel();
        }

        public IActionResult Index()
        {
            IndexModel.Rooms = ClientGRPC.RoomClient.GetRooms(new RoomRequest()).Rooms.ToList();
            IndexModel.Residents = ClientGRPC.ResidentClient.GetResidents(new ResidentGetRequest()).Residents.ToList();
            IndexModel.RoomResidents = ClientGRPC.RoomResidentClient.GetRoomResidents(new RoomResidentRequest()).RoomResidents.ToList();

            return View(IndexModel);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(int id)
        {
            IndexModel.Rooms = ClientGRPC.RoomClient.GetRooms(new RoomRequest()).Rooms.ToList();
            IndexModel.Residents = ClientGRPC.ResidentClient.GetResidents(new ResidentGetRequest()).Residents.ToList();
            IndexModel.RoomResidents = ClientGRPC.RoomResidentClient.GetRoomResidents(new RoomResidentRequest()).RoomResidents.ToList();

            if (id != 0)
            {
                var result = ClientGRPC.RoomResidentClient.DeleteRoomResident(new DeleteRoomResidentRequest()
                {
                    Id = id
                });

                if (result.Result)
                {
                    IndexModel.RoomResidents.Remove(IndexModel.RoomResidents.Where(rr => rr.Id == id).FirstOrDefault());
                }
            }

            return View(IndexModel);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
