using Balto.Service.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Balto.Service
{
    public interface IProjectTableEntryService
    {
        Task<IEnumerable<ProjectTableEntryDto>> GetAll(long projectId, long projectTableId, long userId);
        Task<ProjectTableEntryDto> Get(long projectId, long projectTableId, long projectTableEntryId, long userId);
        Task<bool> Add(ProjectTableEntryDto projectTableEntry, long projectId, long projectTableId, long userId);
        Task<bool> Delete(long projectId, long projectTableId, long projectTableEntryId, long userId);
        Task<bool> Update(ProjectTableEntryDto projectTableEntry, long projectId, long projectTableId, long userId);
        Task<bool> ChangeState(long projectId, long projectTableId, long projectTableEntryId, long userId);
        Task<bool> ChangeOrder(IEnumerable<long> entryIds, long projectId, long projectTableId, long userId);
    }
}
