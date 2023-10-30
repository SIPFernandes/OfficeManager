using OfficeManager.Shared.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CompaniesServiceApi.Shared.Configurations
{
    public class RoomConfig : IEntityTypeConfiguration<Room>
    {
        public void Configure(EntityTypeBuilder<Room> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Name)
                .HasMaxLength(50);

            builder.Property(x => x.Type)
                .HasMaxLength(24);

            builder.Property(x => x.Description)
                .HasMaxLength(500);

            builder.Property(x => x.Status)
                .HasMaxLength(24);

            builder.HasOne<Office>(s => s.Office)
                .WithMany(g => g.Rooms)
                .HasForeignKey(s => s.OfficeId);

            builder.HasMany(s => s.Images)
                .WithOne(ad => ad.Room)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
