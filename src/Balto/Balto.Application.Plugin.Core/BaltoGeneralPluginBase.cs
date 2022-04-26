using Balto.Domain.Goals;
using Balto.Domain.Notes;
using Balto.Domain.Projects;
using Balto.Domain.Tags;
using Balto.Infrastructure.Core.Abstraction;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace Balto.Application.Plugin.Core
{
    public abstract class BaltoGeneralPluginBase<TPlugin> : BaltoPluginBase<TPlugin> where TPlugin : BaltoPluginBase<TPlugin>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<TPlugin> _logger;

        protected INoteRepository NoteRepository => _unitOfWork.NoteRepository;
        protected IGoalRepository GoalRepository => _unitOfWork.GoalRepository;
        protected IProjectRepository ProjectRepository => _unitOfWork.ProjectRepository;
        protected ITagRepository TagRepository => _unitOfWork.TagRepository;
        protected ILogger<TPlugin> Logger => _logger;

        protected async Task CommitChanges() => await _unitOfWork.Commit();

        public BaltoGeneralPluginBase(IUnitOfWork unitOfWork, ILogger<TPlugin> logger)
        {
            _unitOfWork = unitOfWork ??
                throw new ArgumentNullException(nameof(unitOfWork));

            _logger = logger ??
                throw new ArgumentNullException(nameof(logger));
        }
    }
}
