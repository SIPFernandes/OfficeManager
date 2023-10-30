using OfficeManager.Shared.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CompaniesServiceApi.Shared.Configurations
{
    public class FacilityConfig : IEntityTypeConfiguration<Facility>
    {
        public void Configure(EntityTypeBuilder<Facility> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Name)
                .HasMaxLength(50);

            builder.HasOne(s => s.Image)
                .WithOne(ad => ad.Facility)
                .HasForeignKey<Facility>(ad => ad.ImageId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}