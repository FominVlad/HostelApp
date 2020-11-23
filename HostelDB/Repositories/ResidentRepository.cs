using HostelDB.Database;
using HostelDB.Database.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Resident GetObject(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Resident> GetObjectList()
        {
            throw new NotImplementedException();
        }

        public void Save()
        {
            throw new NotImplementedException();
        }

        public void Update(Resident item)
        {
            throw new NotImplementedException();
        }
    }
}
