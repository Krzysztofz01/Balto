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

        public async Task<bool> Add(ObjectiveDto objective)
        {
            var objectiveMapped = mapper.Map<Objective>(objective);
            await objectiveRepository.Add(objectiveMapped);

            if (await objectiveRepository.Save() > 0)
            {
                return true;
            }
            return false;
        }

        public async Task<bool> ChangeState(long objectiveId, bool state, long userId)
        {
            var objective = await objectiveRepository.SingleOrDefault(p => p.UserId == userId && p.Id == objectiveId);
            if (objective is null) return false;

            if(objective.Finished != state)
            {
                objective.Finished = state;
                objectiveRepository.UpdateState(objective);

                if (await objectiveRepository.Save() > 0) return true;
            }
            return false;
        }

        public async Task<bool> Delete(long objectiveId, long userId)
        {
            var objective = await objectiveRepository.SingleOrDefault(o => o.UserId == userId && o.Id == objectiveId);
            if (objective is null) return false;

            objectiveRepository.Remove(objective);
            if (await objectiveRepository.Save() > 0) return true;
            return false;
        }

        public async Task<ObjectiveDto> Get(long objectiveId, long userId)
        {
            var objective = await objectiveRepository.SingleOrDefault(o => o.UserId == userId && o.Id == objectiveId);

            return mapper.Map<ObjectiveDto>(objective);
        }

        public async Task<IEnumerable<ObjectiveDto>> GetAll(long userId)
        {
            var objectives = objectiveRepository.Find(o => o.UserId == userId);

            return mapper.Map<IEnumerable<ObjectiveDto>>(objectives);
        }
    }
}
