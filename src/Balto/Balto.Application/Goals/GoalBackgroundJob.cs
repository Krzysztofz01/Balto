using Balto.Domain.Goals;
using Balto.Infrastructure.Core.Abstraction;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Balto.Application.Goals
{
    public class GoalBackgroundJob : IGoalBackgroundJob
    {
        private readonly IDataAccess _dataAccess;
        private readonly IUnitOfWork _unitOfWork;

        public GoalBackgroundJob(IDataAccess dataAccess, IUnitOfWork unitOfWork)
        {
            _dataAccess = dataAccess ??
                throw new ArgumentNullException(nameof(dataAccess));

            _unitOfWork = unitOfWork ??
                throw new ArgumentNullException(nameof(unitOfWork));
        }

        public async Task DailyResetRecurringGoals()
        {
            var goals = await _dataAccess.GoalsTracked
                .Where(g => g.IsRecurring.Value)
                .ToListAsync();

            //TODO: Check if restart
            throw new NotImplementedException();

            foreach(var goal in goals)
            {
                goal.Apply(new Events.V1.GoalRecurringReset { Id = goal.Id });
            }

            await _unitOfWork.Commit();
        }
    }
};
