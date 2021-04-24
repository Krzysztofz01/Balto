using AutoMapper;
using Balto.Domain;
using Balto.Repository;
using Balto.Service.Dto;
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

        public async Task<bool> Add(ObjectiveDto objective, long userId)
        {
            var objectiveMapped = mapper.Map<Objective>(objective);
            objectiveMapped.UserId = userId;

            await objectiveRepository.Add(objectiveMapped);

            if (await objectiveRepository.Save() > 0)
            {
                return true;
            }
            return false;
        }

        public async Task<bool> ChangeState(long objectiveId, long userId)
        {
            var objective = await objectiveRepository.SingleUsersObjective(objectiveId, userId);
            if (objective is null) return false;

            objective.Finished = !objective.Finished;
            objectiveRepository.UpdateState(objective);

            if (await objectiveRepository.Save() > 0)
            {
                return true;
            }
            return false;
        }

        public async Task<bool> Delete(long objectiveId, long userId)
        {
            var objective = await objectiveRepository.SingleUsersObjective(objectiveId, userId);
            if (objective is null) return false;

            objectiveRepository.Remove(objective);
            if (await objectiveRepository.Save() > 0) return true;
            return false;
        }

        public async Task<ObjectiveDto> Get(long objectiveId, long userId)
        {
            var objective = await objectiveRepository.SingleUsersObjective(objectiveId, userId);

            return mapper.Map<ObjectiveDto>(objective);
        }


        public async Task<IEnumerable<ObjectiveDto>> GetAll(long userId)
        {
            var objectives = objectiveRepository.AllUsersObjectives(userId);

            return mapper.Map<IEnumerable<ObjectiveDto>>(objectives);
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
