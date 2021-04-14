using Balto.Service.Dto;
using System.Threading.Tasks;

namespace Balto.Service
{
    public interface IUserService
    {
        Task<UserDto> Authenticate(string email, string password);
        Task<bool> Register(string email, string password);
    }
}
