using Balto.Domain.Identities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Balto.Application.Identities
{
    public static class Queries
    {
        public static async Task<IEnumerable<Identity>> GetAllIdentities(this IQueryable<Identity> identities)
        {
            return await identities
                .ToListAsync();
        }

        public static async Task<Identity> GetIdentityById(this IQueryable<Identity> identities, Guid id)
        {
            return await identities
                .SingleAsync(i => i.Id == id);
        }

        public static async Task<IEnumerable<Identity>> GetAllIdentitiesInTeam(this IQueryable<Identity> identities, Guid teamId)
        {
            return await identities
                .Where(i => i.TeamId.Value.GetValueOrDefault() == teamId)
                .ToListAsync();
        }
    }
}
