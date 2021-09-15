using AutoMapper;
using Balto.Application.Aggregates.Note;
using Balto.Infrastructure.SqlServer.Context;
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
            BaltoDbContext dbContext,
            IMapper mapper)
        {
            _notes = dbContext.Set<DomainNote.Note>() ??
                throw new ArgumentNullException(nameof(dbContext));

            _mapper = mapper ??
                throw new ArgumentNullException(nameof(mapper));
        }

        [HttpGet]
        public async Task<IActionResult> GetAll() =>
            await RequestHandler.HandleMappedQuery<IEnumerable<DomainNote.Note>, IEnumerable<NoteSimple>>(_notes.GetAllNotes, _mapper);

        [HttpGet("{noteId}")]
        public async Task<IActionResult> GetById(Guid noteId) =>
            await RequestHandler.HandleMappedQuery<Guid, DomainNote.Note, NoteDetails>(noteId, _notes.GetNoteById, _mapper);

        [HttpGet("public")]
        public async Task<IActionResult> GetAllPublic() =>
            await RequestHandler.HandleMappedQuery<IEnumerable<DomainNote.Note>, IEnumerable<NoteSimple>>(_notes.GetAllNotesPublic, _mapper);

        [HttpGet("public/{noteId}")]
        public async Task<IActionResult> GetByIdPublic(Guid noteId) =>
            await RequestHandler.HandleMappedQuery<Guid, DomainNote.Note, NoteDetails>(noteId, _notes.GetNoteByIdPublic, _mapper);
    }
}
