using WarehouseSystemTest.Infrastructure.Database;

namespace WarehouseSystemTest.Domain.ProductCategory.Repositories
{
    public class ProductCategoryStoreRepository
    {
        private readonly DatabaseContext _context;

        public ProductCategoryStoreRepository(DatabaseContext context)
        {
            _context = context;
        }

        public async Task<Models.ProductCategory> Create(Models.ProductCategory payload)
        {
            var created = _context.Add(payload);
            await _context.SaveChangesAsync();
            return created.Entity;
        }

        public async Task<Models.ProductCategory> Update(Models.ProductCategory payload)
        {
            var updated = _context.Update(payload);
            await _context.SaveChangesAsync();
            return updated.Entity;
        }
    }
}
