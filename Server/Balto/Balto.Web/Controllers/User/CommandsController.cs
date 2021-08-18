using Balto.Application.Aggregates.User;
using Balto.Web.Handlers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Balto.Web.Controllers.User
{
    [ApiController]
    [Route("api/v{version:apiVersion}/user")]
    [ApiVersion("1.0")]
    [Authorize(Roles = "Leader")]
    public class CommandsController : ControllerBase
    {
        private readonly UserService _userService;

        public CommandsController(UserService userService)
        {
            _userService = userService ??
                throw new ArgumentNullException(nameof(userService));
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteUser(Commands.V1.UserDelete request) =>
            await RequestHandler.HandleCommand(request, _userService.Handle);

        [HttpPut("activation")]
        public async Task<IActionResult> ActivateUser(Commands.V1.UserActivate request) =>
            await RequestHandler.HandleCommand(request, _userService.Handle);

        [HttpPut("team")]
        public async Task<IActionResult> UserTeamUpdate(Commands.V1.UserTeamUpdate request) =>
            await RequestHandler.HandleCommand(request, _userService.Handle);

        [HttpPut("color")]
        public async Task<IActionResult> UserColorUpdate(Commands.V1.UserColorUpdate request) =>
            await RequestHandler.HandleCommand(request, _userService.Handle);

        [HttpPut("leader")]
        public async Task<IActionResult> UserLeaderUpdate(Commands.V1.UserLeaderStatusUpdate request) =>
            await RequestHandler.HandleCommand(request, _userService.Handle);
    }
}
