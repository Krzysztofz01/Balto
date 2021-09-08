using System;
using System.Collections.Generic;

namespace Balto.Application.Aggregates.Note
{
    public static class Dto
    {
        public static class V1
        {
            public class NoteDetails
            {
                public Guid Id { get; set; }
                public string Title { get; set; }
                public string Content { get; set; }
                public bool Public { get; set; }
                public Guid OwnerId { get; set; }
                public IEnumerable<Guid> CollaboratorsIds { get; set; }
            }

            public class NoteSimple
            {
                public Guid Id { get; set; }
                public string Title { get; set; }
                public Guid OwnerId { get; set; }
            }
        }
    }
}
