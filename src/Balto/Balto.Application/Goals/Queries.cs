﻿using Balto.Domain.Goals;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Balto.Application.Goals
{
    public static class Queries
    {
        public static async Task<IEnumerable<Goal>> GetAllGoals(this IQueryable<Goal> goals)
        {
            return await goals
                .Include(g => g.Tags)
                .AsNoTracking()
                .ToListAsync();
        }

        public static async Task<Goal> GetGoalById(this IQueryable<Goal> goals, Guid goalId)
        {
            return await goals
                .Include(g => g.Tags)
                .AsNoTracking()
                .SingleAsync(g => g.Id == goalId);
        }

        public static async Task<IEnumerable<Goal>> GetAllNonRecurringGoals(this IQueryable<Goal> goals)
        {
            return await goals
                .Include(g => g.Tags)
                .Where(g => !g.IsRecurring.Value)
                .AsNoTracking()
                .ToListAsync();
        }

        public static async Task<IEnumerable<Goal>> GetAllRecurringGoals(this IQueryable<Goal> goals)
        {
            return await goals
                .Include(g => g.Tags)
                .Where(g => g.IsRecurring.Value)
                .AsNoTracking()
                .ToListAsync();
        }
    }
}
