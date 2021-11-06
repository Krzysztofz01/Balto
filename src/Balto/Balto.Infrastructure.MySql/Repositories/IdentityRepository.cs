using Balto.Domain.Identities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace Balto.Infrastructure.MySql.Repositories
{
    public class IdentityRepository : IIdentityRepository
    {
        private readonly BaltoDbContext _context;

        public IdentityRepository(BaltoDbContext dbContext) =>
            _context = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        

        public async Task Add(Identity identity)
        {
            _ = await _context.Identities.AddAsync(identity);
        }

        public async Task<bool> Exists(Guid id)
        {
            return await _context.Identities.AnyAsync(i => i.Id == id);
        }

        public async Task<Identity> Get(Guid id)
        {
            return await _context.Identities
                .Include(i => i.Tokens)
                .SingleAsync(i => i.Id == id);
        }
    }
}
