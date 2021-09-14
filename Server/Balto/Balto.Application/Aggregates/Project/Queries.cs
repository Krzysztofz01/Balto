using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Balto.Application.Aggregates.Project
{
    public static class Queries
    {
        public static async Task<IEnumerable<Domain.Aggregates.Project.Project>> GetAllProjects(this DbSet<Domain.Aggregates.Project.Project> entities)
        {
            return await entities
                .Include(e => e.Contributors)
                .Include(e => e.Tables)
                .ThenInclude(e => e.Cards)
                .ThenInclude(e => e.Comments)
                .AsNoTracking()
                .ToListAsync();
        }

        public static async Task<Domain.Aggregates.Project.Project> GetProjectById(this DbSet<Domain.Aggregates.Project.Project> entities, Guid projectId)
        {
            return await entities
                .Include(e => e.Contributors)
                .Include(e => e.Tables)
                .ThenInclude(e => e.Cards)
                .ThenInclude(e => e.Comments)
                .AsNoTracking()
                .SingleAsync(e => e.Id.Value == projectId);
        }

        public static async Task<Domain.Aggregates.Project.Card.ProjectTableCard> GetCardById(this DbSet<Domain.Aggregates.Project.Project> entities, Guid cardId)
        {
            return await entities
                .Include(e => e.Contributors)
                .Include(e => e.Tables)
                .ThenInclude(e => e.Cards)
                .ThenInclude(e => e.Comments)
                .SelectMany(e => e.Tables)
                .SelectMany(e => e.Cards)
                .AsNoTracking()
                .SingleAsync(e => e.Id.Value == cardId);
        }
    }
}
