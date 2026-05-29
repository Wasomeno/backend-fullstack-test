using WarehouseSystemTest.Infrastructure.Database;
using WarehouseSystemTest.Infrastructure.Repositories;

namespace WarehouseSystemTest.Domain.Product.Repositories
{
    public class ProductQueryRepository
    {
        private readonly DatabaseContext _context;

        public ProductQueryRepository(DatabaseContext context)
        {
            _context = context;
        }

        public async Task<PaginationResult<WarehouseSystemTest.Models.Product>> Pagination()
        {
            var query = _context.Product.AsQueryable();
            var count = query.Count();

            var data = query.ToList();

            return new PaginationResult<WarehouseSystemTest.Models.Product> { Data = data, Count = count };
        }
    }

}
