using Balto.Domain.Aggregates.Objective;
using Balto.Domain.Common;
using Balto.Infrastructure.Abstraction;
using System;
using System.Threading.Tasks;
using static Balto.Application.Aggregates.Objectives.Commands;

namespace Balto.Application.Aggregates.Objectives
{
    public class ObjectiveService : IApplicationService
    {
        private readonly IObjectiveRepository _objectiveRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRequestAuthorizationHandler _requestAuthorizationHandler;

        public ObjectiveService(
            IObjectiveRepository objectiveRepository,
            IUnitOfWork unitOfWork,
            IRequestAuthorizationHandler requestAuthorizationHandler)
        {
            _objectiveRepository = objectiveRepository ??
                throw new ArgumentNullException(nameof(objectiveRepository));

            _unitOfWork = unitOfWork ??
                throw new ArgumentNullException(nameof(unitOfWork));

            _requestAuthorizationHandler = requestAuthorizationHandler ??
                throw new ArgumentNullException(nameof(requestAuthorizationHandler));
        }

        public async Task Handle(object command)
        {
            switch(command)
            {
                case V1.ObjectiveAdd cmd:
                    await HandleCreateV1(cmd);
                    break;

                case V1.ObjectiveDelete cmd:
                    await HandleUpdate(cmd.TargetObjectiveId, c => c.Delete());
                    break;

                case V1.ObjectiveUpdate cmd:
                    await HandleUpdate(cmd.TargetObjectiveId, c => c.Update(cmd.Title, cmd.Description, cmd.Priority));
                    break;

                case V1.ObjectiveStateChange cmd:
                    await HandleUpdate(cmd.TargetObjectiveId, c => c.ChangeState());
                    break;
            }
        }

        private async Task HandleUpdate(Guid objectiveId, Action<Objective> operation)
        {
            var objective = await _objectiveRepository.Load(objectiveId.ToString());
            if (objective is null) throw new InvalidOperationException($"Objective with given id: { objectiveId } not found.");

            operation(objective);

            await _unitOfWork.Commit();
        }

        private async Task HandleCreateV1(V1.ObjectiveAdd cmd)
        {
            var objective = Objective.Factory.Create(
                new ObjectiveOwnerId(_requestAuthorizationHandler.GetUserGuid()),
                ObjectiveTitle.FromString(cmd.Title),
                ObjectiveDescription.FromString(cmd.Description),
                ObjectivePriority.Set(cmd.Priority),
                ObjectivePeriodicity.Set(cmd.Periodicity),
                ObjectiveStartingDate.Set(cmd.StartingDate),
                ObjectiveEndingDate.Set(cmd.EndingDate));

            await _objectiveRepository.Add(objective);

            await _unitOfWork.Commit();
        }
    }
}
