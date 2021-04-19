using Balto.Service.Dto;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Balto.Service
{
    public interface IUserService
    {
        Task<string> Authenticate(string email, string password, string ipAddress);
        Task<bool> Register(string email, string password, string ipAddress);
        Task<UserDto> GetUserFromPayload(IEnumerable<Claim> claims);
    }
}
