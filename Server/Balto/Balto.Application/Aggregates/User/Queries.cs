using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Balto.Application.Aggregates.User
{
    public static class Queries
    {
        public static async Task<IEnumerable<Domain.Aggregates.User.User>> GetAllUsers(this DbSet<Domain.Aggregates.User.User> entities)
        {
            return await entities
                .AsNoTracking()
                .ToListAsync();
        }

        public static async Task<Domain.Aggregates.User.User> GetUserById(this DbSet<Domain.Aggregates.User.User> entities, Guid userId)
        {
            return await entities
                .AsNoTracking()
                .SingleAsync(e => e.Id.Value == userId);
        }

        public static async Task<IEnumerable<Domain.Aggregates.User.User>> GetAllTeamUsers(this DbSet<Domain.Aggregates.User.User> entities, Guid teamId)
        {
            return await entities
                .AsNoTracking()
                .Where(e => e.TeamId.Value == teamId)
                .ToListAsync();
        }

        public static async Task<IEnumerable<Domain.Aggregates.User.User>> GetAllUsersActivated(this DbSet<Domain.Aggregates.User.User> entities)
        {
            return await entities
                .AsNoTracking()
                .Where(e => e.IsActivated)
                .ToListAsync();
        }

        public static async Task<IEnumerable<Domain.Aggregates.User.User>> GetAllUsersNotActivated(this DbSet<Domain.Aggregates.User.User> entities)
        {
            return await entities
                .AsNoTracking()
                .Where(e => !e.IsActivated)
                .ToListAsync();
        }
    }
}
