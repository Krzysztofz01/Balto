using Balto.Service.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Balto.Service
{
    public interface IObjectiveService
    {
        Task<IEnumerable<ObjectiveDto>> GetAll(long userId);
        Task<ObjectiveDto> Get(long objectiveId, long userId);
        Task<bool> Add(ObjectiveDto objective);
        Task<bool> Delete(long objectiveId, long userId);
        Task<bool> ChangeState(long objectiveId, long userId);
    }
}
