using System;
using System.Collections.Generic;

namespace Balto.Domain
{
    public class User : BaseEntity
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public DateTime LastLoginDate { get; set; }
        public string LastLoginIp { get; set; }

        public virtual ICollection<Note> Notes { get; set; }
        public virtual ICollection<Note> SharedReadOnlyNotes { get; set; }
        public virtual ICollection<Note> SharedReadWriteNotes { get; set; }
        public virtual ICollection<Objective> Objectives { get; set; }
        public virtual ICollection<Project> Projects { get; set; }
        public virtual ICollection<Project> SharedReadOnlyProjects { get; set; }
        public virtual ICollection<Project> SharedReadWriteProjects { get; set; }
    }
}
