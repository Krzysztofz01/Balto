using AutoMapper;
using Balto.API.Controllers.Base;
using Balto.Application.Goals;
using Balto.Infrastructure.Core.Abstraction;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using static Balto.Application.Goals.Dto;

namespace Balto.API.Controllers
{
    [Route("api/v{version:apiVersion}/goal")]
    [ApiVersion("1.0")]
    public class GoalQueryController : QueryController
    {
        public GoalQueryController(IDataAccess dataAccess, IMapper mapper, IMemoryCache memoryCache) : base (dataAccess, mapper, memoryCache)
        {
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var response = await _dataAccess.Goals.GetAllGoals();

            var mappedResponse = MapToDto<IEnumerable<GoalSimple>>(response);

            return Ok(mappedResponse);
        }

        [HttpGet("{goalId}")]
        public async Task<IActionResult> GetById(Guid goalId)
        {
            var response = await _dataAccess.Goals.GetGoalById(goalId);

            var mappedResponse = MapToDto<GoalDetails>(response);

            return Ok(mappedResponse);
        }

        [HttpGet("recurring")]
        public async Task<IActionResult> GetAllRecurring()
        {
            var response = await _dataAccess.Goals.GetAllRecurringGoals();

            var mappedResponse = MapToDto<IEnumerable<GoalSimple>>(response);

            return Ok(mappedResponse);
        }

        [HttpGet("nonrecurring")]
        public async Task<IActionResult> GetAllNonRecurring()
        {
            var response = await _dataAccess.Goals.GetAllNonRecurringGoals();

            var mappedResponse = MapToDto<IEnumerable<GoalSimple>>(response);

            return Ok(mappedResponse);
        }
    }
}
