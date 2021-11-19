using Balto.Application.Abstraction;
using Balto.Domain.Core.Events;
using Balto.Domain.Goals;
using Balto.Infrastructure.Core.Abstraction;
using System;
using System.Threading.Tasks;
using static Balto.Application.Goals.Commands;
using static Balto.Domain.Goals.Events.V1;

namespace Balto.Application.Goals
{
    public class GoalService : IGoalService
    {
        private readonly IGoalRepository _goalRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IScopeWrapperService _scopeWrapperService;

        public GoalService(IGoalRepository goalRepository, IUnitOfWork unitOfWork, IScopeWrapperService scopeWrapperService)
        {
            _goalRepository = goalRepository ??
                throw new ArgumentNullException(nameof(goalRepository));

            _unitOfWork = unitOfWork ??
                throw new ArgumentNullException(nameof(unitOfWork));

            _scopeWrapperService = scopeWrapperService ??
                throw new ArgumentNullException(nameof(scopeWrapperService));
        }

        public async Task Handle(IApplicationCommand<Goal> command)
        {
            switch(command)
            {
                case V1.Create c: await Create(new GoalCreated { Title = c.Title, CurrentUserId = _scopeWrapperService.GetUserId() }); break;
                
                case V1.Delete c: await Apply(c.Id, new GoalDeleted { Id = c.Id }); break;
                case V1.Update c: await Apply(c.Id, new GoalUpdated { Id = c.Id, Color = c.Color, Deadline = c.Deadline, Description = c.Description, IsRecurring = c.IsRecurring, Priority = c.Priority, StartingDate = c.StartingDate, Title = c.Title }); break;
                case V1.StatusChange c: await Apply(c.Id, new GoalStateChanged { Id = c.Id, State = c.State }); break;

                default: throw new InvalidOperationException("This command is not supported.");
            }
        }

        private async Task Apply(Guid id, IEventBase @event)
        {
            var goal = await _goalRepository.Get(id);

            goal.Apply(@event);

            await _unitOfWork.Commit();
        }

        private async Task Create(GoalCreated @event)
        {
            var goal = Goal.Factory.Create(@event);

            await _goalRepository.Add(goal);

            await _unitOfWork.Commit();
        }
    }
}
