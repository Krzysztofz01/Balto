using Balto.Domain.Aggregates.User;
using Balto.Infrastructure.SqlServer.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace Balto.Infrastructure.SqlServer.Repositories
{
    public class UserRepository : IUserRepository, IDisposable
    {
        private readonly BaltoDbContext _dbContext;

        public UserRepository(BaltoDbContext dbContext)
        {
            _dbContext = dbContext ??
                throw new ArgumentNullException(nameof(dbContext));
        }

        public async Task Add(User entity)
        {
            await _dbContext.Users.AddAsync(entity);
        }

        public void Dispose()
        {
            _dbContext.Dispose();
        }

        public async Task<bool> Exists(UserId id)
        {
            return await _dbContext.Users.AnyAsync(e => e.Id.Value == id.Value);
        }

        public async Task<User> Load(UserId id)
        {
            return await _dbContext.Users.SingleAsync(e => e.Id.Value == id.Value);
        }
    }
}
