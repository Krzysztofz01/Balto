using Balto.Domain.Identities;
using Balto.Infrastructure.Core.Abstraction;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Balto.Infrastructure.MySql
{
    public class AuthenticationDataAccessService : IAuthenticationDataAccessService
    {
        private readonly BaltoDbContext _context;

        public AuthenticationDataAccessService(BaltoDbContext dbContext) =>
            _context = dbContext ?? throw new ArgumentNullException(nameof(dbContext));

        public async Task<Identity> GetUserByEmail(string email)
        {
            return await _context.Identities
                .Include(e => e.Tokens)
                .SingleAsync(e => e.Email.Value == email);
        }

        public async Task<Identity> GetUserByRefreshToken(string refreshTokenHash)
        {
            return await _context.Identities
                .Include(e => e.Tokens)
                .SingleAsync(e => e.Tokens.Any(t => t.TokenHash == refreshTokenHash));
        }

        public async Task<bool> UserWithEmailExsits(string email)
        {
            return await _context.Identities
                .AnyAsync(e => e.Email.Value == email);
        }
    }
}
