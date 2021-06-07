using Balto.Service.Dto;
using Balto.Service.Handlers;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Balto.Service
{
    public interface IProjectTableEntryService
    {
        Task<ServiceResult<IEnumerable<ProjectTableEntryDto>>> GetAll(long projectId, long projectTableId, long userId);
        Task<ServiceResult<ProjectTableEntryDto>> Get(long projectId, long projectTableId, long projectTableEntryId, long userId);
        Task<IServiceResult> Add(ProjectTableEntryDto projectTableEntry, long projectId, long projectTableId, long userId);
        Task<IServiceResult> Delete(long projectId, long projectTableId, long projectTableEntryId, long userId);
        Task<IServiceResult> Update(ProjectTableEntryDto projectTableEntry, long projectId, long projectTableId, long projectTableEntryId, long userId);
        Task<IServiceResult> ChangeState(long projectId, long projectTableId, long projectTableEntryId, long userId);
        Task<IServiceResult> ChangeOrder(IEnumerable<long> entryIds, long projectId, long projectTableId, long userId);
        Task<IServiceResult> MoveToTable(long projectId, long projectTableId, long projectTableEntryId, long newProjectTableId, long userId);

        Task<ServiceResult<IEnumerable<ProjectTableEntryDto>>> IncomingEntriesDay();
        Task<ServiceResult<IEnumerable<ProjectTableEntryDto>>> IncomingEntriesWeek();
    }
}
