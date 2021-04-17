namespace Balto.Service.Dto
{
    public class ProjectTableEntryDto
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Content { get; set; }
        public long Order { get; set; }
        public bool Finished { get; set; }
    }
}
