using Microsoft.EntityFrameworkCore;
using OfficeManager.Shared.Entities;

namespace CompaniesServiceApi.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {

        }

        public virtual DbSet<Company> Company { get; set; }
        public virtual DbSet<Office> Office { get; set; }
        public virtual DbSet<Room> Room { get; set; }
        public virtual DbSet<Facility> Facility { get; set; }
        public virtual DbSet<RoomFacility> RoomFacility { get; set; }
        public virtual DbSet<Review> Review { get; set; }
        public virtual DbSet<Seat> Seat { get; set; }
        public virtual DbSet<LocationModel> Location { get; set; }
        public virtual DbSet<Image> Image { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
        }

        public async override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            var entries = ChangeTracker
                .Entries()
                .Where(e => e.Entity is BaseEntity && (
                    e.State == EntityState.Added
                    || e.State == EntityState.Modified));

            foreach (var entityEntry in entries)
            {
                ((BaseEntity)entityEntry.Entity).ModifiedAt = DateTime.Now;

                if (entityEntry.State == EntityState.Added)
                {
                    ((BaseEntity)entityEntry.Entity).CreatedAt = DateTime.Now;
                }
            }

            return await base.SaveChangesAsync(cancellationToken);
        }
    }
}




