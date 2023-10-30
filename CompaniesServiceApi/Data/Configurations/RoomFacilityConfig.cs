using OfficeManager.Shared.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CompaniesServiceApi.Shared.Configurations
{
    public class RoomFacilityConfig : IEntityTypeConfiguration<RoomFacility>
    {
        public void Configure(EntityTypeBuilder<RoomFacility> builder)
        {
            builder.HasKey(s => new { s.RoomId, s.FacilityId });

            builder.HasOne<Room>(s => s.Room)
                .WithMany(s => s.RoomFacilities)
                .HasForeignKey(s => s.RoomId);

            builder.HasOne<Facility>(s => s.Facility)
                .WithMany(s => s.RoomFacilities)
                .HasForeignKey(s => s.FacilityId);
        }
    }
}
