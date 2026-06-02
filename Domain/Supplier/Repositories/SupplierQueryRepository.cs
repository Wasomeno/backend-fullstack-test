using WarehouseSystemTest.Infrastructure.Database;
using WarehouseSystemTest.Infrastructure.Repositories;

namespace WarehouseSystemTest.Domain.Supplier.Repositories
{
    public class SupplierQueryRepository
    {
        private readonly DatabaseContext _context;

        public SupplierQueryRepository(DatabaseContext context)
        {
            _context = context;
        }

        public async Task<PaginationResult<Models.Supplier>> Pagination()
        {
            var query = _context.Supplier.AsQueryable();
            var count = query.Count();
            var data = query.ToList();

            return new PaginationResult<Models.Supplier> { Data = data, Count = count };
        }

        public async Task<Models.Supplier?> FindOneById(Guid id) =>
            _context.Supplier.FirstOrDefault(s => s.Id == id);
    }
}
