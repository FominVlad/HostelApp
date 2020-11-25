using HostelDB.Database;
using HostelDB.Database.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace HostelDB.Repositories
{
    public class RoomRepository : IRepository<Room>
    {
        private HostelDbContext DbContext { get; set; }
        public RoomRepository(HostelDbContext dbContext)
        {
            this.DbContext = dbContext;
        }
        public Room Create(Room item)
        {
            DbContext.Rooms.Add(item);
            return item;
        }
        public bool Update(Room item)
        {
            Room roomToUpdate = DbContext.Rooms.Find(item.Id);
            if (roomToUpdate == null)
                return false;

            DbContext.Entry(item).State = EntityState.Modified;
            return true;
        }

        public bool Delete(int id)
        {
            Room room = DbContext.Rooms.Find(id);
            if (room != null)
            {
                DbContext.Remove(room);
                return true;
            }
            return false;
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
