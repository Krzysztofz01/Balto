using Balto.Domain;
using Balto.Repository.Context;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Balto.Repository
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(BaltoDbContext context) : base(context)
        {
        }

        public IEnumerable<User> GetAllUsers()
        {
            return entities
                .Include(u => u.Team);
        }

        public async Task<User> GetSingleUser(long userId)
        {
            return await entities
                .Include(u => u.Team)
                .SingleOrDefaultAsync(u => u.Id == userId);
        }

        public async Task<User> GetUserWithToken(string token)
        {
            return await entities
                .Include(u => u.RefreshTokens)
                .SingleOrDefaultAsync(u => u.RefreshTokens.Any(r => r.Token == token));
        }

        public async Task<bool> IsLeader(long userId)
        {
            return await entities.AnyAsync(u => u.Id == userId && u.IsLeader == true);
        }
    }
}
