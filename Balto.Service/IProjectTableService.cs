using Balto.Service.Dto;
using Balto.Service.Handlers;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Balto.Service
{
    public interface IProjectTableService
    {
        Task<ServiceResult<IEnumerable<ProjectTableDto>>> GetAll(long projectId, long userId);
        Task<ServiceResult<ProjectTableDto>> Get(long projectId, long projectTableId, long userId);
        Task<IServiceResult> Add(ProjectTableDto projectTable, long projectId, long userId);
        Task<IServiceResult> Delete(long projectId, long projectTableId, long userId);
        Task<IServiceResult> Update(ProjectTableDto projectTable, long projectId, long projectTableId, long userId);
    }
}
