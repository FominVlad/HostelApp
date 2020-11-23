using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HostelDB.Database.Models
{
    public class Room
    {
        /// <summary>
        /// Room unique identifier.
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Maximum number of residents in a room.
        /// </summary>
        public int MaxResidentsCount { get; set; }
        /// <summary>
        /// Floor number.
        /// </summary>
        public int Floor { get; set; }
        /// <summary>
        /// Room number.
        /// </summary>
        public int Number { get; set; }

        public List<RoomResident> RoomResidents { get; set; }
    }
}
