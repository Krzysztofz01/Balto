using Balto.Domain.Goals;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace Balto.Infrastructure.MySql.Repositories
{
    public class GoalRepository : IGoalRepository
    {
        private readonly BaltoDbContext _context;

        public GoalRepository(BaltoDbContext dbContext) =>
            _context = dbContext ?? throw new ArgumentNullException(nameof(dbContext));

        public async Task Add(Goal goal)
        {
            _ = await _context.Goals.AddAsync(goal);
        }

        public async Task<bool> Exists(Guid id)
        {
            return await _context.Goals.AnyAsync(g => g.Id == id);
        }

        public async Task<Goal> Get(Guid id)
        {
            return await _context.Goals
                .SingleAsync(g => g.Id == id);
        }
    }
}
