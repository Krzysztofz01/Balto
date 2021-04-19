﻿using Balto.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Balto.Repository.Maps
{
    public class ProjectTableEntryMap
    {
        public ProjectTableEntryMap(EntityTypeBuilder<ProjectTableEntry> entityBuilder)
        {
            //Base entity
            entityBuilder.HasKey(p => p.Id);
            entityBuilder.Property(p => p.AddDate).IsRequired().HasDefaultValueSql("getdate()");
            entityBuilder.Property(p => p.EditDate).IsRequired().HasDefaultValueSql("getdate()");

            entityBuilder.Property(p => p.Name).IsRequired();
            entityBuilder.Property(p => p.Content).HasColumnType("text");
            entityBuilder.Property(p => p.Order).IsRequired().HasDefaultValue(0);
            entityBuilder.Property(p => p.Finished).IsRequired().HasDefaultValue(false);

            //Relations
            //One(ProjectTable) to Many(ProjectTableEntry)
            entityBuilder
                .HasOne<ProjectTable>(p => p.ProjectTable)
                .WithMany(p => p.Entries)
                .HasForeignKey(p => p.ProjectTableId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
