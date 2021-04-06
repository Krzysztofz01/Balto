using Balto.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Balto.Repository.Context
{
    class BaltoDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Objective> Objectives { get; set; }
        public DbSet<Note> Notes { get; set; }
        public DbSet<Project> Projects { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //One user has many objectives but one objective has one user
            modelBuilder.Entity<Objective>()
                .HasOne<User>(o => o.User)
                .WithMany(u => u.Objectives)
                .HasForeignKey(o => o.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            //A user is a owner of multiple notes but one note has one owner
            modelBuilder.Entity<Note>()
                .HasOne<User>(n => n.Owner)
                .WithMany(u => u.Notes)
                .HasForeignKey(n => n.OwnerId)
                .OnDelete(DeleteBehavior.Cascade);

            //ReadWrite access of multiple notes can be shared to multiple users
            //todo

            //Readonly access of multiple notes can be shared to multuple users
            //todo

            //A user is a owner of multiple projects but one project has one owner
            modelBuilder.Entity<Project>()
                .HasOne<User>(p => p.Owner)
                .WithMany(u => u.Projects)
                .HasForeignKey(p => p.OwnerId)
                .OnDelete(DeleteBehavior.Cascade);

            //ReadWrite access of multiple projects can be shared to multiple users
            //todo

            //Readonly access of multiple projects can be shared to multuple users
            //todo

            //A project has multiple projectTables but a projectTable belongs to one project
            modelBuilder.Entity<ProjectTable>()
                .HasOne<Project>(pt => pt.Project)
                .WithMany(p => p.Tabels)
                .HasForeignKey(pt => pt.ProjectId)
                .OnDelete(DeleteBehavior.Cascade);

            //A projectTable has multiple projectTableEntries but a projectTableEntry belongs to one projectTable
            modelBuilder.Entity<ProjectTableEntry>()
                .HasOne<ProjectTable>(pte => pte.ProjectTable)
                .WithMany(pt => pt.Entries)
                .HasForeignKey(pte => pte.ProjectTableId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
