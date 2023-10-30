using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using OfficeManager.Shared.Entities;

namespace CompaniesServiceApi.Data.Configurations
{
    public class LocationConfig : IEntityTypeConfiguration<LocationModel>
    {
        public void Configure(EntityTypeBuilder<LocationModel> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Country)
                .HasMaxLength(24);

            builder.Property(x => x.City)
                .HasMaxLength(24);
        }
    }
}