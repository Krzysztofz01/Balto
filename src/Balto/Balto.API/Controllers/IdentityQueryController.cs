using AutoMapper;
using Balto.API.Controllers.Base;
using Balto.Application.Identities;
using Balto.Infrastructure.Core.Abstraction;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using static Balto.Application.Identities.Dto;

namespace Balto.API.Controllers
{
    [Route("api/v{version:apiVersion}/identity")]
    [ApiVersion("1.0")]
    public class IdentityQueryController : QueryController
    {
        public IdentityQueryController(IDataAccess dataAccess, IMapper mapper, IMemoryCache memoryCache) : base(dataAccess, mapper, memoryCache)
        {
        }

        [HttpGet]
        [Authorize(Roles = "Admin, Support")]
        [ProducesResponseType(typeof(IEnumerable<IdentitySimple>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll()
        {
            var response = await _dataAccess.Identities.GetAllIdentities();

            var mappedResponse = MapToDto<IEnumerable<IdentitySimple>>(response);

            return Ok(mappedResponse);
        }

        [HttpGet("{identityId}")]
        [Authorize(Roles = "Admin, Support")]
        [ProducesResponseType(typeof(IdentityDetails), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetById(Guid identityId)
        {
            var response = await _dataAccess.Identities.GetIdentityById(identityId);

            var mappedResponse = MapToDto<IdentityDetails>(response);

            return Ok(mappedResponse);
        }
    }
}
