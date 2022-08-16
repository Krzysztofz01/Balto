using AutoMapper;
using Balto.API.Controllers.Base;
using Balto.Application.Tags;
using Balto.Infrastructure.Core.Abstraction;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using static Balto.Application.Tags.Dto;

namespace Balto.API.Controllers
{
    [Route("api/v{version:apiVersion}/tag")]
    [ApiVersion("1.0")]
    public class TagQueryController : QueryController
    {
        public TagQueryController(IDataAccess dataAccess, IMapper mapper, IMemoryCache memoryCache, ILogger<TagQueryController> logger) : base(dataAccess, mapper, memoryCache, logger)
        {
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<TagSimple>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll()
        {
            var response = await _dataAccess.Tags.GetAllTags();

            var mappedResponse = MapToDto<IEnumerable<TagSimple>>(response);

            return Ok(mappedResponse);
        }

        [HttpGet("{tagId}")]
        [ProducesResponseType(typeof(TagDetails), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetById(Guid tagId)
        {
            var response = await _dataAccess.Tags.GetTagById(tagId);

            var mappedResponse = MapToDto<TagDetails>(response);

            return Ok(mappedResponse);
        }
    }
}
