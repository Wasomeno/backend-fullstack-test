using Microsoft.EntityFrameworkCore;
using WarehouseSystemTest.Infrastructure.Database;

namespace WarehouseSystemTest.Domain.User.Repositories
{
    public class UserQueryRepository
    {
        private readonly DatabaseContext _context;

        public UserQueryRepository(DatabaseContext context)
        {
            _context = context;
        }

        public async Task<Models.User?> FindOneById(Guid id)
        {
            return await _context.User.FirstOrDefaultAsync(user => user.Id == id);
        }

        public async Task<Models.User?> FindOneByEmail(string email)
        {
            return await _context.User.FirstOrDefaultAsync(user => user.Email == email);
        }

        public async Task<bool> IsEmailExists(string email)
        {
            return await _context.User.AnyAsync(user => user.Email == email);
        }
    }
}
