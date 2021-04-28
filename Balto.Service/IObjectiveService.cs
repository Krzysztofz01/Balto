using Balto.Service.Dto;
using Balto.Service.Handlers;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Balto.Service
{
    public interface IObjectiveService
    {
        Task<ServiceResult<IEnumerable<ObjectiveDto>>> GetAll(long userId);
        Task<ServiceResult<ObjectiveDto>> Get(long objectiveId, long userId);
        Task<IServiceResult> Add(ObjectiveDto objective, long userId);
        Task<IServiceResult> Delete(long objectiveId, long userId);
        Task<IServiceResult> ChangeState(long objectiveId, long userId);
        Task<int> ResetDaily();
    }
}
