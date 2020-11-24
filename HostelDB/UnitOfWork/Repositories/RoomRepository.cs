using HostelDB.Database;
using HostelDB.Database.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HostelDB.Repositories
{
    public class RoomRepository : IRepository<Room>
    {
        private HostelDbContext DbContext { get; set; }
        public RoomRepository(HostelDbContext dbContext)
        {
            this.DbContext = dbContext;
        }
        public void Create(Room item)
        {
            DbContext.Rooms.Add(item);
        }
        public void Update(Room item)
        {
            DbContext.Entry(item).State = EntityState.Modified;
        }

        public void Delete(int id)
        {
            Room room = DbContext.Rooms.Find(id);
            if (room != null)
                DbContext.Remove(room);
        }

        public Room GetObject(int id)
        {
            return DbContext.Rooms.Find(id);
        }

        public IEnumerable<Room> GetObjectList()
        {
            return DbContext.Rooms.AsEnumerable();
        }
    }
}
