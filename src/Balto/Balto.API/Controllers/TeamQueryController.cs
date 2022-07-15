using AutoMapper;
using Balto.API.Controllers.Base;
using Balto.Application.Teams;
using Balto.Infrastructure.Core.Abstraction;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using static Balto.Application.Teams.Dto;

namespace Balto.API.Controllers
{
    public class TeamQueryController : QueryController
    {
        public TeamQueryController(IDataAccess dataAccess, IMapper mapper, IMemoryCache memoryCache) : base(dataAccess, mapper, memoryCache)
        {
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var response = await _dataAccess.Teams.GetAllTeams();

            var mappedResponse = MapToDto<IEnumerable<TeamSimple>>(response);

            return Ok(mappedResponse);
        }

        [HttpGet("{teamId}")]
        public async Task<IActionResult> GetById(Guid teamId)
        {
            var response = await _dataAccess.Teams.GetTeamById(teamId);

            var mappedResponse = MapToDto<TeamDetails>(response);

            return Ok(mappedResponse);
        }

        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetByUserId(Guid userId)
        {
            var response = await _dataAccess.Teams.GetTeamsByUserId(userId);

            var mappedResponse = MapToDto<IEnumerable<TeamSimple>>(response);

            return Ok(mappedResponse);
        }
    }
}
