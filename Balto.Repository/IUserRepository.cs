using Balto.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Balto.Repository
{
    public interface IUserRepository : IRepository<User>
    {
        Task<bool> IsLeader(long userId);
        IEnumerable<User> GetAllUsers();
        IEnumerable<User> GetAllUsersLeader();
        Task<User> GetSingleUser(long userId);
        Task<User> GetUserWithToken(string token);
        Task<User> GetSingleUserByEmail(string email);
    }
}
