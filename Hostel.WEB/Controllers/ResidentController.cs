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
    public class ResidentController : ControllerBase
    {
        private HostelUnitOfWork UnitOfWork { get; set; }

        public ResidentController(HostelUnitOfWork unitOfWork)
        {
            this.UnitOfWork = unitOfWork;
        }

        /// <summary>
        /// Get information about resident.
        /// </summary>
        /// <param name="id">Unique resident id.</param>
        /// <returns>If id is null - return room list. Otherwise - return room with id.</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public IActionResult Get(int? id)
        {
            if (id == null)
            {
                List<Resident> result = UnitOfWork.Residents.GetObjectList().ToList();

                if (result == null)
                    return NoContent();
                else
                    return Ok(result);
            }
            else
            {
                Resident result = UnitOfWork.Residents.GetObject((int)id);

                if (result == null)
                    return NoContent();
                else
                    return Ok(result);
            }
        }

        /// <summary>
        /// Add information about resident.
        /// </summary>
        /// <param name="resident"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult Create(Resident resident)
        {
            if (resident != null)
            {
                int resultId = UnitOfWork.Residents.Create(resident).Id;
                UnitOfWork.Save();
                return Ok(resultId);
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult Update(Resident resident)
        {
            if (resident != null)
            {
                if (UnitOfWork.Residents.Update(resident))
                {
                    UnitOfWork.Save();
                    return Ok();
                }
                else
                    return NoContent();
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult Delete(int id)
        {
            return null;
        }
    }
}
