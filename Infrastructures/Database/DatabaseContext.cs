using Microsoft.EntityFrameworkCore;
using WarehouseSystemTest.Models;

namespace WarehouseSystemTest.Infrastructure.Database
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options)
            : base(options)
        {
        }

        public DbSet<Warehouse> Warehouse { get; set; } = null!;
        public DbSet<WarehouseLocation> WarehouseLocation { get; set; } = null!;
        public DbSet<Product> Product { get; set; } = null;
        public DbSet<ProductCategory> ProductCategory { get; set; } = null;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Warehouse>(entity =>
            {
                entity.Property(warehouse => warehouse.Name).IsRequired().HasMaxLength(150);
                entity.Property(warehouse => warehouse.Address).IsRequired().HasMaxLength(300);
                entity.Property(warehouse => warehouse.City).IsRequired().HasMaxLength(100);
            });

            modelBuilder.Entity<WarehouseLocation>(entity =>
            {
                entity.Property(location => location.Code).IsRequired().HasMaxLength(50);
                entity.Property(location => location.Zone).IsRequired().HasMaxLength(50);
                entity.Property(location => location.Rack).IsRequired().HasMaxLength(50);
                entity.Property(location => location.Bin).IsRequired().HasMaxLength(50);

                entity.HasIndex(location => new { location.WarehouseId, location.Code }).IsUnique();

                entity.HasOne(location => location.Warehouse)
                    .WithMany(warehouse => warehouse.WarehouseLocations)
                    .HasForeignKey(location => location.WarehouseId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.HasOne(p => p.ProductCategory)
                    .WithMany(c => c.Products)
                    .HasForeignKey(p => p.ProductCategoryId)
                    .OnDelete(DeleteBehavior.Restrict);
            });
        }
    }
}
