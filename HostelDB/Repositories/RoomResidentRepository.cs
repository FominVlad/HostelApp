using HostelDB.Database;
using HostelDB.Database.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HostelDB.Repositories
{
    public class RoomResidentRepository : IRepository<RoomResident>
    {
        private HostelDbContext DbContext { get; set; }
        public RoomResidentRepository(HostelDbContext dbContext)
        {
            this.DbContext = dbContext;
        }
        public void Create(RoomResident item)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public RoomResident GetObject(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<RoomResident> GetObjectList()
        {
            throw new NotImplementedException();
        }

        public void Save()
        {
            throw new NotImplementedException();
        }

        public void Update(RoomResident item)
        {
            throw new NotImplementedException();
        }
    }
}
