using System.Collections.Generic;

namespace Balto.Service.Dto
{
    public class ProjectTableDto
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public IEnumerable<ProjectTableEntryDto> Entries { get; set; }
    }
}
