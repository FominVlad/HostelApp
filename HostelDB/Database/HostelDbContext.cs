using HostelDB.Database.EntityConfigurations;
using HostelDB.Database.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HostelDB.Database
{
    public class HostelDbContext : DbContext
    {
        //public HostelDbContext(DbContextOptions<HostelDbContext> options) : 
        //    base(options) { }

        public DbSet<Room> Rooms { get; set; }
        public DbSet<Resident> Residents { get; set; }
        public DbSet<RoomResident> RoomResidents { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new RoomConfiguration());
            modelBuilder.ApplyConfiguration(new ResidentConfiguration());
            modelBuilder.ApplyConfiguration(new RoomResidentConfiguration());
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=helloappdb;Trusted_Connection=True;");
        }
    }
}
