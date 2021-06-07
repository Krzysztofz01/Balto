using Balto.Service.Dto;
using Balto.Service.Handlers;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Balto.Service
{
    public interface IProjectService
    {
        Task<ServiceResult<IEnumerable<ProjectDto>>> GetAll(long userId);
        Task<ServiceResult<ProjectDto>> Get(long projectId, long userId);
        Task<IServiceResult> Add(ProjectDto project, long userId);
        Task<IServiceResult> Delete(long projectId, long userId);
        Task<IServiceResult> Update(ProjectDto project, long projectId, long userId);
        Task<IServiceResult> InviteUser(long projectId, string collaboratorEmail, long userId);
        Task<IServiceResult> Leave(long projectId, long userId);
    }
}
