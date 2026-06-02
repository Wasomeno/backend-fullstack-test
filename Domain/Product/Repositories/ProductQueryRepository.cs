using Microsoft.EntityFrameworkCore;
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
            var query = _context.Product.Include(p => p.ProductCategory).AsQueryable();
            var count = query.Count();
            var data = query.ToList();

            return new PaginationResult<WarehouseSystemTest.Models.Product> { Data = data, Count = count };
        }

        public async Task<WarehouseSystemTest.Models.Product?> FindOneById(Guid id) =>
            _context.Product.Include(p => p.ProductCategory).FirstOrDefault(p => p.Id == id);
    }
}
