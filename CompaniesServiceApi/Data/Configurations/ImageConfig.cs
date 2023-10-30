using OfficeManager.Shared.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CompaniesServiceApi.Data.Configurations
{
    public class ImageConfig : IEntityTypeConfiguration<Image>
    {
        public void Configure(EntityTypeBuilder<Image> builder)
        {
            builder.HasKey(x => x.Id);

            builder.HasOne(s => s.Company)
                .WithOne(ad => ad.Image)
                .HasForeignKey<Company>(ad => ad.ImageId);

            builder.HasOne(s => s.Office)
                .WithOne(ad => ad.Image)
                .HasForeignKey<Office>(ad => ad.ImageId);

            builder.HasOne(s => s.Facility)
                .WithOne(ad => ad.Image)
                .HasForeignKey<Facility>(ad => ad.ImageId);

            builder.HasOne(s => s.Room)
                .WithMany(g => g.Images)
                .HasForeignKey(s => s.RoomId);

            builder.HasOne(x => x.Office)
                .WithOne(x => x.Image)
                .HasForeignKey<Office>(x => x.ImageId)
                .OnDelete(DeleteBehavior.ClientCascade);
        }
    }
}
