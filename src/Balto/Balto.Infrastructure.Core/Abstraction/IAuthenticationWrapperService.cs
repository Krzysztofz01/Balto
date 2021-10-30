using Balto.Domain.Identities;

namespace Balto.Infrastructure.Core.Abstraction
{
    public interface IAuthenticationWrapperService
    {
        bool VerifyPasswordHashes(string contractPasswordHash, string storedPasswordHash);
        bool VerifyStringHashes(string contractValue, string storedValue);
        string HashPassword(string contractPasswordString);
        string HashString(string contractValue);
        bool ValidatePasswords(string contractPasswordString, string contractPasswordStringRepeat);
        string GenerateJsonWebToken(Identity identity);
        string GenerateRefreshToken();
    }
}
