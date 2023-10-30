using OfficeManager.Shared.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CompaniesServiceApi.Shared.Configurations
{
    public class SeatConfig : IEntityTypeConfiguration<Seat>
    {
        public void Configure(EntityTypeBuilder<Seat> builder)
        {
            builder.HasKey(x => x.Id);

            builder.HasOne<Room>(s => s.Room)
                .WithMany(g => g.Seats)
                .HasForeignKey(s => s.RoomId);
        }
    }
}
