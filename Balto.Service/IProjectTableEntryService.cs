using Balto.Service.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Balto.Service
{
    public interface IProjectTableEntryService
    {
        Task<IEnumerable<ProjectTableEntryDto>> GetAll(long projectTableId);
        Task<ProjectTableEntryDto> Get(long projectTableEntryId);
        Task<bool> Add(ProjectTableEntryDto projectTableEntry);
        Task<bool> Delete(long projectTableEntryId);
        Task<bool> Update(ProjectTableEntryDto projectTableEntry);
        Task<bool> ChangeState(long projectTableEntryId, bool state);
    }
}
