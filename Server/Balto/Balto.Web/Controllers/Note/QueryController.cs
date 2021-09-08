using AutoMapper;
using Balto.Application.Aggregates.Note;
using Balto.Web.Handlers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using static Balto.Application.Aggregates.Note.Dto.V1;
using DomainNote = Balto.Domain.Aggregates.Note;

namespace Balto.Web.Controllers.Note
{
    [ApiController]
    [Route("api/v{version:apiVersion}/note")]
    [ApiVersion("1.0")]
    [Authorize]
    public class QueryController : ControllerBase
    {
        private readonly DbSet<DomainNote.Note> _notes;
        private readonly IMapper _mapper;

        public QueryController(
            DbSet<DomainNote.Note> note,
            IMapper mapper)
        {
            _notes = note ??
                throw new ArgumentNullException(nameof(note));

            _mapper = mapper ??
                throw new ArgumentNullException(nameof(mapper));
        }

        [HttpGet]
        public async Task<IActionResult> GetAll() =>
            await RequestHandler.HandleMappedQuery<IEnumerable<DomainNote.Note>, IEnumerable<NoteSimple>>(_notes.GetAll, _mapper);

        [HttpGet("{noteId}")]
        public async Task<IActionResult> GetById(Guid noteId) =>
            await RequestHandler.HandleMappedQuery<Guid, DomainNote.Note, NoteDetails>(noteId, _notes.GetById, _mapper);

        [HttpGet("public")]
        public async Task<IActionResult> GetAllPublic() =>
            await RequestHandler.HandleMappedQuery<IEnumerable<DomainNote.Note>, IEnumerable<NoteSimple>>(_notes.GetAllPublic, _mapper);

        [HttpGet("public/{noteId}")]
        public async Task<IActionResult> GetByIdPublic(Guid noteId) =>
            await RequestHandler.HandleMappedQuery<Guid, DomainNote.Note, NoteDetails>(noteId, _notes.GetByIdPublic, _mapper);
    }
}
