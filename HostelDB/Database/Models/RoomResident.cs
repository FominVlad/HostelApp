using System;

namespace HostelDB.Database.Models
{
    public class RoomResident
    {
        /// <summary>
        /// Room and resident connection unique identifier.
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Room unique identifier.
        /// </summary>
        public int RoomId { get; set; }
        /// <summary>
        /// Resident unique identifier.
        /// </summary>
        public int ResidentId { get; set; }
        /// <summary>
        /// Resident's settle date.
        /// </summary>
        public DateTime SettleDate { get; set; }
        /// <summary>
        /// Resident's evicting date.
        /// </summary>
        public DateTime EvictDate { get; set; }

        public Room Room { get; set; }
        public Resident Resident { get; set; }
    }
}
