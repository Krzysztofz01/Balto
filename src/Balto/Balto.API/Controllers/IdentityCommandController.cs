using Balto.API.Controllers.Base;
using Balto.Application.Abstraction;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using static Balto.Application.Identities.Commands;

namespace Balto.API.Controllers
{
    [Route("api/v{version:apiVersion}/identity")]
    [ApiVersion("1.0")]
    [Authorize(Roles = "Admin")]
    public class IdentityCommandController : CommandController
    {
        private readonly IIdentityService _identityService;

        public IdentityCommandController(IIdentityService identityService) : base()
        {
            _identityService = identityService ??
                throw new ArgumentNullException(nameof(identityService));
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(V1.Delete command) =>
            await Command(command, _identityService.Handle);

        [HttpPut]
        public async Task<IActionResult> Update(V1.Update command) =>
            await Command(command, _identityService.Handle);

        [HttpPut("activation")]
        public async Task<IActionResult> Activation(V1.Activation command) =>
            await Command(command, _identityService.Handle);

        [HttpPut("role")]
        public async Task<IActionResult> RoleChange(V1.RoleChange command) =>
            await Command(command, _identityService.Handle);
    }
}
