using Balto.Service.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Balto.Service
{
    public interface IProjectTableService
    {
        Task<IEnumerable<ProjectTableDto>> GetAll(long projectId, long userId);
        Task<ProjectTableDto> Get(long projectId, long projectTableId, long userId);
        Task<bool> Add(ProjectTableDto projectTable, long projectId, long userId);
        Task<bool> Delete(long projectId, long projectTableId, long userId);
        Task<bool> Update(ProjectTableDto projectTable, long projectId, long userId);
    }
}
