﻿// <auto-generated />
using System;
using Balto.Infrastructure.MySql;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Balto.Infrastructure.MySql.Migrations
{
    [DbContext(typeof(BaltoDbContext))]
    [Migration("20211106005306_InitialMigration")]
    partial class InitialMigration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasDefaultSchema("balto")
                .HasAnnotation("Relational:MaxIdentifierLength", 64)
                .HasAnnotation("ProductVersion", "5.0.11");

            modelBuilder.Entity("Balto.Domain.Goals.Goal", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime?>("DeletedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime(6)");

                    b.HasKey("Id");

                    b.ToTable("Goals");
                });

            modelBuilder.Entity("Balto.Domain.Identities.Identity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime?>("DeletedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime(6)");

                    b.HasKey("Id");

                    b.ToTable("Identities");
                });

            modelBuilder.Entity("Balto.Domain.Projects.Project", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime?>("DeletedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime(6)");

                    b.HasKey("Id");

                    b.ToTable("Projects");
                });

            modelBuilder.Entity("Balto.Domain.Goals.Goal", b =>
                {
                    b.OwnsOne("Balto.Domain.Goals.GoalColor", "Color", b1 =>
                        {
                            b1.Property<Guid>("GoalId")
                                .HasColumnType("char(36)");

                            b1.Property<string>("Value")
                                .HasColumnType("longtext");

                            b1.HasKey("GoalId");

                            b1.ToTable("Goals");

                            b1.WithOwner()
                                .HasForeignKey("GoalId");
                        });

                    b.OwnsOne("Balto.Domain.Goals.GoalDeadline", "Deadline", b1 =>
                        {
                            b1.Property<Guid>("GoalId")
                                .HasColumnType("char(36)");

                            b1.Property<DateTime?>("Value")
                                .HasColumnType("datetime(6)");

                            b1.HasKey("GoalId");

                            b1.ToTable("Goals");

                            b1.WithOwner()
                                .HasForeignKey("GoalId");
                        });

                    b.OwnsOne("Balto.Domain.Goals.GoalDescription", "Description", b1 =>
                        {
                            b1.Property<Guid>("GoalId")
                                .HasColumnType("char(36)");

                            b1.Property<string>("Value")
                                .HasMaxLength(300)
                                .HasColumnType("varchar(300)");

                            b1.HasKey("GoalId");

                            b1.ToTable("Goals");

                            b1.WithOwner()
                                .HasForeignKey("GoalId");
                        });

                    b.OwnsOne("Balto.Domain.Goals.GoalIsRecurring", "IsRecurring", b1 =>
                        {
                            b1.Property<Guid>("GoalId")
                                .HasColumnType("char(36)");

                            b1.Property<bool>("Value")
                                .HasColumnType("tinyint(1)");

                            b1.HasKey("GoalId");

                            b1.ToTable("Goals");

                            b1.WithOwner()
                                .HasForeignKey("GoalId");
                        });

                    b.OwnsOne("Balto.Domain.Goals.GoalOwnerId", "OwnerId", b1 =>
                        {
                            b1.Property<Guid>("GoalId")
                                .HasColumnType("char(36)");

                            b1.Property<Guid>("Value")
                                .HasColumnType("char(36)");

                            b1.HasKey("GoalId");

                            b1.ToTable("Goals");

                            b1.WithOwner()
                                .HasForeignKey("GoalId");
                        });

                    b.OwnsOne("Balto.Domain.Goals.GoalPriority", "Priority", b1 =>
                        {
                            b1.Property<Guid>("GoalId")
                                .HasColumnType("char(36)");

                            b1.Property<int>("Value")
                                .HasColumnType("int");

                            b1.HasKey("GoalId");

                            b1.ToTable("Goals");

                            b1.WithOwner()
                                .HasForeignKey("GoalId");
                        });

                    b.OwnsOne("Balto.Domain.Goals.GoalStartingDate", "StartingDate", b1 =>
                        {
                            b1.Property<Guid>("GoalId")
                                .HasColumnType("char(36)");

                            b1.Property<DateTime>("Value")
                                .HasColumnType("datetime(6)");

                            b1.HasKey("GoalId");

                            b1.ToTable("Goals");

                            b1.WithOwner()
                                .HasForeignKey("GoalId");
                        });

                    b.OwnsOne("Balto.Domain.Goals.GoalStatus", "Status", b1 =>
                        {
                            b1.Property<Guid>("GoalId")
                                .HasColumnType("char(36)");

                            b1.Property<DateTime?>("FinishDate")
                                .HasColumnType("datetime(6)");

                            b1.Property<bool>("Finished")
                                .HasColumnType("tinyint(1)");

                            b1.HasKey("GoalId");

                            b1.ToTable("Goals");

                            b1.WithOwner()
                                .HasForeignKey("GoalId");
                        });

                    b.OwnsOne("Balto.Domain.Goals.GoalTitle", "Title", b1 =>
                        {
                            b1.Property<Guid>("GoalId")
                                .HasColumnType("char(36)");

                            b1.Property<string>("Value")
                                .HasMaxLength(100)
                                .HasColumnType("varchar(100)");

                            b1.HasKey("GoalId");

                            b1.ToTable("Goals");

                            b1.WithOwner()
                                .HasForeignKey("GoalId");
                        });

                    b.Navigation("Color");

                    b.Navigation("Deadline");

                    b.Navigation("Description");

                    b.Navigation("IsRecurring");

                    b.Navigation("OwnerId");

                    b.Navigation("Priority");

                    b.Navigation("StartingDate");

                    b.Navigation("Status");

                    b.Navigation("Title");
                });

            modelBuilder.Entity("Balto.Domain.Identities.Identity", b =>
                {
                    b.OwnsOne("Balto.Domain.Identities.IdentityActivation", "Activation", b1 =>
                        {
                            b1.Property<Guid>("IdentityId")
                                .HasColumnType("char(36)");

                            b1.Property<bool>("Value")
                                .HasColumnType("tinyint(1)");

                            b1.HasKey("IdentityId");

                            b1.ToTable("Identities");

                            b1.WithOwner()
                                .HasForeignKey("IdentityId");
                        });

                    b.OwnsOne("Balto.Domain.Identities.IdentityColor", "Color", b1 =>
                        {
                            b1.Property<Guid>("IdentityId")
                                .HasColumnType("char(36)");

                            b1.Property<string>("Value")
                                .HasColumnType("longtext");

                            b1.HasKey("IdentityId");

                            b1.ToTable("Identities");

                            b1.WithOwner()
                                .HasForeignKey("IdentityId");
                        });

                    b.OwnsOne("Balto.Domain.Identities.IdentityEmail", "Email", b1 =>
                        {
                            b1.Property<Guid>("IdentityId")
                                .HasColumnType("char(36)");

                            b1.Property<string>("Value")
                                .HasColumnType("varchar(255)");

                            b1.HasKey("IdentityId");

                            b1.HasIndex("Value")
                                .IsUnique();

                            b1.ToTable("Identities");

                            b1.WithOwner()
                                .HasForeignKey("IdentityId");
                        });

                    b.OwnsOne("Balto.Domain.Identities.IdentityLastLogin", "LastLogin", b1 =>
                        {
                            b1.Property<Guid>("IdentityId")
                                .HasColumnType("char(36)");

                            b1.Property<DateTime>("Date")
                                .HasColumnType("datetime(6)");

                            b1.Property<string>("IpAddress")
                                .HasColumnType("longtext");

                            b1.HasKey("IdentityId");

                            b1.ToTable("Identities");

                            b1.WithOwner()
                                .HasForeignKey("IdentityId");
                        });

                    b.OwnsOne("Balto.Domain.Identities.IdentityName", "Name", b1 =>
                        {
                            b1.Property<Guid>("IdentityId")
                                .HasColumnType("char(36)");

                            b1.Property<string>("Value")
                                .HasMaxLength(40)
                                .HasColumnType("varchar(40)");

                            b1.HasKey("IdentityId");

                            b1.ToTable("Identities");

                            b1.WithOwner()
                                .HasForeignKey("IdentityId");
                        });

                    b.OwnsOne("Balto.Domain.Identities.IdentityPasswordHash", "PasswordHash", b1 =>
                        {
                            b1.Property<Guid>("IdentityId")
                                .HasColumnType("char(36)");

                            b1.Property<string>("Value")
                                .HasColumnType("longtext");

                            b1.HasKey("IdentityId");

                            b1.ToTable("Identities");

                            b1.WithOwner()
                                .HasForeignKey("IdentityId");
                        });

                    b.OwnsOne("Balto.Domain.Identities.IdentityRole", "Role", b1 =>
                        {
                            b1.Property<Guid>("IdentityId")
                                .HasColumnType("char(36)");

                            b1.Property<int>("Value")
                                .HasColumnType("int");

                            b1.HasKey("IdentityId");

                            b1.ToTable("Identities");

                            b1.WithOwner()
                                .HasForeignKey("IdentityId");
                        });

                    b.OwnsOne("Balto.Domain.Identities.IdentityTeamId", "TeamId", b1 =>
                        {
                            b1.Property<Guid>("IdentityId")
                                .HasColumnType("char(36)");

                            b1.Property<Guid?>("Value")
                                .HasColumnType("char(36)");

                            b1.HasKey("IdentityId");

                            b1.ToTable("Identities");

                            b1.WithOwner()
                                .HasForeignKey("IdentityId");
                        });

                    b.OwnsMany("Balto.Domain.Identities.Tokens.Token", "Tokens", b1 =>
                        {
                            b1.Property<Guid>("Id")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("char(36)");

                            b1.Property<DateTime>("Created")
                                .HasColumnType("datetime(6)");

                            b1.Property<DateTime>("CreatedAt")
                                .HasColumnType("datetime(6)");

                            b1.Property<string>("CreatedByIpAddress")
                                .HasColumnType("longtext");

                            b1.Property<DateTime?>("DeletedAt")
                                .HasColumnType("datetime(6)");

                            b1.Property<DateTime>("Expires")
                                .HasColumnType("datetime(6)");

                            b1.Property<bool>("IsRevoked")
                                .HasColumnType("tinyint(1)");

                            b1.Property<string>("ReplacedByTokenHash")
                                .HasColumnType("longtext");

                            b1.Property<DateTime?>("Revoked")
                                .HasColumnType("datetime(6)");

                            b1.Property<string>("RevokedByIpAddress")
                                .HasColumnType("longtext");

                            b1.Property<string>("TokenHash")
                                .HasColumnType("varchar(255)");

                            b1.Property<DateTime>("UpdatedAt")
                                .HasColumnType("datetime(6)");

                            b1.Property<Guid>("identityId")
                                .HasColumnType("char(36)");

                            b1.HasKey("Id");

                            b1.HasIndex("TokenHash")
                                .IsUnique();

                            b1.HasIndex("identityId");

                            b1.ToTable("Token");

                            b1.WithOwner()
                                .HasForeignKey("identityId");
                        });

                    b.Navigation("Activation");

                    b.Navigation("Color");

                    b.Navigation("Email");

                    b.Navigation("LastLogin");

                    b.Navigation("Name");

                    b.Navigation("PasswordHash");

                    b.Navigation("Role");

                    b.Navigation("TeamId");

                    b.Navigation("Tokens");
                });

            modelBuilder.Entity("Balto.Domain.Projects.Project", b =>
                {
                    b.OwnsOne("Balto.Domain.Projects.ProjectOwnerId", "OwnerId", b1 =>
                        {
                            b1.Property<Guid>("ProjectId")
                                .HasColumnType("char(36)");

                            b1.Property<Guid>("Value")
                                .HasColumnType("char(36)");

                            b1.HasKey("ProjectId");

                            b1.ToTable("Projects");

                            b1.WithOwner()
                                .HasForeignKey("ProjectId");
                        });

                    b.OwnsOne("Balto.Domain.Projects.ProjectTicketToken", "TicketToken", b1 =>
                        {
                            b1.Property<Guid>("ProjectId")
                                .HasColumnType("char(36)");

                            b1.Property<string>("Value")
                                .HasColumnType("varchar(255)");

                            b1.HasKey("ProjectId");

                            b1.HasIndex("Value")
                                .IsUnique();

                            b1.ToTable("Projects");

                            b1.WithOwner()
                                .HasForeignKey("ProjectId");
                        });

                    b.OwnsOne("Balto.Domain.Projects.ProjectTitle", "Title", b1 =>
                        {
                            b1.Property<Guid>("ProjectId")
                                .HasColumnType("char(36)");

                            b1.Property<string>("Value")
                                .HasMaxLength(100)
                                .HasColumnType("varchar(100)");

                            b1.HasKey("ProjectId");

                            b1.ToTable("Projects");

                            b1.WithOwner()
                                .HasForeignKey("ProjectId");
                        });

                    b.OwnsMany("Balto.Domain.Projects.ProjectContributors.ProjectContributor", "Contributors", b1 =>
                        {
                            b1.Property<Guid>("Id")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("char(36)");

                            b1.Property<DateTime>("CreatedAt")
                                .HasColumnType("datetime(6)");

                            b1.Property<DateTime?>("DeletedAt")
                                .HasColumnType("datetime(6)");

                            b1.Property<DateTime>("UpdatedAt")
                                .HasColumnType("datetime(6)");

                            b1.Property<Guid>("projectId")
                                .HasColumnType("char(36)");

                            b1.HasKey("Id");

                            b1.HasIndex("projectId");

                            b1.ToTable("ProjectContributor");

                            b1.WithOwner()
                                .HasForeignKey("projectId");

                            b1.OwnsOne("Balto.Domain.Projects.ProjectContributors.ProjectContributorIdentityId", "IdentityId", b2 =>
                                {
                                    b2.Property<Guid>("ProjectContributorId")
                                        .HasColumnType("char(36)");

                                    b2.Property<Guid>("Value")
                                        .HasColumnType("char(36)");

                                    b2.HasKey("ProjectContributorId");

                                    b2.ToTable("ProjectContributor");

                                    b2.WithOwner()
                                        .HasForeignKey("ProjectContributorId");
                                });

                            b1.OwnsOne("Balto.Domain.Projects.ProjectContributors.ProjectContributorRole", "Role", b2 =>
                                {
                                    b2.Property<Guid>("ProjectContributorId")
                                        .HasColumnType("char(36)");

                                    b2.Property<int>("Value")
                                        .HasColumnType("int");

                                    b2.HasKey("ProjectContributorId");

                                    b2.ToTable("ProjectContributor");

                                    b2.WithOwner()
                                        .HasForeignKey("ProjectContributorId");
                                });

                            b1.Navigation("IdentityId");

                            b1.Navigation("Role");
                        });

                    b.OwnsMany("Balto.Domain.Projects.ProjectTables.ProjectTable", "Tables", b1 =>
                        {
                            b1.Property<Guid>("Id")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("char(36)");

                            b1.Property<DateTime>("CreatedAt")
                                .HasColumnType("datetime(6)");

                            b1.Property<DateTime?>("DeletedAt")
                                .HasColumnType("datetime(6)");

                            b1.Property<DateTime>("UpdatedAt")
                                .HasColumnType("datetime(6)");

                            b1.Property<Guid>("projectId")
                                .HasColumnType("char(36)");

                            b1.HasKey("Id");

                            b1.HasIndex("projectId");

                            b1.ToTable("ProjectTable");

                            b1.WithOwner()
                                .HasForeignKey("projectId");

                            b1.OwnsOne("Balto.Domain.Projects.ProjectTables.ProjectTableColor", "Color", b2 =>
                                {
                                    b2.Property<Guid>("ProjectTableId")
                                        .HasColumnType("char(36)");

                                    b2.Property<string>("Value")
                                        .HasColumnType("longtext");

                                    b2.HasKey("ProjectTableId");

                                    b2.ToTable("ProjectTable");

                                    b2.WithOwner()
                                        .HasForeignKey("ProjectTableId");
                                });

                            b1.OwnsOne("Balto.Domain.Projects.ProjectTables.ProjectTableTitle", "Title", b2 =>
                                {
                                    b2.Property<Guid>("ProjectTableId")
                                        .HasColumnType("char(36)");

                                    b2.Property<string>("Value")
                                        .HasMaxLength(100)
                                        .HasColumnType("varchar(100)");

                                    b2.HasKey("ProjectTableId");

                                    b2.ToTable("ProjectTable");

                                    b2.WithOwner()
                                        .HasForeignKey("ProjectTableId");
                                });

                            b1.OwnsMany("Balto.Domain.Projects.ProjectTasks.ProjectTask", "Tasks", b2 =>
                                {
                                    b2.Property<Guid>("Id")
                                        .ValueGeneratedOnAdd()
                                        .HasColumnType("char(36)");

                                    b2.Property<DateTime>("CreatedAt")
                                        .HasColumnType("datetime(6)");

                                    b2.Property<DateTime?>("DeletedAt")
                                        .HasColumnType("datetime(6)");

                                    b2.Property<DateTime>("UpdatedAt")
                                        .HasColumnType("datetime(6)");

                                    b2.Property<Guid>("tableId")
                                        .HasColumnType("char(36)");

                                    b2.HasKey("Id");

                                    b2.HasIndex("tableId");

                                    b2.ToTable("ProjectTask");

                                    b2.WithOwner()
                                        .HasForeignKey("tableId");

                                    b2.OwnsOne("Balto.Domain.Projects.ProjectTasks.ProjectTaskAssignedContributorId", "AssignedContributorId", b3 =>
                                        {
                                            b3.Property<Guid>("ProjectTaskId")
                                                .HasColumnType("char(36)");

                                            b3.Property<Guid?>("Value")
                                                .HasColumnType("char(36)");

                                            b3.HasKey("ProjectTaskId");

                                            b3.ToTable("ProjectTask");

                                            b3.WithOwner()
                                                .HasForeignKey("ProjectTaskId");
                                        });

                                    b2.OwnsOne("Balto.Domain.Projects.ProjectTasks.ProjectTaskColor", "Color", b3 =>
                                        {
                                            b3.Property<Guid>("ProjectTaskId")
                                                .HasColumnType("char(36)");

                                            b3.Property<string>("Value")
                                                .HasColumnType("longtext");

                                            b3.HasKey("ProjectTaskId");

                                            b3.ToTable("ProjectTask");

                                            b3.WithOwner()
                                                .HasForeignKey("ProjectTaskId");
                                        });

                                    b2.OwnsOne("Balto.Domain.Projects.ProjectTasks.ProjectTaskContent", "Content", b3 =>
                                        {
                                            b3.Property<Guid>("ProjectTaskId")
                                                .HasColumnType("char(36)");

                                            b3.Property<string>("Value")
                                                .HasMaxLength(300)
                                                .HasColumnType("varchar(300)");

                                            b3.HasKey("ProjectTaskId");

                                            b3.ToTable("ProjectTask");

                                            b3.WithOwner()
                                                .HasForeignKey("ProjectTaskId");
                                        });

                                    b2.OwnsOne("Balto.Domain.Projects.ProjectTasks.ProjectTaskCreatorId", "CreatorId", b3 =>
                                        {
                                            b3.Property<Guid>("ProjectTaskId")
                                                .HasColumnType("char(36)");

                                            b3.Property<Guid?>("Value")
                                                .HasColumnType("char(36)");

                                            b3.HasKey("ProjectTaskId");

                                            b3.ToTable("ProjectTask");

                                            b3.WithOwner()
                                                .HasForeignKey("ProjectTaskId");
                                        });

                                    b2.OwnsOne("Balto.Domain.Projects.ProjectTasks.ProjectTaskDeadline", "Deadline", b3 =>
                                        {
                                            b3.Property<Guid>("ProjectTaskId")
                                                .HasColumnType("char(36)");

                                            b3.Property<DateTime?>("Value")
                                                .HasColumnType("datetime(6)");

                                            b3.HasKey("ProjectTaskId");

                                            b3.ToTable("ProjectTask");

                                            b3.WithOwner()
                                                .HasForeignKey("ProjectTaskId");
                                        });

                                    b2.OwnsOne("Balto.Domain.Projects.ProjectTasks.ProjectTaskOrdinalNumber", "OrdinalNumber", b3 =>
                                        {
                                            b3.Property<Guid>("ProjectTaskId")
                                                .HasColumnType("char(36)");

                                            b3.Property<int>("Value")
                                                .HasColumnType("int");

                                            b3.HasKey("ProjectTaskId");

                                            b3.ToTable("ProjectTask");

                                            b3.WithOwner()
                                                .HasForeignKey("ProjectTaskId");
                                        });

                                    b2.OwnsOne("Balto.Domain.Projects.ProjectTasks.ProjectTaskPriority", "Priority", b3 =>
                                        {
                                            b3.Property<Guid>("ProjectTaskId")
                                                .HasColumnType("char(36)");

                                            b3.Property<int>("Value")
                                                .HasColumnType("int");

                                            b3.HasKey("ProjectTaskId");

                                            b3.ToTable("ProjectTask");

                                            b3.WithOwner()
                                                .HasForeignKey("ProjectTaskId");
                                        });

                                    b2.OwnsOne("Balto.Domain.Projects.ProjectTasks.ProjectTaskStartingDate", "StartingDate", b3 =>
                                        {
                                            b3.Property<Guid>("ProjectTaskId")
                                                .HasColumnType("char(36)");

                                            b3.Property<DateTime>("Value")
                                                .HasColumnType("datetime(6)");

                                            b3.HasKey("ProjectTaskId");

                                            b3.ToTable("ProjectTask");

                                            b3.WithOwner()
                                                .HasForeignKey("ProjectTaskId");
                                        });

                                    b2.OwnsOne("Balto.Domain.Projects.ProjectTasks.ProjectTaskStatus", "Status", b3 =>
                                        {
                                            b3.Property<Guid>("ProjectTaskId")
                                                .HasColumnType("char(36)");

                                            b3.Property<DateTime?>("FinishDate")
                                                .HasColumnType("datetime(6)");

                                            b3.Property<bool>("Finished")
                                                .HasColumnType("tinyint(1)");

                                            b3.Property<Guid?>("FinishedBy")
                                                .HasColumnType("char(36)");

                                            b3.HasKey("ProjectTaskId");

                                            b3.ToTable("ProjectTask");

                                            b3.WithOwner()
                                                .HasForeignKey("ProjectTaskId");
                                        });

                                    b2.OwnsOne("Balto.Domain.Projects.ProjectTasks.ProjectTaskTitle", "Title", b3 =>
                                        {
                                            b3.Property<Guid>("ProjectTaskId")
                                                .HasColumnType("char(36)");

                                            b3.Property<string>("Value")
                                                .HasMaxLength(100)
                                                .HasColumnType("varchar(100)");

                                            b3.HasKey("ProjectTaskId");

                                            b3.ToTable("ProjectTask");

                                            b3.WithOwner()
                                                .HasForeignKey("ProjectTaskId");
                                        });

                                    b2.Navigation("AssignedContributorId");

                                    b2.Navigation("Color");

                                    b2.Navigation("Content");

                                    b2.Navigation("CreatorId");

                                    b2.Navigation("Deadline");

                                    b2.Navigation("OrdinalNumber");

                                    b2.Navigation("Priority");

                                    b2.Navigation("StartingDate");

                                    b2.Navigation("Status");

                                    b2.Navigation("Title");
                                });

                            b1.Navigation("Color");

                            b1.Navigation("Tasks");

                            b1.Navigation("Title");
                        });

                    b.Navigation("Contributors");

                    b.Navigation("OwnerId");

                    b.Navigation("Tables");

                    b.Navigation("TicketToken");

                    b.Navigation("Title");
                });
#pragma warning restore 612, 618
        }
    }
}
