using Balto.Domain.Goals;
using Balto.Infrastructure.Core.Abstraction;
using System;

namespace Balto.Application.Plugin.Core
{
    public abstract class BaltoGoalPluginBase : BaltoPluginBase
    {
        protected readonly IGoalRepository _goalRepository;
        protected readonly IUnitOfWork _unitOfWork;

        private BaltoGoalPluginBase() { }
        public BaltoGoalPluginBase(IGoalRepository goalRepository, IUnitOfWork unitOfWork)
        {
            _goalRepository = goalRepository ??
                throw new ArgumentNullException(nameof(goalRepository));

            _unitOfWork = unitOfWork ??
                throw new ArgumentNullException(nameof(unitOfWork));
        }
    }
}
