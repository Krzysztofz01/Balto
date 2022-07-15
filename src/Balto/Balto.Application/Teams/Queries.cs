using Balto.Domain.Team;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Balto.Application.Teams
{
    public static class Queries
    {
        public static async Task<IEnumerable<Team>> GetAllTeams(this IQueryable<Team> teams)
        {
            return await teams
                .AsNoTracking()
                .ToListAsync();
        }

        public static async Task<Team> GetTeamById(this IQueryable<Team> teams, Guid teamId)
        {
            return await teams
                .Include(t => t.Members)
                .AsNoTracking()
                .SingleAsync(t => t.Id == teamId);
        }

        public static async Task<IEnumerable<Team>> GetTeamsByUserId(this IQueryable<Team> teams, Guid userId)
        {
            return await teams
                .Include(t => t.Members)
                .AsNoTracking()
                .Where(t => t.Members.Any(m => m.IdentityId.Value == userId))
                .ToListAsync();
        }
    }
}
