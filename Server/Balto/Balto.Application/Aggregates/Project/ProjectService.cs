using Balto.Domain.Aggregates.Project;
using Balto.Domain.Common;
using Balto.Infrastructure.Abstraction;
using System;
using System.Threading.Tasks;

namespace Balto.Application.Aggregates.Project
{
    public class ProjectService : IApplicationService
    {
        private readonly IProjectRepository _projectRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRequestAuthorizationHandler _requestAuthorizationHandler;

        public ProjectService(
            IProjectRepository projectRepository,
            IUnitOfWork unitOfWork,
            IRequestAuthorizationHandler requestAuthorizationHandler)
        {
            _projectRepository = projectRepository ??
                throw new ArgumentNullException(nameof(projectRepository));

            _unitOfWork = unitOfWork ??
                throw new ArgumentNullException(nameof(unitOfWork));

            _requestAuthorizationHandler = requestAuthorizationHandler ??
                throw new ArgumentNullException(nameof(requestAuthorizationHandler));
        }

        public Task Handle(object command)
        {
            throw new NotImplementedException();
        }
    }
}
