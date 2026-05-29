using WarehouseSystemTest.Infrastructure.Database;
using WarehouseSystemTest.Infrastructure.Repositories;

namespace WarehouseSystemTest.Domain.Warehouse.Repositories
{
    public class WarehouseQueryRepository
    {
        private readonly DatabaseContext _context;

        public WarehouseQueryRepository(DatabaseContext context)
        {
            _context = context;
        }

        public async Task<PaginationResult<WarehouseSystemTest.Models.Warehouse>> Pagination()
        {
            var query = _context.Warehouse.AsQueryable();
            var count = query.Count();

            var data = query.ToList();

            return new PaginationResult<WarehouseSystemTest.Models.Warehouse> { Data = data, Count = count };
        }
    }

}
