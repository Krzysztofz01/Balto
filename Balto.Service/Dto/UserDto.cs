namespace Balto.Service.Dto
{
    public class UserDto
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public TeamDto Team { get; set; }
        public bool IsLeader { get; set; }
        public string Color { get; set; }
        public bool IsActivated { get; set; }
    }
}
