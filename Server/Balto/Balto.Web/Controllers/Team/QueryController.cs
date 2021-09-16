using AutoMapper;
using Balto.Application.Aggregates.Team;
using Balto.Infrastructure.SqlServer.Context;
using Balto.Web.Handlers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TeamDomain = Balto.Domain.Aggregates.Team;
using static Balto.Application.Aggregates.Team.Dto.V1;

namespace Balto.Web.Controllers.Team
{
    [ApiController]
    [Route("api/v{version:apiVersion}/team")]
    [ApiVersion("1.0")]
    [Authorize]
    public class QueryController : ControllerBase
    {
        private readonly DbSet<TeamDomain.Team> _teams;
        private readonly IMapper _mapper;

        public QueryController(
            BaltoDbContext dbContext,
            IMapper mapper)
        {
            _teams = dbContext.Set<TeamDomain.Team>() ??
                throw new ArgumentNullException(nameof(dbContext));

            _mapper = mapper ??
                throw new ArgumentNullException(nameof(mapper));
        }

        [HttpGet]
        public async Task<IActionResult> GetAll() =>
            await RequestHandler.HandleMappedQuery<IEnumerable<TeamDomain.Team>, IEnumerable<TeamSimple>>(_teams.GetAllTeams, _mapper);

        [HttpGet("{teamId}")]
        public async Task<IActionResult> GetById(Guid teamId) =>
            await RequestHandler.HandleMappedQuery<Guid, TeamDomain.Team, TeamDetails>(teamId, _teams.GetTeamById, _mapper);
    }
}
