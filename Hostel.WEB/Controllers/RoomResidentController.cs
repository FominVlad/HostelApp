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
    public class RoomResidentController : ControllerBase
    {
        private HostelUnitOfWork UnitOfWork { get; set; }

        public RoomResidentController(HostelUnitOfWork unitOfWork)
        {
            this.UnitOfWork = unitOfWork;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public IActionResult Get(int? id)
        {
            if (id == null)
            {
                List<RoomResident> result = UnitOfWork.RoomResidents.GetObjectList().ToList();

                if (result == null)
                    return NoContent();
                else
                    return Ok(result);
            }
            else
            {
                RoomResident result = UnitOfWork.RoomResidents.GetObject((int)id);

                if (result == null)
                    return NoContent();
                else
                    return Ok(result);
            }
        }
    }
}
