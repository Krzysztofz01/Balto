using AutoMapper;
using Balto.API.Utility;
using Balto.Application.Goals;
using Balto.Domain.Goals;
using Balto.Infrastructure.Core.Abstraction;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using static Balto.Application.Goals.Dto;

namespace Balto.API.Controllers
{
    [ApiController]
    [Route("api/v{version:apiVersion}/goal")]
    [ApiVersion("1.0")]
    [Authorize]
    public class GoalQueryController : ControllerBase
    {
        private readonly IDataAccess _dataAccess;
        private readonly IMapper _mapper;

        public GoalQueryController(IDataAccess dataAccess, IMapper mapper)
        {
            _dataAccess = dataAccess ??
                throw new ArgumentNullException(nameof(dataAccess));

            _mapper = mapper ??
                throw new ArgumentNullException(nameof(mapper));
        }

        [HttpGet]
        public async Task<IActionResult> GetAll() =>
            await RequestHandler.MapQuery<IEnumerable<Goal>, IEnumerable<GoalSimple>>(_dataAccess.Goals.GetAllGoals(), _mapper);

        [HttpGet("{goalId}")]
        public async Task<IActionResult> GetById(Guid goalId) =>
            await RequestHandler.MapQuery<Goal, GoalDetails>(_dataAccess.Goals.GetGoalById(goalId), _mapper);

        [HttpGet("recurring")]
        public async Task<IActionResult> GetAllRecurring() =>
            await RequestHandler.MapQuery<IEnumerable<Goal>, IEnumerable<GoalSimple>>(_dataAccess.Goals.GetAllRecurringGoals(), _mapper);

        [HttpGet("nonrecurring")]
        public async Task<IActionResult> GetAllNonRecurring() =>
            await RequestHandler.MapQuery<IEnumerable<Goal>, IEnumerable<GoalSimple>>(_dataAccess.Goals.GetAllNonRecurringGoals(), _mapper);
    }
}
