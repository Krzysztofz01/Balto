﻿using Balto.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Balto.Repository.Maps
{
    public class NoteReadWriteUserMap
    {
        public NoteReadWriteUserMap(EntityTypeBuilder<NoteReadWriteUser> entityBuilder)
        {
            entityBuilder.HasKey(n => new { n.NoteId, n.UserId });

            //Relations
            //Many(User) to Many(Note) as readwrite using helper table

            //One(User) to Many(Note as readwrite)
            entityBuilder
                .HasOne<User>(n => n.User)
                .WithMany(u => u.SharedReadWriteNotes)
                .HasForeignKey(n => n.UserId)
                .OnDelete(DeleteBehavior.NoAction);

            //One(Note) to Many(User as readwrite)
            entityBuilder
                .HasOne<Note>(n => n.Note)
                .WithMany(n => n.ReadWriteUsers)
                .HasForeignKey(n => n.NoteId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}