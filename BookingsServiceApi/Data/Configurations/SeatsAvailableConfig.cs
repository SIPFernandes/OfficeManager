using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OfficeManager.Shared.Entities;

namespace BookingsServiceApi.Data.Configurations
{
    public class SeatsAvailableConfig : IEntityTypeConfiguration<SeatsAvailable>
    {
        public void Configure(EntityTypeBuilder<SeatsAvailable> builder)
        {
            builder.HasKey(x => x.Id);
        }
    }
}
