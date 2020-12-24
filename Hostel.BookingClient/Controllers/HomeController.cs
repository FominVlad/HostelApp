using System;
using System.Diagnostics;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Hostel.BookingClient.Models;
using Hostel.gRPCService;
using Hostel.BookingClient.Models.DTOs;

namespace Hostel.BookingClient.Controllers
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
            IndexModel.Rooms = ClientGRPC.RoomClient
                               .GetRooms(new RoomRequest()).Rooms.ToList();
            IndexModel.Residents = ClientGRPC.ResidentClient
                                .GetResidents(new ResidentGetRequest()).Residents.ToList();
            IndexModel.RoomResidents = ClientGRPC.RoomResidentClient
                                .GetRoomResidents(new RoomResidentRequest()).RoomResidents.ToList();

            return View(IndexModel);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(int roomId, int residentId, ResidentCreateRequestDTO residentCreateDTO)
        {
            IndexModel.Rooms = ClientGRPC.RoomClient
                                 .GetRooms(new RoomRequest()).Rooms.ToList();
            IndexModel.Residents = ClientGRPC.ResidentClient
                                 .GetResidents(new ResidentGetRequest()).Residents.ToList();
            IndexModel.RoomResidents = ClientGRPC.RoomResidentClient
                                 .GetRoomResidents(new RoomResidentRequest()).RoomResidents.ToList();

            if (residentId != 0)
            {
                var result = ClientGRPC.RoomResidentClient.CreateRoomResident(new CreateRoomResidentRequest() 
                {
                    RoomId = roomId,
                    ResidentId = residentId
                });

                IndexModel.RoomResidents.Add(new RoomResident() 
                { 
                    Id = result.Id, 
                    RoomId = roomId, 
                    ResidentId = residentId, 
                    EvictDate = DateTime.Now.ToString(), 
                    SettleDate = DateTime.Now.ToString() 
                });
            }
            else
            {
                var result = ClientGRPC.RoomResidentClient.CreateRoomCreateResident(new CreateRoomCreateResidentRequest()
                {
                    RoomId = roomId,
                    ResidentId = residentId,
                    Surname = residentCreateDTO.Surname,
                    Name = residentCreateDTO.Name,
                    Patronymic = residentCreateDTO.Patronymic,
                    Birthday = residentCreateDTO.Birthday
                });

                IndexModel.Residents.Add(new ResidentGet() 
                { 
                    Id = result.ResidentId, 
                    Birthday = residentCreateDTO.Birthday, 
                    Surname = residentCreateDTO.Surname, 
                    Name = residentCreateDTO.Name, 
                    Patronymic = residentCreateDTO.Patronymic 
                });
                
                IndexModel.RoomResidents.Add(new RoomResident() 
                { 
                    Id = result.Id, 
                    RoomId = roomId, 
                    ResidentId = result.ResidentId, 
                    EvictDate = DateTime.Now.ToString(), 
                    SettleDate = DateTime.Now.ToString() 
                });
            }

            return View(IndexModel);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel 
            { 
                RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier 
            });
        }
    }
}
