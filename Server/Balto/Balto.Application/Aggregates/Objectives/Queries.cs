using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Balto.Application.Aggregates.Objectives
{
    public static class Queries
    {
        public static async Task<IEnumerable<Domain.Aggregates.Objective.Objective>> GetAllObjectives(this DbSet<Domain.Aggregates.Objective.Objective> entites)
        {
            return await entites
                .AsNoTracking()
                .ToListAsync();
        }

        public static async Task<Domain.Aggregates.Objective.Objective> GetObjectiveById(this DbSet<Domain.Aggregates.Objective.Objective> entites, Guid objectiveId)
        {
            return await entites
                .AsNoTracking()
                .SingleAsync(e => e.Id.Value == objectiveId);
        }
    }
}
