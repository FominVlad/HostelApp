using HostelDB.Database.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HostelDB.Database.EntityConfigurations
{
    public class RoomResidentConfiguration : IEntityTypeConfiguration<RoomResident>
    {
        public void Configure(EntityTypeBuilder<RoomResident> builder)
        {
            builder.HasKey(rr => rr.Id).HasName("PK_RoomResidents_Id");

            builder.HasOne(rr => rr.Room)
                .WithMany(room => room.RoomResidents)
                .HasForeignKey(rr => rr.RoomId)
                .HasConstraintName("FK_RoomResidents_RoomId_Rooms_Id");

            builder.HasOne(rr => rr.Resident)
                .WithMany(resident => resident.RoomResidents)
                .HasForeignKey(rr => rr.ResidentId)
                .HasConstraintName("FK_RoomResidents_ResidentId_Residents_Id");

            builder.HasIndex(rr => rr.Id).HasName("I_RoomResidents_Id");
            builder.HasIndex(rr => rr.SettleDate).HasName("I_RoomResidents_SettleDate");
            builder.HasIndex(rr => rr.EvictDate).HasName("I_RoomResidents_EvictDate");

            builder.Property(rr => rr.RoomId).IsRequired()
                .HasComment("Room unique identifier.");
            builder.Property(rr => rr.ResidentId).IsRequired()
                .HasComment("Resident unique identifier.");
            builder.Property(rr => rr.SettleDate).IsRequired()
                .HasComment("Resident's settle date.");
            builder.Property(rr => rr.EvictDate).IsRequired()
                .HasComment("Resident's evicting date.");
            builder.Property(rr => rr.Id)
                .HasComment("Room and resident connection unique identifier.");
        }
    }
}
