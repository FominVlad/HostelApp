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
        public void Create(Resident item)
        {
            DbContext.Residents.Add(item);
        }

        public void Delete(int id)
        {
            Resident resident = DbContext.Residents.Find(id);
            if (resident != null)
                DbContext.Residents.Remove(resident);
        }

        public Resident GetObject(int id)
        {
            return DbContext.Residents.Find(id);
        }

        public IEnumerable<Resident> GetObjectList()
        {
            return DbContext.Residents.AsEnumerable();
        }

        public void Update(Resident item)
        {
            DbContext.Entry(item).State = EntityState.Modified;
        }
    }
}
