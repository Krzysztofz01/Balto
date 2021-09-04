using Balto.Application.Aggregates.Objectives;
using Balto.Infrastructure.SqlServer.Context;
using Balto.Web.Handlers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace Balto.Web.Controllers.Objective
{
    [ApiController]
    [Route("api/v{version:apiVersion}/objective")]
    [ApiVersion("1.0")]
    public class QueryController : ControllerBase
    {
        private readonly DbSet<Domain.Aggregates.Objective.Objective> _objectives;

        public QueryController(BaltoDbContext dbContext)
        {
            _objectives = dbContext.Set<Domain.Aggregates.Objective.Objective>() ??
                throw new ArgumentNullException(nameof(dbContext));
        }

        [HttpGet]
        public async Task<IActionResult> GetAll() =>
            await RequestHandler.HandleQuery(_objectives.GetAllObjectives);

        [HttpGet("{objectiveId}")]
        public async Task<IActionResult> GetById(Guid objectiveId) =>
            await RequestHandler.HandleQuery(objectiveId, _objectives.GetObjectiveById);
    }
}
