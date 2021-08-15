using System.Threading.Tasks;

namespace Balto.Domain.Aggregates.User
{
    public interface IAuthenticationRepository
    {
        Task<User> GetUserByEmail(string email);
        Task<User> GetUserByRefreshToken(string refreshToken);
    }
}
