using Balto.Infrastructure.Core.Abstraction;
using System;
using System.Threading.Tasks;

namespace Balto.Infrastructure.MySql
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly BaltoDbContext _context;

        public UnitOfWork(BaltoDbContext dbContext) =>
            _context = dbContext ?? throw new ArgumentNullException(nameof(dbContext));

        public async Task Commit()
        {
            _ = await _context.SaveChangesAsync();
        }
    }
}
