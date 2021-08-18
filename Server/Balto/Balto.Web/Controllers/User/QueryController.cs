using Balto.Application.Aggregates.User;
using Balto.Infrastructure.SqlServer.Context;
using Balto.Web.Handlers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace Balto.Web.Controllers.User
{
    [ApiController]
    [Route("api/v{version:apiVersion}/user")]
    [ApiVersion("1.0")]
    [Authorize(Roles = "Leader")]
    public class QueryController : ControllerBase
    {
        private readonly DbSet<Domain.Aggregates.User.User> _users;

        public QueryController(BaltoDbContext dbContext)
        {
            _users = dbContext.Set<Domain.Aggregates.User.User>() ??
                throw new ArgumentNullException(nameof(dbContext));
        }

        [HttpGet]
        public async Task<IActionResult> GetAll() => 
            await RequestHandler.HandleQuery(_users.GetAllUsers);

        [HttpGet("{userId}")]
        public async Task<IActionResult> GetById(Guid userId) =>
            await RequestHandler.HandleQuery(userId, _users.GetUserById);

        [HttpGet("team/{teamId}")]
        public async Task<IActionResult> GetAllFromTeamById(Guid teamId) =>
            await RequestHandler.HandleQuery(teamId, _users.GetAllTeamUsers);

        [HttpGet("active")]
        public async Task<IActionResult> GetAllActive() =>
            await RequestHandler.HandleQuery(_users.GetAllUsersActivated);

        [HttpGet("inactive")]
        public async Task<IActionResult> GetAllInactive() =>
            await RequestHandler.HandleQuery(_users.GetAllUsersNotActivated);
    }
}
