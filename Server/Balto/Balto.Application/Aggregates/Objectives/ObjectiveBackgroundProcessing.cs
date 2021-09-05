using Balto.Domain.Aggregates.Objective;
using Balto.Domain.Common;
using Balto.Infrastructure.SqlServer.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Balto.Application.Aggregates.Objectives
{
    public class ObjectiveBackgroundProcessing : IObjectiveBackgroundProcessing
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly BaltoDbContext _dbContext;

        public ObjectiveBackgroundProcessing(
            IUnitOfWork unitOfWork,
            BaltoDbContext dbContext)
        {
            _unitOfWork = unitOfWork ??
                throw new ArgumentNullException(nameof(unitOfWork));

            _dbContext = dbContext ??
                throw new ArgumentNullException(nameof(dbContext));
        }

        public async Task ResetDailyObjectives()
        {
            var objectives = await GetAllDailyObjectives();

            foreach (var objective in objectives) objective.ResetState();

            await _unitOfWork.Commit();
        }

        public async Task ResetWeeklyObjectives()
        {
            var objectives = await GetAllWeeklyObjectives();

            foreach (var objective in objectives) objective.ResetState();

            await _unitOfWork.Commit();
        }


        //TODO: Refactor query
        private async Task<IEnumerable<Objective>> GetAllDailyObjectives()
        {
            return await _dbContext.Set<Objective>()
                .Where(e => e.Periodicity.Value == ObjectivePeriodicityType.Daily)
                .ToListAsync();
        }

        //TODO: Refactor query
        private async Task<IEnumerable<Objective>> GetAllWeeklyObjectives()
        {
            return await _dbContext.Set<Objective>()
                .Where(e => e.Periodicity.Value == ObjectivePeriodicityType.Weekly)
                .ToListAsync();
        }
    }
}
