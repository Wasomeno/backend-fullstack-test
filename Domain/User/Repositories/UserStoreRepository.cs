using WarehouseSystemTest.Infrastructure.Database;

namespace WarehouseSystemTest.Domain.User.Repositories
{
    public class UserStoreRepository
    {
        private readonly DatabaseContext _context;

        public UserStoreRepository(DatabaseContext context)
        {
            _context = context;
        }

        public async Task<Models.User> Create(Models.User payload)
        {
            var created = _context.User.Add(payload);
            await _context.SaveChangesAsync();
            return created.Entity;
        }
    }
}
