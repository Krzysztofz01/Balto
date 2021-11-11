using AutoMapper;
using Balto.API.Utility;
using Balto.Application.Identities;
using Balto.Domain.Identities;
using Balto.Infrastructure.Core.Abstraction;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using static Balto.Application.Identities.Dto;

namespace Balto.API.Controllers
{
    [ApiController]
    [Route("api/v{version:apiVersion}/identity")]
    [ApiVersion("1.0")]
    public class IdentityQueryController : ControllerBase
    {
        private readonly IDataAccess _dataAccess;
        private readonly IMapper _mapper;

        public IdentityQueryController(IDataAccess dataAccess, IMapper mapper)
        {
            _dataAccess = dataAccess ??
                throw new ArgumentNullException(nameof(dataAccess));

            _mapper = mapper ??
                throw new ArgumentNullException(nameof(mapper));
        }

        [HttpGet]
        [Authorize(Roles = "Admin, Support")]
        public async Task<IActionResult> GetAll() =>
            await RequestHandler.MapQuery<IEnumerable<Identity>, IEnumerable<IdentitySimple>>(_dataAccess.Identities.GetAllIdentities(), _mapper);

        [HttpGet("{identityId}")]
        [Authorize(Roles = "Admin, Support")]
        public async Task<IActionResult> GetById(Guid identityId) =>
            await RequestHandler.MapQuery<Identity, IdentityDetails>(_dataAccess.Identities.GetIdentityById(identityId), _mapper);

        [HttpGet("team/{teamId}")]
        [Authorize]
        public async Task<IActionResult> GeAllInTeam(Guid teamId) =>
            await RequestHandler.MapQuery<IEnumerable<Identity>, IEnumerable<IdentitySimple>>(_dataAccess.Identities.GetAllIdentitiesInTeam(teamId), _mapper);
    }
}
