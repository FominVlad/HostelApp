using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HostelDB.Database.Models
{
    public class Resident
    {
        /// <summary>
        /// Resident unique identifier.
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Resident surname.
        /// </summary>
        public string Surname { get; set; }
        /// <summary>
        /// Resident name.
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Resident patronymic.
        /// </summary>
        public string Patronymic { get; set; }
        /// <summary>
        /// Resident birthday.
        /// </summary>
        public DateTime Birthday { get; set; }

        public List<RoomResident> RoomResidents { get; set; }
    }
}
