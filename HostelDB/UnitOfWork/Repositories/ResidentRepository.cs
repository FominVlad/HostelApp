using HostelDB.Database;
using HostelDB.Database.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace HostelDB.Repositories
{
    public class ResidentRepository : IRepository<Resident>
    {
        private HostelDbContext DbContext { get; set; }
        public ResidentRepository(HostelDbContext dbContext)
        {
            this.DbContext = dbContext;
        }
        public Resident Create(Resident item)
        {
            DbContext.Residents.Add(item);
            DbContext.SaveChanges();
            return item;
        }

        public bool Delete(int id)
        {
            Resident resident = DbContext.Residents.Find(id);
            if (resident != null)
            {
                DbContext.Residents.Remove(resident);
                return true;
            }
            return false;
        }

        public Resident GetObject(int id)
        {
            return DbContext.Residents.Find(id);
        }

        public IEnumerable<Resident> GetObjectList()
        {
            return DbContext.Residents.AsEnumerable();
        }

        public bool Update(Resident item)
        {
            Resident residentToUpdate = DbContext.Residents.Find(item.Id);
            if (residentToUpdate == null)
                return false;

            DbContext.Entry(item).State = EntityState.Modified;
            return true;
        }
    }
}
