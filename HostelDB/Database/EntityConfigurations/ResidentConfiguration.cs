using HostelDB.Database.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HostelDB.Database.EntityConfigurations
{
    public class ResidentConfiguration : IEntityTypeConfiguration<Resident>
    {
        public void Configure(EntityTypeBuilder<Resident> builder)
        {
            builder.HasKey(r => r.Id).HasName("PK_Residents_Id");

            builder.HasIndex(r => r.Id)
                .HasName("I_Residents_Id");
            builder.HasIndex(r => new { r.Surname, r.Name, r.Patronymic })
                .HasName("I_Residents_Surname_Name_Patronymic");

            builder.Property(r => r.Surname).IsRequired().HasComment("Resident surname.");
            builder.Property(r => r.Name).IsRequired().HasComment("Resident name.");
            builder.Property(r => r.Patronymic).IsRequired().HasComment("Resident patronymic.");
            builder.Property(r => r.Birthday).IsRequired().HasComment("Resident birthday.");
            builder.Property(r => r.Id).HasComment("Resident unique identifier.");
        }
    }
}
