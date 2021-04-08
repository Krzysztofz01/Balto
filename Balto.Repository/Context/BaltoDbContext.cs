using Balto.Domain;
using Balto.Repository.Maps;
using Microsoft.EntityFrameworkCore;

namespace Balto.Repository.Context
{
    public class BaltoDbContext : DbContext
    {
        //Entities
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Objective> Objectives { get; set; }
        public virtual DbSet<Note> Notes { get; set; }
        public virtual DbSet<Project> Projects { get; set; }
        public virtual DbSet<ProjectTable> ProjectTables { get; set; }
        public virtual DbSet<ProjectTableEntry> ProjectTableEntries { get; set; }

        //Many-to-many helpers
        public virtual DbSet<NoteReadWriteUser> NoteReadWrtieUsers { get; set; }
        public virtual DbSet<NoteReadOnlyUser> NoteReadOnlyUsers { get; set; }
        public virtual DbSet<ProjectReadWriteUser> ProjectReadWriteUsers { get; set; }
        public virtual DbSet<ProjectReadOnlyUser> ProjectReadOnlyUsers { get; set; }

        public BaltoDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //User mapping
            new UserMap(modelBuilder.Entity<User>());

            //Objective mapping
            new ObjectiveMap(modelBuilder.Entity<Objective>());

            //Note mapping
            new NoteMap(modelBuilder.Entity<Note>());

            //NoteReadWriteUser mapping
            new NoteReadWriteUserMap(modelBuilder.Entity<NoteReadWriteUser>());

            //NoteReadOnlyUser mapping
            new NoteReadOnlyUserMap(modelBuilder.Entity<NoteReadOnlyUser>());

            //Project mapping
            new ProjectMap(modelBuilder.Entity<Project>());

            //ProjectReadWriteUser mapping
            new ProjectReadWriteUserMap(modelBuilder.Entity<ProjectReadWriteUser>());
           
            //ProjectReadOnlyUser mapping
            new ProjectReadOnlyUserMap(modelBuilder.Entity<ProjectReadOnlyUser>());

            //ProjectTable mapping
            new ProjectTableMap(modelBuilder.Entity<ProjectTable>());

            //ProjectTalbeEntry mapping
            new ProjectTableEntryMap(modelBuilder.Entity<ProjectTableEntry>());
        }
    }
}
