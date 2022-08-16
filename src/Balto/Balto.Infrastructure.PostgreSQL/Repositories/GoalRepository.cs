using Balto.Domain.Goals;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace Balto.Infrastructure.PostgreSQL.Repositories
{
    public class GoalRepository : IGoalRepository
    {
        private readonly BaltoDbContext _context;

        public GoalRepository(BaltoDbContext dbContext) =>
            _context = dbContext ?? throw new ArgumentNullException(nameof(dbContext));

        public Task Add(Goal goal)
        {
            _ = _context.Goals.Add(goal);
            return Task.CompletedTask;
        }

        public async Task<bool> Exists(Guid id)
        {
            return await _context.Goals
                .AsNoTracking()
                .AnyAsync(g => g.Id == id);
        }

        public async Task<Goal> Get(Guid id)
        {
            return await _context.Goals
                .Include(g => g.Tags)
                .FirstAsync(g => g.Id == id);
        }
    }
}
