using Balto.Service.Dto;
using Balto.Service.Handlers;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Balto.Service
{
    public interface IUserService
    {
        Task<ServiceResult<string>> Authenticate(string email, string password, string ipAddress);
        Task<IServiceResult> Register(string email, string password, string ipAddress);
        Task<IServiceResult> Reset(string email, string password);
        Task<ServiceResult<IEnumerable<UserDto>>> GetUsers(long leaderUserId);
        Task<IServiceResult> UserSetTeam(long userId, long teamId, long leaderUserId);
        
        Task<UserDto> GetUserFromPayload(IEnumerable<Claim> claims);
        Task<long?> GetIdByEmail(string email);
    }
}
