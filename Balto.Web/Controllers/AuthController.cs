using System;
using System.Threading.Tasks;
using Balto.Service;
using Balto.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Balto.Web.Controllers
{
    [ApiController]
    [Route("api/v{version:apiVersion}/auth")]
    [ApiVersion("1.0")]
    public class AuthController : ControllerBase
    {
        private readonly IUserService userService;
        private readonly ILogger<AuthController> logger;

        public AuthController(
            IUserService userService,
            ILogger<AuthController> logger)
        {
            this.userService = userService ??
                throw new ArgumentNullException(nameof(userService));

            this.logger = logger ??
                throw new ArgumentNullException(nameof(logger));
        }

        [HttpPost]
        public async Task<ActionResult<AuthResponse>> UserLoginV1(UserForm form)
        {
            try
            {
                string ipAddress = Request.HttpContext.Connection.RemoteIpAddress.ToString();
                
                string token = await userService.Authenticate(form.Email, form.Password, ipAddress);
                if (token is null) return NotFound();

                logger.LogInformation($"User: { form.Email } authenticated sucessful.");
                return Ok(new AuthResponse()
                {
                    BearerToken = token
                });          
            } 
            catch(Exception e)
            {
                logger.LogError(e, $"System failure on user: { form.Email } authentication.");
                return StatusCode(500);
            }
        }

        [HttpPost("register")]
        public async Task<IActionResult> UserRegisterV1(UserForm form)
        {
            try
            {
                var ipAddress = Request.HttpContext.Connection.RemoteIpAddress.ToString();

                bool status = await userService.Register(form.Email, form.Password, ipAddress);
                if (!status) return Conflict();

                logger.LogInformation($"User: { form.Email } sucessful registered!");
                return Ok();

            } 
            catch(Exception e)
            {
                logger.LogError(e, $"System failure on user: { form.Email } registration.");
                return StatusCode(500);
            }
        }
    }
}
