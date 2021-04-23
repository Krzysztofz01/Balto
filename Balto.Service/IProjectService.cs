using Balto.Service.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Balto.Service
{
    public interface IProjectService
    {
        Task<IEnumerable<ProjectDto>> GetAll(long userId);
        Task<ProjectDto> Get(long projectId, long userId);
        Task<bool> Add(ProjectDto project, long userId);
        Task<bool> Delete(long projectId, long userId);
        Task<bool> Update(ProjectDto project, long userId);
    }
}
