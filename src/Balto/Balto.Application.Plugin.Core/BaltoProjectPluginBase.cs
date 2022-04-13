using Balto.Domain.Projects;
using Balto.Infrastructure.Core.Abstraction;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace Balto.Application.Plugin.Core
{
    public abstract class BaltoProjectPluginBase<TPlugin> : BaltoPluginBase<TPlugin> where TPlugin : BaltoPluginBase<TPlugin>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<TPlugin> _logger;

        protected IProjectRepository ProjectRepository => _unitOfWork.ProjectRepository;
        protected ILogger<TPlugin> Logger => _logger;

        protected async Task CommitChanges() => await _unitOfWork.Commit();
        private BaltoProjectPluginBase() { }
        public BaltoProjectPluginBase(IUnitOfWork unitOfWork, ILogger<TPlugin> logger)
        {
            _unitOfWork = unitOfWork ??
                throw new ArgumentNullException(nameof(unitOfWork));

            _logger = logger ??
                throw new ArgumentNullException(nameof(logger));
        }
    }
}
