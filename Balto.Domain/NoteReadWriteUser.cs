﻿namespace Balto.Domain
{
    public class NoteReadWriteUser : BaseEntity
    {
        public long? NoteId { get; set; }
        public virtual Note Note { get; set; }
        public long? UserId { get; set; }
        public virtual User User { get; set; }
    }
}
