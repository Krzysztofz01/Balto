using Balto.API.Controllers.Base;
using Balto.Application.Abstraction;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using static Balto.Application.Tags.Commands;

namespace Balto.API.Controllers
{
    [Route("api/v{version:apiVersion}/tag")]
    [ApiVersion("1.0")]
    public class TagCommandController : CommandController
    {
        private readonly ITagService _tagService;

        public TagCommandController(ITagService tagService, ILogger<TagCommandController> logger) : base(logger)
        {
            _tagService = tagService ??
                throw new ArgumentNullException(nameof(tagService));
        }

        [HttpPost]
        public async Task<IActionResult> Create(V1.Create command) =>
            await Command(command, _tagService.Handle);

        [HttpDelete]
        public async Task<IActionResult> Delete(V1.Delete command) =>
            await Command(command, _tagService.Handle);
    }
}
