using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Hostel.BookingClient.Models;
using Hostel.gRPCService;

namespace Hostel.BookingClient.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private ClientGRPC ClientGRPC { get; set; }

        public HomeController(ILogger<HomeController> logger, ClientGRPC clientGRPC)
        {
            _logger = logger;
            this.ClientGRPC = clientGRPC;
        }

        public IActionResult Index()
        {
            List<Models.Room> rooms = new List<Models.Room>();
            foreach (gRPCService.Room room in ClientGRPC.Client.GetRooms(new RoomRequest()).Rooms)
            {
                rooms.Add(new Models.Room() { Id = room.Id, MaxResidentsCount = room.MaxResidentsCount, Floor = room.Floor, Number = room.Number });
            }
            return View(rooms);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        //[HttpPost]
        //async public Task<IActionResult> Index(string message)
        //{
        //    Test.RequestMessage = message;

        //    //var reply = await Test.ClientGRPC.Client.SayHelloAsync(new HelloRequest { Name = message });

        //    //Test.ReplyMessage = reply.Message;

        //    return View(Test);
        //}

        [HttpGet]
        public IActionResult Aasd(string message)
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
