using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HostelDB.Database.Models;
using HostelDB.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Hostel.WEB.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoomController : ControllerBase
    {
        private HostelUnitOfWork UnitOfWork { get; set; }

        public RoomController(HostelUnitOfWork unitOfWork)
        {
            this.UnitOfWork = unitOfWork;
        }

        /// <summary>
        /// Get information about rooms.
        /// </summary>
        /// <param name="id">Unique room id. (Not required).</param>
        /// <returns>If id is null - return room list. Otherwise - return room with id.</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public IActionResult Get(int? id)
        {
            if (id == null)
            {
                List<Room> result = UnitOfWork.Rooms.GetObjectList().ToList();

                if (result == null)
                    return NoContent();
                else
                    return Ok(result);
            }
            else
            {
                Room result = UnitOfWork.Rooms.GetObject((int)id);

                if (result == null)
                    return NoContent();
                else
                    return Ok(result);
            }
        }
    }
}
