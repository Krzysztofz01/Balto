using Balto.Service.Dto;
using Balto.Service.Handlers;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Balto.Service
{
    public interface IUserService
    {
        Task<ServiceResult<AuthDto>> Authenticate(string email, string password, string ipAddress);
        Task<ServiceResult<AuthDto>> RefreshToken(string token, string ipAddress);
        Task<IServiceResult> RevokeToken(string token, string ipAddress);
        Task<IServiceResult> Register(string email, string name, string password, string ipAddress);
        Task<IServiceResult> Reset(string email, string password);

        Task<ServiceResult<IEnumerable<UserDto>>> GetAll();
        Task<ServiceResult<IEnumerable<UserDto>>> GetAllLeader();
        Task<ServiceResult<UserDto>> Get(long userId);
        Task<ServiceResult<UserDto>> GetLeader(long userId);
        Task<IServiceResult> Delete(long userId);
        Task<IServiceResult> Activate(long userId);

        Task<IServiceResult> UserSetTeam(long userId, long teamId, long leaderUserId);
        
        Task<UserDto> GetUserFromPayload(IEnumerable<Claim> claims);
        Task<long?> GetIdByEmail(string email);
    }
}
