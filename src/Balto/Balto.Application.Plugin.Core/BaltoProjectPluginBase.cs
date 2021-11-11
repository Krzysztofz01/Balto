using Balto.Domain.Projects;
using Balto.Infrastructure.Core.Abstraction;
using System;

namespace Balto.Application.Plugin.Core
{
    public abstract class BaltoProjectPluginBase : BaltoPluginBase
    {
        protected readonly IProjectRepository _projectRepository;
        protected readonly IUnitOfWork _unitOfWork;

        private BaltoProjectPluginBase() { }
        public BaltoProjectPluginBase(IProjectRepository projectRepository, IUnitOfWork unitOfWork)
        {
            _projectRepository = projectRepository ??
                throw new ArgumentNullException(nameof(projectRepository));

            _unitOfWork = unitOfWork ??
                throw new ArgumentNullException(nameof(unitOfWork));
        }
    }
}
