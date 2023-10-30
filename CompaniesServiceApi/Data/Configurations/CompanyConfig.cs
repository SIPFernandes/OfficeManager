using OfficeManager.Shared.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CompaniesServiceApi.Shared.Configurations
{
    public class CompanyConfig : IEntityTypeConfiguration<Company>
    {
        public void Configure(EntityTypeBuilder<Company> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(x => x.Description)
                .HasMaxLength(500);

            builder.HasOne(s => s.Image)
                .WithOne(ad => ad.Company)
                .HasForeignKey<Company>(ad => ad.ImageId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(s => s.Offices)
                .WithOne(ad => ad.Company)
                .OnDelete(DeleteBehavior.ClientCascade);
        }
    }
}