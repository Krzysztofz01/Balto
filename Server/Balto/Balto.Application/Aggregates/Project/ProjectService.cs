using Balto.Domain.Aggregates.Project;
using Balto.Domain.Common;
using Balto.Infrastructure.Abstraction;
using System;
using System.Threading.Tasks;
using static Balto.Application.Aggregates.Project.Commands;

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

        public async Task Handle(object command)
        {
            switch(command)
            {
                case V1.ProjectAdd cmd:
                    await HandleProjectCreateV1(cmd);
                    break;

                case V1.ProjectUpdate cmd:
                    await HandleUpdate(cmd.Id, c => c.Update(cmd.Title, _requestAuthorizationHandler.GetUserGuid()));
                    break;

                case V1.ProjectDelete cmd:
                    await HandleUpdate(cmd.Id, c => c.Delete(_requestAuthorizationHandler.GetUserGuid()));
                    break;

                case V1.ProjectAddContributor cmd:
                    await HandleUpdate(cmd.Id, c => c.AddContributor(cmd.UserId, _requestAuthorizationHandler.GetUserGuid()));
                    break;

                case V1.ProjectDeleteContributor cmd:
                    await HandleUpdate(cmd.Id, c => c.DeleteContributor(cmd.UserId, _requestAuthorizationHandler.GetUserGuid()));
                    break;

                case V1.ProjectLeave cmd:
                    await HandleUpdate(cmd.Id, c => c.Leave(_requestAuthorizationHandler.GetUserGuid()));
                    break;

                case V1.ProjectChangeTicketStatus cmd:
                    await HandleUpdate(cmd.Id, c => c.ChangeTicketTokenStatus(_requestAuthorizationHandler.GetUserGuid()));
                    break;

                case V1.ProjectAddTicket cmd:
                    await HandleProjectTicketCreateV1(cmd);
                    break;

                case V1.ProjectAddTable cmd:
                    await HandleUpdate(cmd.Id, c => c.AddTable(cmd.Title, _requestAuthorizationHandler.GetUserGuid()));
                    break;

                case V1.ProjectUpdateTable cmd:
                    await HandleUpdate(cmd.Id, c => c.UpdateTable(cmd.TableId, cmd.Title, cmd.Color, _requestAuthorizationHandler.GetUserGuid()));
                    break;

                case V1.ProjectDeleteTable cmd:
                    await HandleUpdate(cmd.Id, c => c.DeleteTable(cmd.TableId, _requestAuthorizationHandler.GetUserGuid()));
                    break;

                case V1.ProjectAddCard cmd:
                    await HandleUpdate(cmd.Id, c => c.AddCard(cmd.TableId, cmd.Title, _requestAuthorizationHandler.GetUserGuid()));
                    break;

                case V1.ProjectUpdateCard cmd:
                    await HandleUpdate(cmd.Id, c => c.UpdateCard(cmd.CardId, cmd.Title, cmd.Content, cmd.Color, cmd.StartingDate, cmd.Notify, cmd.EndingDate, cmd.AssignedUserId, cmd.Priority));
                    break;

                case V1.ProjectDeleteCard cmd:
                    await HandleUpdate(cmd.Id, c => c.DeleteCard(cmd.CardId, _requestAuthorizationHandler.GetUserGuid()));
                    break;

                case V1.ProjectChangeCardStatus cmd:
                    await HandleUpdate(cmd.Id, c => c.ChangeCardStatus(cmd.CardId, _requestAuthorizationHandler.GetUserGuid()));
                    break;

                case V1.ProjectChangeCardsOrdinalNumber cmd:
                    await HandleUpdate(cmd.Id, c => c.UpdateCardOrdinalNumbers(cmd.CardOrderMap));
                    break;

                case V1.ProjectAddComment cmd:
                    await HandleUpdate(cmd.Id, c => c.AddCommentToCard(cmd.CardId, cmd.Content, _requestAuthorizationHandler.GetUserGuid()));
                    break;

                case V1.ProjectDeleteComment cmd:
                    await HandleUpdate(cmd.Id, c => c.DeleteCommentFromCard(cmd.CommentId, _requestAuthorizationHandler.GetUserGuid()));
                    break;
            }
        }

        private async Task HandleUpdate(Guid projectId, Action<Domain.Aggregates.Project.Project> operation)
        {
            var project = await _projectRepository.Load(projectId.ToString());
            if (project is null) throw new InvalidCastException($"Project with given id: { projectId } not found.");

            operation(project);

            await _unitOfWork.Commit();
        }

        private async Task HandleProjectCreateV1(V1.ProjectAdd cmd)
        {
            var project = Domain.Aggregates.Project.Project.Factory.Create(
                new ProjectOwnerId(_requestAuthorizationHandler.GetUserGuid()),
                ProjectTitle.FromString(cmd.Title));

            await _projectRepository.Add(project);

            await _unitOfWork.Commit();
        }

        private async Task HandleProjectTicketCreateV1(V1.ProjectAddTicket cmd)
        {
            var project = await _projectRepository.LoadByToken(cmd.Token);
            if (project is null) throw new InvalidCastException($"Project with given token not found.");

            project.AddTicket(cmd.Title, cmd.Content);

            await _unitOfWork.Commit();
        }
    }
}
