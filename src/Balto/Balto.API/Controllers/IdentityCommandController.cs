using Balto.API.Utility;
using Balto.Application.Abstraction;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using static Balto.Application.Identities.Commands;

namespace Balto.API.Controllers
{
    [ApiController]
    [Route("api/v{version:apiVersion}/identity")]
    [ApiVersion("1.0")]
    [Authorize(Roles = "Admin")]
    public class IdentityCommandController : ControllerBase
    {
        private readonly IIdentityService _identityService;

        public IdentityCommandController(IIdentityService identityService)
        {
            _identityService = identityService ??
                throw new ArgumentNullException(nameof(identityService));
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(V1.Delete command) =>
            await RequestHandler.Command(command, _identityService.Handle);

        [HttpPut]
        public async Task<IActionResult> Update(V1.Update command) =>
            await RequestHandler.Command(command, _identityService.Handle);

        [HttpPut("activation")]
        public async Task<IActionResult> Activation(V1.Activation command) =>
            await RequestHandler.Command(command, _identityService.Handle);

        [HttpPut("role")]
        public async Task<IActionResult> RoleChange(V1.RoleChange command) =>
            await RequestHandler.Command(command, _identityService.Handle);

        [HttpPut("team")]
        public async Task<IActionResult> TeamChange(V1.TeamChange command) =>
            await RequestHandler.Command(command, _identityService.Handle);
    }
}
