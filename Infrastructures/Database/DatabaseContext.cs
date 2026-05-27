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
    }
}
