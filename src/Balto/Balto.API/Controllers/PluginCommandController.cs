using Balto.API.Controllers.Base;
using Balto.Application.Plugin.TrelloIntegration;
using Balto.Application.Plugin.TrelloIntegration.Abstraction;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace Balto.API.Controllers
{
    [Route("api/v{version:apiVersion}/plugin")]
    [ApiVersion("1.0")]
    public class PluginCommandController : CommandController
    {
        private readonly ITrelloIntegration _trelloIntegration;

        public PluginCommandController(ITrelloIntegration trelloIntegration, ILogger<PluginCommandController> logger) : base(logger)
        {
            _trelloIntegration = trelloIntegration ??
                throw new ArgumentNullException(nameof(trelloIntegration));
        }

        [HttpPost("trello")]
        public async Task<IActionResult> CreateProjectFromTrello([FromForm] Commands.CreateProjectFromTrelloBoard command) =>
            await ExecuteService(command, _trelloIntegration.Handle);
    }
}
