using HostelDB.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HostelDB.Repositories
{
    public class UnitOfWork : IDisposable
    {
        private HostelDbContext DbContext { get; set; }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
