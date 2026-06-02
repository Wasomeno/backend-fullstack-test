using WarehouseSystemTest.Infrastructure.Database;
using WarehouseSystemTest.Infrastructure.Repositories;

namespace WarehouseSystemTest.Domain.Product.Repositories
{

    public class ProductStoreRepository
    {
        private readonly DatabaseContext _context;

        public ProductStoreRepository(DatabaseContext context)
        {
            _context = context;
        }

        public async Task<WarehouseSystemTest.Models.Product> Create(WarehouseSystemTest.Models.Product payload)
        {
            var created = _context.Add(payload);
            await _context.SaveChangesAsync();
            await _context.Entry(created.Entity).Reference(p => p.ProductCategory).LoadAsync();
            return created.Entity;
        }

        public async Task<WarehouseSystemTest.Models.Product> Update(WarehouseSystemTest.Models.Product payload)
        {
            var created = _context.Update(payload);
            await _context.SaveChangesAsync();
            await _context.Entry(created.Entity).Reference(p => p.ProductCategory).LoadAsync();
            return created.Entity;
        }
    }

}
