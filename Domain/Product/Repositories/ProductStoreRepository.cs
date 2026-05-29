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
            return created.Entity;
        }
    }

}
