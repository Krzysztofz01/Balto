using AutoMapper;
using Balto.Application.Aggregates.User;
using Balto.Infrastructure.SqlServer.Context;
using Balto.Web.Handlers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UserDomain = Balto.Domain.Aggregates.User;
using static Balto.Application.Aggregates.User.Dto.V1;

namespace Balto.Web.Controllers.User
{
    [ApiController]
    [Route("api/v{version:apiVersion}/user")]
    [ApiVersion("1.0")]
    [Authorize(Roles = "Leader")]
    public class QueryController : ControllerBase
    {
        private readonly DbSet<UserDomain.User> _users;
        private readonly IMapper _mapper;

        public QueryController(
            BaltoDbContext dbContext,
            IMapper mapper)
        {
            _users = dbContext.Set<UserDomain.User>() ??
                throw new ArgumentNullException(nameof(dbContext));

            _mapper = mapper ??
                throw new ArgumentNullException(nameof(mapper));
        }

        [HttpGet]
        public async Task<IActionResult> GetAll() =>
            await RequestHandler.HandleMappedQuery<IEnumerable<UserDomain.User>, IEnumerable<UserSimple>>(_users.GetAllUsers, _mapper);

        [HttpGet("{userId}")]
        public async Task<IActionResult> GetById(Guid userId) =>
            await RequestHandler.HandleMappedQuery<Guid, UserDomain.User, UserDetails>(userId, _users.GetUserById, _mapper);

        [HttpGet("team/{teamId}")]
        public async Task<IActionResult> GetAllFromTeamById(Guid teamId) =>
            await RequestHandler.HandleMappedQuery<Guid, IEnumerable<UserDomain.User>, IEnumerable<UserSimple>>(teamId, _users.GetAllTeamUsers, _mapper);

        [HttpGet("active")]
        public async Task<IActionResult> GetAllActive() =>
            await RequestHandler.HandleMappedQuery<IEnumerable<UserDomain.User>, IEnumerable<UserSimple>>(_users.GetAllUsersActivated, _mapper);

        [HttpGet("inactive")]
        public async Task<IActionResult> GetAllInactive() =>
            await RequestHandler.HandleMappedQuery<IEnumerable<UserDomain.User>, IEnumerable<UserSimple>>(_users.GetAllUsersNotActivated, _mapper);
    }
}
