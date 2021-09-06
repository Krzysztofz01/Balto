using AutoMapper;
using Balto.Application.Aggregates.Objectives;
using Balto.Infrastructure.SqlServer.Context;
using Balto.Web.Handlers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DomainObjective = Balto.Domain.Aggregates.Objective;
using static Balto.Application.Aggregates.Objectives.Dto.V1;


namespace Balto.Web.Controllers.Objective
{
    [ApiController]
    [Route("api/v{version:apiVersion}/objective")]
    [ApiVersion("1.0")]
    [Authorize]
    public class QueryController : ControllerBase
    {
        private readonly DbSet<DomainObjective.Objective> _objectives;
        private readonly IMapper _mapper;

        public QueryController(
            BaltoDbContext dbContext,
            IMapper mapper)
        {
            _objectives = dbContext.Set<DomainObjective.Objective>() ??
                throw new ArgumentNullException(nameof(dbContext));

            _mapper = mapper ??
                throw new ArgumentNullException(nameof(mapper));
        }

        [HttpGet]
        public async Task<IActionResult> GetAll() =>
            await RequestHandler.HandleMappedQuery<IEnumerable<DomainObjective.Objective>, IEnumerable<ObjectiveSimple>>(_objectives.GetAllObjectives, _mapper);

        [HttpGet("{objectiveId}")]
        public async Task<IActionResult> GetById(Guid objectiveId) =>
            await RequestHandler.HandleMappedQuery<Guid, DomainObjective.Objective, ObjectiveDetails>(objectiveId, _objectives.GetObjectiveById, _mapper);
    }
}
