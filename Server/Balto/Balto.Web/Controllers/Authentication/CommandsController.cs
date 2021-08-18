using Balto.Application.Authentication;
using Balto.Web.Handlers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Balto.Web.Controllers.Authentication
{
    [ApiController]
    [Route("api/v{version:apiVersion}/auth")]
    [ApiVersion("1.0")]
    public class CommandsController : ControllerBase
    {
        private readonly AuthenticationService _authenticationService;

        public CommandsController(AuthenticationService authenticationService)
        {
            _authenticationService = authenticationService ??
                throw new ArgumentNullException(nameof(authenticationService));
        }

        [HttpPost]
        public async Task<IActionResult> UserLogin(Commands.V1.UserLogin request) =>
            await RequestHandler.HandleResultCommand(request, _authenticationService.HandleWithResponse);

        [HttpPost("register")]
        public async Task<IActionResult> UserRegister(Commands.V1.UserRegister request) =>
            await RequestHandler.HandleResultCommand(request, _authenticationService.HandleWithResponse);

        [Authorize]
        [HttpPost("logout")]
        public async Task<IActionResult> UserLogout(Commands.V1.UserLogout request) =>
            await RequestHandler.HandleCommand(request, _authenticationService.Handle);

        [HttpPost("refresh")]
        public async Task<IActionResult> UserRefresh(Commands.V1.UserRefresh request) =>
            await RequestHandler.HandleCommand(request, _authenticationService.Handle);

        [Authorize]
        [HttpPost("reset")]
        public async Task<IActionResult> UserReset(Commands.V1.UserResetPassword request) =>
            await RequestHandler.HandleCommand(request, _authenticationService.Handle);
    }
}
