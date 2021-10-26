namespace Balto.Infrastructure.Core.Abstraction
{
    public interface IAuthenticationWrapperService
    {
        bool VerifyPasswordHashes(string contractPasswordHash, string storedPasswordHash);
        string HashPassword(string contractPasswordString);
        bool ValidatePasswords(string contractPasswordString, string contractPasswordStringRepeat);
        string GenerateJsonWebToken(User user);
    }
}
