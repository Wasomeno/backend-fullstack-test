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

        public DbSet<TodoItem> TodoItems { get; set; } = null!;
        public DbSet<Warehouse> Warehouses { get; set; } = null!;
        public DbSet<WarehouseLocation> WarehouseLocations { get; set; } = null!;
        public DbSet<ProductCategory> ProductCategories { get; set; } = null!;
        public DbSet<Product> Products { get; set; } = null!;
        public DbSet<Supplier> Suppliers { get; set; } = null!;
        public DbSet<StockLevel> StockLevels { get; set; } = null!;
        public DbSet<StockMovement> StockMovements { get; set; } = null!;
        public DbSet<User> Users { get; set; } = null!;

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

            modelBuilder.Entity<ProductCategory>(entity =>
            {
                entity.Property(category => category.Name).IsRequired().HasMaxLength(150);
                entity.Property(category => category.CategoryCode).IsRequired().HasMaxLength(50);
                entity.HasIndex(category => category.CategoryCode).IsUnique();
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.Property(product => product.Name).IsRequired().HasMaxLength(150);
                entity.Property(product => product.Unit).IsRequired().HasMaxLength(50);
                entity.Property(product => product.ProductCode).IsRequired().HasMaxLength(50);
                entity.Property(product => product.Weight).HasPrecision(18, 2);

                entity.HasIndex(product => product.ProductCode).IsUnique();

                entity.HasOne(product => product.ProductCategory)
                    .WithMany(category => category.Products)
                    .HasForeignKey(product => product.ProductCategoryId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<Supplier>(entity =>
            {
                entity.Property(supplier => supplier.Name).IsRequired().HasMaxLength(150);
                entity.Property(supplier => supplier.Address).IsRequired().HasMaxLength(300);
                entity.Property(supplier => supplier.PhoneNumber).IsRequired().HasMaxLength(30);
                entity.Property(supplier => supplier.Email).IsRequired().HasMaxLength(150);
                entity.HasIndex(supplier => supplier.Email).IsUnique();
            });

            modelBuilder.Entity<StockLevel>(entity =>
            {
                entity.Property(stockLevel => stockLevel.Quantity).IsRequired();
                entity.HasIndex(stockLevel => new { stockLevel.WarehouseLocationId, stockLevel.ProductId }).IsUnique();

                entity.HasOne(stockLevel => stockLevel.WarehouseLocation)
                    .WithMany(location => location.StockLevels)
                    .HasForeignKey(stockLevel => stockLevel.WarehouseLocationId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(stockLevel => stockLevel.Product)
                    .WithMany(product => product.StockLevels)
                    .HasForeignKey(stockLevel => stockLevel.ProductId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<StockMovement>(entity =>
            {
                entity.Property(movement => movement.Type).IsRequired().HasMaxLength(30);
                entity.Property(movement => movement.MovementCodeNumber).IsRequired().HasMaxLength(50);
                entity.Property(movement => movement.Quantity).IsRequired();
                entity.Property(movement => movement.Status).IsRequired().HasMaxLength(30);
                entity.Property(movement => movement.MovedAt).IsRequired();
                entity.Property(movement => movement.Notes).HasMaxLength(500);

                entity.HasIndex(movement => movement.MovementCodeNumber).IsUnique();

                entity.HasOne(movement => movement.Supplier)
                    .WithMany(supplier => supplier.StockMovements)
                    .HasForeignKey(movement => movement.SupplierId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(movement => movement.Product)
                    .WithMany(product => product.StockMovements)
                    .HasForeignKey(movement => movement.ProductId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(movement => movement.WarehouseLocation)
                    .WithMany(location => location.StockMovements)
                    .HasForeignKey(movement => movement.WarehouseLocationId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(movement => movement.CreatedBy)
                    .WithMany(user => user.CreatedStockMovements)
                    .HasForeignKey(movement => movement.CreatedById)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(movement => movement.CompletedBy)
                    .WithMany(user => user.CompletedStockMovements)
                    .HasForeignKey(movement => movement.CompletedById)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(movement => movement.WarehouseLocationFrom)
                    .WithMany(location => location.SourceStockMovements)
                    .HasForeignKey(movement => movement.WarehouseLocationFromId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(movement => movement.WarehouseLocationTo)
                    .WithMany(location => location.DestinationStockMovements)
                    .HasForeignKey(movement => movement.WarehouseLocationToId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(user => user.Email).IsRequired().HasMaxLength(150);
                entity.Property(user => user.Name).IsRequired().HasMaxLength(150);
                entity.Property(user => user.Password).IsRequired().HasMaxLength(255);
                entity.Property(user => user.Role).IsRequired().HasMaxLength(50);
                entity.HasIndex(user => user.Email).IsUnique();
            });
        }
    }
}
