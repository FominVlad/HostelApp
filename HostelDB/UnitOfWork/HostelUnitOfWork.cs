using HostelDB.Database;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HostelDB.Repositories
{
    public class HostelUnitOfWork : IDisposable
    {
        private HostelDbContext DbContext { get; set; }
        private RoomRepository RoomRepository { get; set; }
        private ResidentRepository ResidentRepository { get; set; }
        private RoomResidentRepository RoomResidentRepository { get; set; }
        private bool Disposed { get; set; }

        public HostelUnitOfWork()
        {
            this.DbContext = new HostelDbContext();
            Disposed = false;
        }

        public RoomRepository Rooms
        {
            get
            {
                if (RoomRepository == null)
                    RoomRepository = new RoomRepository(DbContext);
                return this.RoomRepository;
            }
        }

        public ResidentRepository Residents
        {
            get
            {
                if (ResidentRepository == null)
                    ResidentRepository = new ResidentRepository(DbContext);
                return this.ResidentRepository;
            }
        }

        public RoomResidentRepository RoomResidents
        {
            get
            {
                if (RoomResidentRepository == null)
                    RoomResidentRepository = new RoomResidentRepository(DbContext);
                return RoomResidentRepository;
            }
        }

        public void Dispose()
        {
            if (!this.Disposed)
            {
                DbContext.Dispose();
                GC.SuppressFinalize(this);
            }

            this.Disposed = true;
        }

        public void Save()
        {
            DbContext.SaveChanges();
        }
    }
}
