using HostelDB.Database;
using HostelDB.Database.Models;
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
            throw new NotImplementedException();
        }
        public void Update(Room item)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Room GetObject(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Room> GetObjectList()
        {
            throw new NotImplementedException();
        }

        public void Save()
        {
            throw new NotImplementedException();
        }
    }
}
