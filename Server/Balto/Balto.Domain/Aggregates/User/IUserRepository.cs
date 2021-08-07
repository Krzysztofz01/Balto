using System.Threading.Tasks;

namespace Balto.Domain.Aggregates.User
{
    public interface IUserRepository
    {
        Task<User> Load(UserId id);
        Task Add(User entity);
        Task<bool> Exists(UserId id);
    }
}
