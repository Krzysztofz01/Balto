using System.Collections.Generic;

namespace Balto.Service.Dto
{
    public class ProjectDto
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public long OwnerId { get; set; }
        public UserDto Owner { get; set; }
        public IEnumerable<UserDto> ReadWriteUsers { get; set; }
        public IEnumerable<ProjectTableDto> Tabels { get; set; }
    }
}
