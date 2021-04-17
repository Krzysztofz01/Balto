using Balto.Service.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Balto.Service
{
    public interface IProjectTableEntryService
    {
        Task<IEnumerable<ProjectTableEntryDto>> GetAll(long projectTableId, long userId);
        Task<ProjectTableEntryDto> Get(long projectTableEntryId, long userId);
        Task<bool> Add(ProjectTableEntryDto projectTableEntry, long projectTableId, long userId);
        Task<bool> Delete(long projectTableEntryId, long userId);
        Task<bool> ChangeState(long projectTableEntryId, bool state, long userId);
        Task<bool> ChangeOrder(IEnumerable<long> entryIds, long projectTableId, long userId);
    }
}
