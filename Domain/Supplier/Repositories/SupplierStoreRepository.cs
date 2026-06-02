using WarehouseSystemTest.Infrastructure.Database;

namespace WarehouseSystemTest.Domain.Supplier.Repositories
{
    public class SupplierStoreRepository
    {
        private readonly DatabaseContext _context;

        public SupplierStoreRepository(DatabaseContext context)
        {
            _context = context;
        }

        public async Task<Models.Supplier> Create(Models.Supplier payload)
        {
            var created = _context.Add(payload);
            await _context.SaveChangesAsync();
            return created.Entity;
        }

        public async Task<Models.Supplier> Update(Models.Supplier payload)
        {
            var updated = _context.Update(payload);
            await _context.SaveChangesAsync();
            return updated.Entity;
        }
    }
}
