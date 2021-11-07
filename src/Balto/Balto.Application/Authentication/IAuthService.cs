using System.Threading.Tasks;

namespace Balto.Application.Authentication
{
    public interface IAuthService
    {
        Task<Responses.V1.Login> Login(Requests.V1.Login request);
        Task<Responses.V1.Refresh> Refresh(Requests.V1.Refresh request);

        Task Logout(Requests.V1.Logout request);
        Task PasswordReset(Requests.V1.PasswordReset request);
        Task Register(Requests.V1.Register request);
    }
}
