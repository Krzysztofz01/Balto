using Balto.Application.Abstraction;
using Balto.Domain.Core.Events;
using Balto.Domain.Projects;
using Balto.Infrastructure.Core.Abstraction;
using System;
using System.Threading.Tasks;
using static Balto.Application.Projects.Commands;
using static Balto.Domain.Projects.Events.V1;

namespace Balto.Application.Projects
{
    public class ProjectService : IProjectService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IScopeWrapperService _scopeWrapperService;

        public ProjectService(IUnitOfWork unitOfWork, IScopeWrapperService scopeWrapperService)
        {
            _unitOfWork = unitOfWork ??
                throw new ArgumentNullException(nameof(unitOfWork));

            _scopeWrapperService = scopeWrapperService ??
                throw new ArgumentNullException(nameof(scopeWrapperService));
        }

        public async Task Handle(IApplicationCommand<Project> command)
        {
            switch(command)
            {
                case V1.Create c: await Create(new ProjectCreated { Title = c.Title, CurrentUserId = _scopeWrapperService.GetUserId() }); break;

                case V1.Update c: await Apply(c.Id, new ProjectUpdated { Id = c.Id, Title = c.Title, TicketStatus = c.TicketStatus, CurrentUserId = _scopeWrapperService.GetUserId() }); break;
                case V1.Delete c: await Apply(c.Id, new ProjectDeleted { Id = c.Id, CurrentUserId = _scopeWrapperService.GetUserId() }); break;
                case V1.PushTicket c: await Apply(c.TicketToken, new TicketPushed { Title = c.Title, Content = c.Content }); break;

                case V1.AddContributor c: await Apply(c.Id, new ProjectContributorAdded { Id = c.Id, UserId = c.UserId, CurrentUserId = _scopeWrapperService.GetUserId() }); break;
                case V1.DeleteContributor c: await Apply(c.Id, new ProjectContributorDeleted { Id = c.Id, UserId = c.UserId, CurrentUserId = _scopeWrapperService.GetUserId() }); break;
                case V1.UpdateContributor c: await Apply(c.Id, new ProjectContributorUpdated { Id = c.Id, UserId = c.UserId, CurrentUserId = _scopeWrapperService.GetUserId(), Role = c.Role }); break;
                case V1.LeaveAsContributor c: await Apply(c.Id, new ProjectContributorLeft { Id = c.Id, CurrentUserId = _scopeWrapperService.GetUserId() }); break;

                case V1.AddTable c: await Apply(c.Id, new ProjectTableCreated { Id = c.Id, Title = c.Title }); break;
                case V1.UpdateTable c: await Apply(c.Id, new ProjectTableUpdated { Id = c.Id, TableId = c.TableId, Title = c.Title, Color = c.Color }); break;
                case V1.DeleteTable c: await Apply(c.Id, new ProjectTableDeleted { Id = c.Id, TableId = c.TableId, CurrentUserId = _scopeWrapperService.GetUserId() }); break;
                case V1.TableOrdinalNumbersChanged c: await Apply(c.Id, new ProjectTableTasksOrdinalNumbersChanged { Id = c.Id, IdOrdinalNumberPairs = c.IdOrdinalNumberPairs, TableId = c.TableId }); break;

                case V1.AddTask c: await Apply(c.Id, new ProjectTaskCreated { Id = c.Id, TableId = c.TableId, Title = c.Title, CurrentUserId = _scopeWrapperService.GetUserId() }); break;
                case V1.UpdateTask c: await Apply(c.Id, new ProjectTaskUpdated { Id = c.Id, AssignedContributorId = c.AssignedContributorId, Content = c.Content, Deadline = c.Deadline, Priority = c.Priority, StartingDate = c.StartingDate, TableId = c.TableId, TaskId = c.TaskId, Title = c.Title }); break;
                case V1.DeleteTask c: await Apply(c.Id, new ProjectTaskDeleted { Id = c.Id, TableId = c.TableId, TaskId = c.TaskId }); break;
                case V1.ChangeTaskStatus c: await Apply(c.Id, new ProjectTaskStatusChanged { Id = c.Id, TableId = c.TableId, TaskId = c.TaskId, Status = c.Status, CurrentUserId = _scopeWrapperService.GetUserId() }); break;

                case V1.TaskTagAssign c: await Apply(c.Id, new ProjectTaskTagAssigned { Id = c.Id, TableId = c.TableId, TaskId = c.TaskId, TagId = c.TagId }); break;
                case V1.TaskTagUnassign c: await Apply(c.Id, new ProjectTaskTagUnassigned { Id = c.Id, TableId = c.TableId, TaskId = c.TaskId, TagId = c.TagId }); break;

                default: throw new InvalidOperationException("This command is not supported.");
            }
        }

        private async Task Apply(Guid id, IEventBase @event)
        {
            var project = await _unitOfWork.ProjectRepository.Get(id);

            project.Apply(@event);

            await _unitOfWork.Commit();
        }

        private async Task Apply(string ticketToken, TicketPushed @event)
        {
            var project = await _unitOfWork.ProjectRepository.Get(ticketToken);

            @event.Id = project.Id;
            project.Apply(@event);

            await _unitOfWork.Commit();
        }

        private async Task Create(ProjectCreated @event)
        {
            var project = Project.Factory.Create(@event);

            await _unitOfWork.ProjectRepository.Add(project);

            await _unitOfWork.Commit();
        }
    }
}
