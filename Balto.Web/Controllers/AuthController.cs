using System;
using System.Threading.Tasks;
using Balto.Service;
using Balto.Service.Handlers;
using Balto.Web.ViewModels;
using Microsoft.AspNetCore.Authorization;
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
        public async Task<ActionResult<AuthResponse>> UserLoginV1(AuthRequestLogin form)
        {
            string ipAddress = Request.HttpContext.Connection.RemoteIpAddress.ToString();

            try
            {
                var result = await userService.Authenticate(form.Email, form.Password, ipAddress);

                if (result.Status() == ResultStatus.NotFound) return NotFound();
                if (result.Status() == ResultStatus.NotPermited) return Forbid();
                if (result.Status() == ResultStatus.Sucess)
                {
                    logger.LogInformation($"User: { form.Email } ({ ipAddress }) authenticated sucessful.");
                    return Ok(new AuthResponse()
                    {
                        BearerToken = result.Result()
                    });
                }

                return BadRequest();            
            } 
            catch(Exception e)
            {
                logger.LogError(e, $"System failure on user: { form.Email } ({ ipAddress }) authentication.");
                return Problem();
            }
        }

        [HttpPost("register")]
        public async Task<IActionResult> UserRegisterV1(AuthRequestRegister form)
        {
            string ipAddress = Request.HttpContext.Connection.RemoteIpAddress.ToString();

            try
            {
                var result = await userService.Register(form.Email, form.Name, form.Password, ipAddress);

                if (result.Status() == ResultStatus.Conflict) return Conflict();
                if (result.Status() == ResultStatus.Sucess)
                {
                    logger.LogInformation($"User: { form.Email } ({ ipAddress }) sucessful registered!");
                    return Ok();
                }

                return BadRequest();
            } 
            catch(Exception e)
            {
                logger.LogError(e, $"System failure on user: { form.Email } ({ ipAddress }) registration.");
                return Problem();
            }
        }

        [HttpPost("reset")]
        [Authorize]
        public async Task<IActionResult> UserResetV1(AuthRequestReset form)
        {
            string ipAddress = Request.HttpContext.Connection.RemoteIpAddress.ToString();
            var user = await userService.GetUserFromPayload(User.Claims);

            try
            {
                var result = await userService.Reset(user.Email, form.Password);

                if (result.Status() == ResultStatus.NotFound) return NotFound();
                if (result.Status() == ResultStatus.Sucess) return Ok();
                return BadRequest();
            }
            catch (Exception e)
            {
                logger.LogError(e, $"System failure on user: { user.Email } ({ ipAddress }) password reset.");
                return Problem();
            }
        }
    }
}
