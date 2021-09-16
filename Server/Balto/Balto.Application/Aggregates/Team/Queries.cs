using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Balto.Application.Aggregates.Team
{
    public static class Queries
    {
        public static async Task<IEnumerable<Domain.Aggregates.Team.Team>> GetAllTeams(this DbSet<Domain.Aggregates.Team.Team> entites)
        {
            return await entites
                .AsNoTracking()
                .ToListAsync();
        }

        public static async Task<Domain.Aggregates.Team.Team> GetTeamById(this DbSet<Domain.Aggregates.Team.Team> entites, Guid teamId)
        {
            return await entites
                .AsNoTracking()
                .SingleAsync(e => e.Id.Value == teamId);
        }
    }
}
