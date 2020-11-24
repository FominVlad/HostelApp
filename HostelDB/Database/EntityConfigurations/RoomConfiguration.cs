using HostelDB.Database.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HostelDB.Database.EntityConfigurations
{
    public class RoomConfiguration : IEntityTypeConfiguration<Room>
    {
        public void Configure(EntityTypeBuilder<Room> builder)
        {
            builder.HasKey(r => r.Id).HasName("PK_Rooms_Id");
            builder.HasAlternateKey(r => new { r.Floor, r.Number });

            builder.HasIndex(r => r.Id).HasDatabaseName("I_Rooms_Id");

            builder.Property(r => r.Floor).IsRequired()
                .HasComment("Floor number.");
            builder.Property(r => r.Number).IsRequired()
                .HasComment("Room number.");
            builder.Property(r => r.MaxResidentsCount).IsRequired()
                .HasComment("Maximum number of residents in a room.");
            builder.Property(r => r.Id)
                .HasComment("Room unique identifier.");
        }
    }
}
