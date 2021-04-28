using System;
using System.Threading.Tasks;
using Balto.Service;
using Balto.Service.Integration.Trello;
using Balto.Web.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Balto.Web.Controllers
{
    [ApiController]
    [Route("api/v{version:apiVersion}/integration/trello")]
    [ApiVersion("1.0")]
    public class TrelloIntegrationController : ControllerBase
    {
        private readonly ITrelloIntegrationService trelloIntegrationService;
        private readonly IUserService userService;
        private readonly ILogger<TrelloIntegrationController> logger;

        public TrelloIntegrationController(
            ITrelloIntegrationService trelloIntegrationService,
            IUserService userService,
            ILogger<TrelloIntegrationController> logger)
        {
            this.trelloIntegrationService = trelloIntegrationService ??
                throw new ArgumentNullException(nameof(trelloIntegrationService));

            this.userService = userService ??
                throw new ArgumentNullException(nameof(userService));

            this.logger = logger ??
                throw new ArgumentNullException(nameof(logger));
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> IntegrateTrelloProjectV1([FromForm] TrelloIntegrationPostView trelloIntegrationPostView)
        {
            try
            {
                var user = await userService.GetUserFromPayload(User.Claims);

                throw new NotImplementedException();
            }
            catch(Exception e)
            {
                logger.LogError(e, "System failure on Trello to Balto project migration!");
                return Problem();
            }
        }
    }
}
