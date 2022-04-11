using Balto.Domain.Goals;
using Balto.Infrastructure.Core.Abstraction;
using Microsoft.Extensions.Logging;
using Quartz;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Balto.Application.Goals
{
    [DisallowConcurrentExecution]
    public class GoalRecurringResetJob : IJob
    {
        public const string CronExpression = "0 0 0 1/1 * ? *";
        public const string JobName = "GoalRecurringGoalsReset";

        private readonly IUnitOfWork _unitOfWork;
        private readonly IDataAccess _dataAccess;
        private readonly ILogger<GoalRecurringResetJob> _logger;

        public GoalRecurringResetJob(IUnitOfWork unitOfWork, IDataAccess dataAccess, ILogger<GoalRecurringResetJob> logger)
        {
            _unitOfWork = unitOfWork ??
                throw new ArgumentNullException(nameof(unitOfWork));

            _dataAccess = dataAccess ??
                throw new ArgumentNullException(nameof(dataAccess));

            _logger = logger ??
                throw new ArgumentNullException(nameof(logger));
        }

        [Obsolete("This method has inconsistent usage of data access abstraction.")]
        public async Task Execute(IJobExecutionContext context)
        {
            try
            {
                _logger.LogInformation($"{JobName} scheduled job started.");

                var goals = _dataAccess.GoalsTracked
                    .Where(g => g.IsRecurring.Value);

                foreach (var goal in goals)
                {
                    goal.Apply(new Events.V1.GoalRecurringReset { Id = goal.Id });
                }

                await _unitOfWork.Commit();

                _logger.LogInformation($"{JobName} scheduled job finished.");
            }
            catch (Exception ex)
            {
                _logger.LogError($"{JobName} scheduled job failed.", ex);
            }
        }
    }
}
