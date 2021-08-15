using Balto.Domain.Aggregates.User;

namespace Balto.Infrastructure.Authentication
{
    public interface IAuthenticationHelperService
    {
        bool VerifyPasswordHash(string contractPassword, string passwordHash);
        string HashPassword(string contractPassword);
        bool CheckIfPasswordsMatch(string contractPassword, string contractPasswordRepeat);
        string GenerateJsonWebToken(User user);

    }
}
