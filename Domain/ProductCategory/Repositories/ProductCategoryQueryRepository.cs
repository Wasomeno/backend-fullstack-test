using WarehouseSystemTest.Infrastructure.Database;
using WarehouseSystemTest.Infrastructure.Repositories;

namespace WarehouseSystemTest.Domain.ProductCategory.Repositories
{
    public class ProductCategoryQueryRepository
    {
        private readonly DatabaseContext _context;

        public ProductCategoryQueryRepository(DatabaseContext context)
        {
            _context = context;
        }

        public async Task<PaginationResult<Models.ProductCategory>> Pagination()
        {
            var query = _context.ProductCategory.AsQueryable();
            var count = query.Count();
            var data = query.ToList();

            return new PaginationResult<Models.ProductCategory> { Data = data, Count = count };
        }

        public async Task<Models.ProductCategory?> FindOneById(Guid id) =>
            _context.ProductCategory.FirstOrDefault(c => c.Id == id);
    }
}
