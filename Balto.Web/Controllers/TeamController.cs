using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Balto.Service;
using Balto.Service.Dto;
using Balto.Service.Handlers;
using Balto.Web.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Balto.Web.Controllers
{
    [ApiController]
    [Route("api/v{version:apiVersion}/team")]
    [ApiVersion("1.0")]
    public class TeamController : ControllerBase
    {
        private readonly ITeamService teamService;
        private readonly IUserService userService;
        private readonly IMapper mapper;
        private readonly ILogger<TeamController> logger;

        public TeamController(
            ITeamService teamService,
            IUserService userService,
            IMapper mapper,
            ILogger<TeamController> logger)
        {
            this.teamService = teamService ??
                throw new ArgumentNullException(nameof(teamService));

            this.userService = userService ??
                throw new ArgumentNullException(nameof(userService));

            this.mapper = mapper ??
                throw new ArgumentNullException(nameof(mapper));

            this.logger = logger ??
                throw new ArgumentNullException(nameof(logger));
        }

        [HttpGet]
        [Authorize]
        public async Task<ActionResult<IEnumerable<TeamGetView>>> GetOneForDefaultAllForLeaderV1()
        {
            //If a default role user hits this endpont he will
            //receive only the team that he is assigned to
            //The leader will receive all teams

            try
            {
                var user = await userService.GetUserFromPayload(User.Claims);

                if(user.IsLeader)
                {
                    var leaderResult = await teamService.GetAll(user.Id);

                    if(leaderResult.Status() == ResultStatus.Sucess)
                    {
                        var teamsMapped = mapper.Map<IEnumerable<TeamGetView>>(leaderResult.Result());
                        return Ok(teamsMapped);
                    }
                    return BadRequest();
                }

                var defaultResult = await teamService.GetUsersTeam(user.Id);

                if (defaultResult.Status() == ResultStatus.NotFound) return NotFound();
                if (defaultResult.Status() == ResultStatus.Sucess)
                {
                    var teamMapped = mapper.Map<IEnumerable<TeamGetView>>(new List<TeamDto> { defaultResult.Result() });
                    return Ok(teamMapped);
                }
                
                return BadRequest();
            }
            catch (Exception e)
            {
                logger.LogError(e, "System failure on getting teams!");
                return Problem();
            }
        }

        [HttpPost]
        [Authorize(Roles = "Leader")]
        public async Task<IActionResult> PostOneV1(TeamPostView team)
        {
            try
            {
                var user = await userService.GetUserFromPayload(User.Claims);

                var teamMapped = mapper.Map<TeamDto>(team);

                var result = await teamService.Add(teamMapped, user.Id);

                if (result.Status() == ResultStatus.Sucess) return Ok();
                return BadRequest();
            }
            catch (Exception e)
            {
                logger.LogError(e, "System failure on posting team!");
                return Problem();
            }
        }

        [HttpGet("{teamId}")]
        [Authorize(Roles = "Leader")]
        public async Task<ActionResult<TeamGetView>> GetByIdV1(long teamId)
        {
            try
            {
                var user = await userService.GetUserFromPayload(User.Claims);

                var result = await teamService.Get(teamId, user.Id);

                if (result.Status() == ResultStatus.NotFound) return NotFound();
                if (result.Status() == ResultStatus.Sucess)
                {
                    var teamMapped = mapper.Map<TeamGetView>(result.Result());
                    return Ok(teamMapped);
                }

                return BadRequest();
            }
            catch (Exception e)
            {
                logger.LogError(e, "System failure on getting team by id!");
                return Problem();
            }
        }

        [HttpDelete("{teamId}")]
        [Authorize(Roles = "Leader")]
        public async Task<IActionResult> DeleteByIdV1(long teamId)
        {
            try
            {
                var user = await userService.GetUserFromPayload(User.Claims);

                var result = await teamService.Delete(teamId, user.Id);

                if (result.Status() == ResultStatus.NotFound) return NotFound();
                if (result.Status() == ResultStatus.Sucess) return Ok();
                return BadRequest();
            }
            catch (Exception e)
            {
                logger.LogError(e, "System failure on deleting team by id!");
                return Problem();
            }
        }

        [HttpPatch("{teamId}")]
        [Authorize(Roles = "Leader")]
        public async Task<IActionResult> UpdateByIdV1(long teamId, [FromBody]TeamPatchView team)
        {
            try
            {
                var user = await userService.GetUserFromPayload(User.Claims);

                var teamMapped = mapper.Map<TeamDto>(team);

                var result = await teamService.Update(teamMapped, teamId, user.Id);

                if (result.Status() == ResultStatus.NotFound) return NotFound();
                if (result.Status() == ResultStatus.Sucess) return Ok();
                return BadRequest();
            }
            catch (Exception e)
            {
                logger.LogError(e, "System failure on updating team by id!");
                return Problem();
            }
        }
    }
}
