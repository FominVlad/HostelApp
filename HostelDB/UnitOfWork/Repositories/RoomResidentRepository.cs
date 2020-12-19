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
        public RoomResident Create(RoomResident item)
        {
            DbContext.RoomResidents.Add(item);
            DbContext.SaveChanges();
            return item;
        }

        public bool Delete(int id)
        {
            RoomResident roomResident = DbContext.RoomResidents.Find(id);
            if (roomResident != null)
            {
                DbContext.Remove(roomResident);
                return true;
            }
            return false;
        }

        public RoomResident GetObject(int id)
        {
            return DbContext.RoomResidents.Find(id);
        }

        public IEnumerable<RoomResident> GetObjectList()
        {
            return DbContext.RoomResidents.AsEnumerable();
        }

        public bool Update(RoomResident item)
        {
            RoomResident roomResidentToUpdate = DbContext.RoomResidents.Find(item.Id);
            if (roomResidentToUpdate == null)
                return false;

            DbContext.Entry(item).State = EntityState.Modified;
            return true;
        }
    }
}
