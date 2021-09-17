using Balto.Application.Integrations.Trello;
using Balto.Web.Handlers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Balto.Web.Controllers.Integration
{
    [ApiController]
    [Route("api/v{version:apiVersion}/integration")]
    [ApiVersion("1.0")]
    [Authorize]
    public class CommandsController : ControllerBase
    {
        private readonly TrelloService _trelloService;

        public CommandsController(TrelloService trelloService)
        {
            _trelloService = trelloService ??
                throw new ArgumentNullException(nameof(trelloService));
        }

        [HttpPost("trello")]
        public async Task<IActionResult> AddProject(Commands.V1.TrelloBoardAdd request) =>
            await RequestHandler.HandleCommand(request, _trelloService.Handle);
    }
}
