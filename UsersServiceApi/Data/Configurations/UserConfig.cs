using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using OfficeManager.Shared.Entities;

namespace UsersServiceApi.Data.Configurations
{
    public class UserConfig : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(x => x.Email)
                .IsRequired()
                .HasMaxLength(100);

            builder.HasIndex(u => u.Email)
                .IsUnique();

            builder.Property(x => x.PhoneNumber)
                .IsRequired()
                .HasMaxLength(15);

            builder.Property(x => x.Position)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(x => x.Image)
                .HasMaxLength(200);

            builder.HasOne<Role>(s => s.Role)
                .WithMany(g => g.Users)
                .HasForeignKey(s => s.RoleId);
        }
    }
}
