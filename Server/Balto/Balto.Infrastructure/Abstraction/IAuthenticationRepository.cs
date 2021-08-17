using Balto.Domain.Aggregates.User;
using System.Threading.Tasks;

namespace Balto.Infrastructure.Abstraction
{
    public interface IAuthenticationRepository
    {
        Task<User> GetUserByEmail(string email);
        Task<User> GetUserByRefreshToken(string refreshToken);
        Task<bool> UserWithEmailExists(string email);
    }
}
