using Balto.Domain.Tags;
using Balto.Infrastructure.Core.Abstraction;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace Balto.Application.Plugin.Core
{
    public abstract class BaltoTagPluginBase<TPlugin> : BaltoPluginBase<TPlugin> where TPlugin : BaltoPluginBase<TPlugin>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<TPlugin> _logger;

        protected ITagRepository TagRepository => _unitOfWork.TagRepository;
        protected ILogger<TPlugin> Logger => _logger;

        protected async Task CommitChanges() => await _unitOfWork.Commit();


        public BaltoTagPluginBase(IUnitOfWork unitOfWork, ILogger<TPlugin> logger)
        {
            _unitOfWork = unitOfWork ??
                throw new ArgumentNullException(nameof(unitOfWork));

            _logger = logger ??
                throw new ArgumentNullException(nameof(logger));
        }
    }
}
