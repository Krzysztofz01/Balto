using Balto.Domain.Notes;
using Balto.Infrastructure.Core.Abstraction;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace Balto.Application.Plugin.Core
{
    public abstract class BaltoNotePluginBase<TPlugin> : BaltoPluginBase<TPlugin> where TPlugin : BaltoPluginBase<TPlugin>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<TPlugin> _logger;
        private readonly IScopeWrapperService _scopeWrapperService;

        protected INoteRepository NoteRepository => _unitOfWork.NoteRepository;
        protected ILogger<TPlugin> Logger => _logger;
        protected Guid CurrentUserId => _scopeWrapperService.GetUserId();

        protected async Task CommitChanges() => await _unitOfWork.Commit();

        public BaltoNotePluginBase(
            IUnitOfWork unitOfWork,
            ILogger<TPlugin> logger,
            IScopeWrapperService scopeWrapperService)
        {
            _unitOfWork = unitOfWork ??
                throw new ArgumentNullException(nameof(unitOfWork));

            _logger = logger ??
                throw new ArgumentNullException(nameof(logger));

            _scopeWrapperService = scopeWrapperService ??
                throw new ArgumentNullException(nameof(scopeWrapperService));
        }
    }
}
