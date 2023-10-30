using OfficeManager.Shared.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CompaniesServiceApi.Shared.Configurations
{
    public class OfficeConfig : IEntityTypeConfiguration<Office>
    {
        public void Configure(EntityTypeBuilder<Office> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(50);

            builder.HasOne<Company>(s => s.Company)
                .WithMany(g => g.Offices)
                .HasForeignKey(s => s.CompanyId);

            builder.HasOne<LocationModel>(s => s.Location)
                .WithMany(g => g.Offices)
                .HasForeignKey(s => s.LocationId);
        }
    }
}
