using Balto.Domain.Aggregates.Project;
using Balto.Domain.Common;
using Balto.Infrastructure.Abstraction;
using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using static Balto.Application.Integrations.Trello.Commands;

namespace Balto.Application.Integrations.Trello
{
    public class TrelloService : IApplicationService
    {
        private readonly IProjectRepository _projectRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRequestAuthorizationHandler _requestAuthorizationHandler;
        private readonly ITrelloIntegration _trelloIntegration;

        public TrelloService(
            IProjectRepository projectRepository,
            IUnitOfWork unitOfWork,
            IRequestAuthorizationHandler requestAuthorizationHandler,
            ITrelloIntegration trelloIntegration)
        {
            _projectRepository = projectRepository ??
                throw new ArgumentNullException(nameof(projectRepository));

            _unitOfWork = unitOfWork ??
                throw new ArgumentNullException(nameof(unitOfWork));

            _requestAuthorizationHandler = requestAuthorizationHandler ??
                throw new ArgumentNullException(nameof(requestAuthorizationHandler));

            _trelloIntegration = trelloIntegration ??
                throw new ArgumentNullException(nameof(trelloIntegration));
        }

        public async Task Handle(object command)
        {
            switch(command)
            {
                case V1.TrelloBoardAdd cmd:
                    await HandleCreateV1(cmd);
                    break;
            }
        }

        private async Task HandleCreateV1(V1.TrelloBoardAdd cmd)
        {
            var jsonStringBuilder = new StringBuilder();

            using var reader = new StreamReader(cmd.JsonFile.OpenReadStream());

            while (reader.Peek() >= 0) jsonStringBuilder.AppendLine(await reader.ReadLineAsync());

            var board = _trelloIntegration.DeserializeExportFile(jsonStringBuilder.ToString());

            var project = _trelloIntegration.GenerateProject(board, _requestAuthorizationHandler.GetUserGuid());

            await _projectRepository.Add(project);

            await _unitOfWork.Commit();
        }
    }
}
