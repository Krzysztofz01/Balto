using System;
using System.Threading.Tasks;
using Balto.Service;
using Balto.Service.Handlers;
using Balto.Web.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
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
                    var auth = result.Result();

                    //Set the cookie for refresh token
                    Response.Cookies.Append("refreshToken", auth.RefreshToken, new CookieOptions
                    {
                        HttpOnly = true,
                        Expires = DateTime.UtcNow.AddDays(7)
                    });

                    //Return the token 
                    return Ok(new AuthResponse()
                    {
                        BearerToken = auth.Token
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

        [HttpPost("refresh")]
        public async Task<ActionResult<AuthResponse>> RefreshTokenV1()
        {
            string ipAddress = Request.HttpContext.Connection.RemoteIpAddress.ToString();

            try
            {
                var refreshToken = Request.Cookies["refreshToken"];

                var result = await userService.RefreshToken(refreshToken, ipAddress);

                if (result.Status() == ResultStatus.NotFound) return NotFound();
                if (result.Status() == ResultStatus.NotPermited) return Forbid();
                if (result.Status() == ResultStatus.Sucess)
                {
                    var auth = result.Result();
                    logger.LogInformation($"User: { auth.Email } ({ ipAddress }) refreshed the token.");

                    //Set the cookie for refresh token
                    Response.Cookies.Append("refreshToken", auth.RefreshToken, new CookieOptions
                    {
                        HttpOnly = true,
                        Expires = DateTime.UtcNow.AddDays(7)
                    });

                    //Return the token 
                    return Ok(new AuthResponse()
                    {
                        BearerToken = auth.Token
                    });
                }

                return BadRequest();
            }
            catch (Exception e)
            {
                logger.LogError(e, $"System failure on refreshing token ({ ipAddress }).");
                return Problem();
            }
        }

        [HttpPost("revoke")]
        [Authorize]
        public async Task<IActionResult> RevokeTokenV1()
        {
            string ipAddress = Request.HttpContext.Connection.RemoteIpAddress.ToString();
            var user = await userService.GetUserFromPayload(User.Claims);

            try
            {
                var refreshToken = Request.Cookies["refreshToken"];
                if (string.IsNullOrEmpty(refreshToken)) return BadRequest();
                
                var result = await userService.RevokeToken(refreshToken, ipAddress);
                
                if (result.Status() == ResultStatus.Sucess) return Ok();
                return BadRequest();
            }
            catch (Exception e)
            {
                logger.LogError(e, $"System failure on revoking token of: { user.Email } ({ ipAddress }).");
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
