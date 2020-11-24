using HostelDB.Database;
using HostelDB.Database.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

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
            DbContext.RoomResidents.Add(item);
        }

        public void Delete(int id)
        {
            RoomResident roomResident = DbContext.RoomResidents.Find(id);
            if (roomResident != null)
                DbContext.Remove(roomResident);
        }

        public RoomResident GetObject(int id)
        {
            return DbContext.RoomResidents.Find(id);
        }

        public IEnumerable<RoomResident> GetObjectList()
        {
            return DbContext.RoomResidents.AsEnumerable();
        }

        public void Update(RoomResident item)
        {
            DbContext.Entry(item).State = EntityState.Modified;
        }
    }
}
