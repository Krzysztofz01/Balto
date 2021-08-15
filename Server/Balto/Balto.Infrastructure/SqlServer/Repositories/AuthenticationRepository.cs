using Balto.Domain.Aggregates.User;
using Balto.Infrastructure.SqlServer.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Balto.Infrastructure.SqlServer.Repositories
{
    public class AuthenticationRepository : IAuthenticationRepository
    {
        private readonly BaltoDbContext _dbContext;

        public AuthenticationRepository(BaltoDbContext dbContext)
        {
            _dbContext = dbContext ??
                throw new ArgumentNullException(nameof(dbContext));
        }

        public async Task<User> GetUserByEmail(string email)
        {
            return await _dbContext.Users
                .SingleAsync(e => e.Email == email);
        }

        public async Task<User> GetUserByRefreshToken(string refreshToken)
        {
            return await _dbContext.Users
                .Include(e => e.RefreshTokens)
                .SingleAsync(e => e.RefreshTokens.Any(t => t.Token == refreshToken));
        }
    }
}
