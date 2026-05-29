using WarehouseSystemTest.Infrastructure.Database;
using WarehouseSystemTest.Infrastructure.Repositories;

namespace WarehouseSystemTest.Domain.Warehouse.Repositories
{

    public class WarehouseStoreRepository
    {
        private readonly DatabaseContext _context;

        public WarehouseStoreRepository(DatabaseContext context)
        {
            _context = context;
        }

        public async Task<WarehouseSystemTest.Models.Warehouse> Create(WarehouseSystemTest.Models.Warehouse payload)
        {
            var created = _context.Add(payload);
            await _context.SaveChangesAsync();
            return created.Entity;
        }
    }

}
