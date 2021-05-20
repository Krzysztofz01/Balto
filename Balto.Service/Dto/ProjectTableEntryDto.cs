using System;

namespace Balto.Service.Dto
{
    public class ProjectTableEntryDto
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Content { get; set; }
        public long Order { get; set; }
        public bool Finished { get; set; }
        public int Priority { get; set; }
        public UserDto UserAdded { get; set; }
        public UserDto UserFinished { get; set; }
        public DateTime StartingDate { get; set; }
        public DateTime EndingDate { get; set; }
        public DateTime? FinishDate { get; set; }
        public bool Notify { get; set; }
    }
}
