using System;
using System.Collections.Generic;

namespace Balto.Domain
{
    public class User : BaseEntity
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public DateTime LastLoginDate { get; set; }
        public string LastLoginIp { get; set; }
        public bool IsLeader { get; set; }
        public long? TeamId { get; set; }
        public virtual Team Team { get; set; }

        public virtual ICollection<Note> Notes { get; set; }
        public virtual ICollection<NoteReadOnlyUser> SharedReadOnlyNotes { get; set; }
        public virtual ICollection<NoteReadWriteUser> SharedReadWriteNotes { get; set; }
        public virtual ICollection<Objective> Objectives { get; set; }
        public virtual ICollection<Project> Projects { get; set; }
        public virtual ICollection<ProjectReadOnlyUser> SharedReadOnlyProjects { get; set; }
        public virtual ICollection<ProjectReadWriteUser> SharedReadWriteProjects { get; set; }
        public virtual ICollection<ProjectTableEntry> ProjectTableEntriesAdded { get; set; }
        public virtual ICollection<ProjectTableEntry> ProjectTableEntriesFinished { get; set; }
    }
}
