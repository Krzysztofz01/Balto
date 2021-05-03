using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Balto.Service;
using Balto.Service.Handlers;
using Balto.Web.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Balto.Web.Controllers
{
    [ApiController]
    [Route("api/v{version:apiVersion}/user")]
    [ApiVersion("1.0")]
    public class UserController : ControllerBase
    {
        private readonly IUserService userService;
        private readonly IMapper mapper;
        private readonly ILogger<UserController> logger;

        public UserController(
            IUserService userService,
            IMapper mapper,
            ILogger<UserController> logger)
        {
            this.userService = userService ??
                throw new ArgumentNullException(nameof(userService));

            this.mapper = mapper ??
                throw new ArgumentNullException(nameof(mapper));

            this.logger = logger ??
                throw new ArgumentNullException(nameof(logger));
        }

        [HttpGet]
        [Authorize]
        public async Task<ActionResult<IEnumerable<UserGetView>>> GetAllV1()
        {
            try
            {
                var result = await userService.GetAll();
                if(result.Status() == ResultStatus.Sucess)
                {
                    var usersMapped = mapper.Map<IEnumerable<UserGetView>>(result.Result());
                    return Ok(usersMapped);
                }
                return BadRequest();
            }
            catch (Exception e)
            {
                logger.LogError(e, "System failure on getting all users!");
                return Problem();
            }
        }

        [HttpGet("{userId}")]
        [Authorize(Roles = "Leader")]
        public async Task<ActionResult<UserGetView>> GetByIdV1(long userId)
        {
            try
            {
                var result = await userService.Get(userId);

                if (result.Status() == ResultStatus.NotFound) return NotFound();
                if (result.Status() == ResultStatus.Sucess)
                {
                    var userMapped = mapper.Map<UserGetView>(result.Result());
                    return Ok(userMapped);
                }

                return BadRequest();
            }
            catch (Exception e)
            {
                logger.LogError(e, "System failure on getting user by id!");
                return Problem();
            }
        }


        [HttpDelete("{userId}")]
        [Authorize(Roles = "Leader")]
        public async Task<IActionResult> DeleteByIdV1(long userId)
        {
            try
            {
                var result = await userService.Delete(userId);

                if (result.Status() == ResultStatus.NotFound) return NotFound();
                if (result.Status() == ResultStatus.Sucess) return Ok();
                return BadRequest();
            }
            catch (Exception e)
            {
                logger.LogError(e, "System failure on deleting user by id!");
                return Problem();
            }
        }

        [HttpPatch("{userId}/team/{teamId}")]
        [Authorize(Roles = "Leader")]
        public async Task<IActionResult> PatchUserTeamV1(long userId, long teamId)
        {
            try
            {
                var user = await userService.GetUserFromPayload(User.Claims);

                var result = await userService.UserSetTeam(userId, teamId, user.Id);

                if (result.Status() == ResultStatus.NotFound) return NotFound();
                if (result.Status() == ResultStatus.Sucess) return Ok();
                return BadRequest();
            }
            catch (Exception e)
            {
                logger.LogError(e, "System failure on updating users team!");
                return Problem();
            }
        }
    }
}
