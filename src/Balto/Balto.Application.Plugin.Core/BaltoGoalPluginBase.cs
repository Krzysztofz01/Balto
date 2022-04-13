using Balto.Domain.Goals;
using Balto.Infrastructure.Core.Abstraction;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace Balto.Application.Plugin.Core
{
    public abstract class BaltoGoalPluginBase<TPlugin> : BaltoPluginBase<TPlugin> where TPlugin : BaltoPluginBase<TPlugin>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<TPlugin> _logger;

        protected IGoalRepository GoalRepository => _unitOfWork.GoalRepository;
        protected ILogger<TPlugin> Logger => _logger;

        protected async Task CommitChanges() => await _unitOfWork.Commit();

        private BaltoGoalPluginBase() { }
        public BaltoGoalPluginBase(IUnitOfWork unitOfWork, ILogger<TPlugin> logger)
        {
            _unitOfWork = unitOfWork ??
                throw new ArgumentNullException(nameof(unitOfWork));

            _logger = logger ??
                throw new ArgumentNullException(nameof(logger));
        }
    }
}
