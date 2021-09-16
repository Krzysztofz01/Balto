using Balto.Domain.Aggregates.Team;
using Balto.Infrastructure.SqlServer.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace Balto.Infrastructure.SqlServer.Repositories
{
    public class TeamRepository : ITeamRepository, IDisposable
    {
        private readonly BaltoDbContext _dbContext;

        public TeamRepository(BaltoDbContext dbContext)
        {
            _dbContext = dbContext ??
                throw new ArgumentNullException(nameof(dbContext));
        }

        public async Task Add(Team entity)
        {
            await _dbContext.Teams.AddAsync(entity);
        }

        public void Dispose()
        {
            _dbContext.Dispose();
        }

        public async Task<bool> Exists(TeamId id)
        {
            return await _dbContext.Teams.AnyAsync(e => e.Id.Value == id);
        }

        public async Task<Team> Load(TeamId id)
        {
            return await _dbContext.Teams.SingleAsync(e => e.Id.Value == id);
        }
    }
}
