using AutoMapper;
using Balto.Domain;
using Balto.Repository;
using Balto.Service.Dto;
using Balto.Service.Handlers;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Balto.Service
{
    public class ObjectiveService : IObjectiveService
    {
        private readonly IObjectiveRepository objectiveRepository;
        private readonly IMapper mapper;

        public ObjectiveService(
            IObjectiveRepository objectiveRepository,
            IMapper mapper)
        {
            this.objectiveRepository = objectiveRepository ??
                throw new ArgumentNullException(nameof(objectiveRepository));

            this.mapper = mapper ??
                throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<IServiceResult> Add(ObjectiveDto objective, long userId)
        {
            var objectiveMapped = mapper.Map<Objective>(objective);
            objectiveMapped.UserId = userId;

            await objectiveRepository.Add(objectiveMapped);

            if (await objectiveRepository.Save() > 0)
            {
                return new ServiceResult(ResultStatus.Sucess);
            }
            return new ServiceResult(ResultStatus.Failed);
        }

        public async Task<IServiceResult> ChangeState(long objectiveId, long userId)
        {
            var objective = await objectiveRepository.SingleUsersObjective(objectiveId, userId);
            if (objective is null) return new ServiceResult(ResultStatus.NotFound);

            objective.Finished = !objective.Finished;
            objectiveRepository.UpdateState(objective);

            if (await objectiveRepository.Save() > 0)
            {
                return new ServiceResult(ResultStatus.Sucess);
            }
            return new ServiceResult(ResultStatus.Failed);
        }

        public async Task<IServiceResult> Delete(long objectiveId, long userId)
        {
            var objective = await objectiveRepository.SingleUsersObjective(objectiveId, userId);
            if (objective is null) new ServiceResult(ResultStatus.NotFound);

            objectiveRepository.Remove(objective);
            if (await objectiveRepository.Save() > 0) return new ServiceResult(ResultStatus.Sucess);
            return new ServiceResult(ResultStatus.Failed);
        }

        public async Task<int> DeleteOldFinished()
        {
            var objectives = objectiveRepository.Find(o => o.Finished == true && o.EndingDate.AddDays(14) < DateTime.Now);
            foreach(var objective in objectives)
            {
                objectiveRepository.Remove(objective);
            }

            return await objectiveRepository.Save();
        }

        public async Task<ServiceResult<ObjectiveDto>> Get(long objectiveId, long userId)
        {
            var objective = await objectiveRepository.SingleUsersObjective(objectiveId, userId);
            if (objective is null) return new ServiceResult<ObjectiveDto>(ResultStatus.NotFound);

            return new ServiceResult<ObjectiveDto>(mapper.Map<ObjectiveDto>(objective));
        }


        public async Task<ServiceResult<IEnumerable<ObjectiveDto>>> GetAll(long userId)
        {
            var objectives = objectiveRepository.AllUsersObjectives(userId);

            return new ServiceResult<IEnumerable<ObjectiveDto>>(mapper.Map<IEnumerable<ObjectiveDto>>(objectives));
        }

        public async Task<ServiceResult<IEnumerable<ObjectiveDto>>> IncomingObjectivesDay()
        {
            var objectives = objectiveRepository.IncomingObjectivesDay();
            var objectivesMapped = mapper.Map<IEnumerable<ObjectiveDto>>(objectives);
            return new ServiceResult<IEnumerable<ObjectiveDto>>(objectivesMapped);
        }

        public async Task<ServiceResult<IEnumerable<ObjectiveDto>>> IncomingObjectivesWeek()
        {
            var objectives = objectiveRepository.IncomingObjectivesWeek();
            var objectivesMapped = mapper.Map<IEnumerable<ObjectiveDto>>(objectives);
            return new ServiceResult<IEnumerable<ObjectiveDto>>(objectivesMapped);
        }

        public async Task<int> ResetDaily()
        {
            var dailyObjectives = objectiveRepository.Find(o => o.Daily == true && o.Finished == true);
            foreach(var objective in dailyObjectives)
            {
                objective.Finished = false;
                objectiveRepository.UpdateState(objective);
            }

            return await objectiveRepository.Save();
        }
    }
}
