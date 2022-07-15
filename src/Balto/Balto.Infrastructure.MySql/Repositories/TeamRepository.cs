using Balto.Domain.Team;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace Balto.Infrastructure.MySql.Repositories
{
    public class TeamRepository : ITeamRepository
    {
        private readonly BaltoDbContext _context;

        public TeamRepository(BaltoDbContext dbContext) =>
            _context = dbContext ?? throw new ArgumentNullException(nameof(dbContext));

        public Task Add(Team team)
        {
            _ = _context.Teams.Add(team);
            return Task.CompletedTask;
        }

        public async Task<bool> Exists(Guid id)
        {
            return await _context.Teams
                .AsNoTracking()
                .AnyAsync(t => t.Id == id);
        }

        public async Task<Team> Get(Guid id)
        {
            return await _context.Teams
                .Include(t => t.Members)
                .FirstAsync(t => t.Id == id);
        }
    }
}
