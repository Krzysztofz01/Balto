using AutoMapper;
using Balto.API.Controllers.Base;
using Balto.Application.Notes;
using Balto.Infrastructure.Core.Abstraction;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using static Balto.Application.Notes.Dto;

namespace Balto.API.Controllers
{
    [Route("api/v{version:apiVersion}/note")]
    [ApiVersion("1.0")]
    public class NoteQueryController : QueryController
    {
        public NoteQueryController(IDataAccess dataAccess, IMapper mapper, IMemoryCache memoryCache) : base(dataAccess, mapper, memoryCache)
        {
        }

        [HttpGet]
        public async Task<IActionResult> GetAllNotes()
        {
            var response = await _dataAccess.Notes.GetAllNotes();

            var mappedResponse = MapToDto<IEnumerable<NoteSimple>>(response);

            return Ok(mappedResponse);
        }

        [HttpGet("{noteId}")]
        public async Task<IActionResult> GetNoteById(Guid noteId)
        {
            var response = await _dataAccess.Notes.GetNoteById(noteId);

            var mappedResponse = MapToDto<NoteDetails>(response);

            return Ok(mappedResponse);
        }
    }
}
