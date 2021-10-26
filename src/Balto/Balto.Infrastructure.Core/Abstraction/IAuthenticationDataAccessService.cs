using System.Threading.Tasks;

namespace Balto.Infrastructure.Core.Abstraction
{
    public interface IAuthenticationDataAccessService
    {
        Task<User> GetUserByEmail(string email);
        Task<User> GetUserByRefreshToken(string refreshToken);
        Task<bool> UserWithEmailExsits(string email);
    }
}
