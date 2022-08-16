using AutoMapper;
using Balto.Application.Logging;
using Balto.Infrastructure.Core.Abstraction;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using System;

namespace Balto.API.Controllers.Base
{
    [ApiController]
    [Produces("application/json")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public abstract class QueryController : ControllerBase
    {
        protected readonly IDataAccess _dataAccess;
        protected readonly IMapper _mapper;
        protected readonly IMemoryCache _memoryCache;
        protected readonly ILogger<QueryController> _logger;

        public QueryController(IDataAccess dataAccess, IMapper mapper, IMemoryCache memoryCache, ILogger<QueryController> logger)
        {
            _dataAccess = dataAccess ??
                throw new ArgumentNullException(nameof(dataAccess));

            _mapper = mapper ??
                throw new ArgumentNullException(nameof(mapper));

            _memoryCache = memoryCache ??
                throw new ArgumentNullException(nameof(memoryCache));

            _logger = logger ??
                throw new ArgumentNullException(nameof(logger));
        }

        protected object ResolveFromCache()
        {
            var key = Request.GetEncodedPathAndQuery();

            if (_memoryCache.TryGetValue(key, out object cachedResponse)) return cachedResponse;

            return null;
        }

        protected void StoreToCache(object response)
        {
            var key = Request.GetEncodedPathAndQuery();

            var cacheOptions = new MemoryCacheEntryOptions()
                .SetAbsoluteExpiration(DateTime.Now.AddMinutes(15))
                .SetSlidingExpiration(TimeSpan.FromMinutes(2))
                .SetPriority(CacheItemPriority.High);

            _memoryCache.Set(key, response, cacheOptions);
        }

        protected object MapToDto<TDto>(object response) where TDto : class
        {
            return _mapper.Map<TDto>(response);
        }

        public override OkObjectResult Ok([ActionResultObjectValue] object value)
        {
            _logger.LogQueryController(Request.GetEncodedPathAndQuery());

            return base.Ok(value);
        }
    }
}
