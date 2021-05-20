namespace Balto.Web.ViewModels
{
    public class UserGetView
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public TeamGetView Team { get; set; }
        public bool IsLeader { get; set; }
        public string Color { get; set; }
    }
}
