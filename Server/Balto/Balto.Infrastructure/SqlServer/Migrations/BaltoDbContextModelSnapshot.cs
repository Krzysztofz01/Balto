﻿// <auto-generated />
using System;
using Balto.Infrastructure.SqlServer.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Balto.Infrastructure.SqlServer.Migrations
{
    [DbContext(typeof(BaltoDbContext))]
    partial class BaltoDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.9")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Balto.Domain.Aggregates.Objective.Objective", b =>
                {
                    b.Property<Guid>("ObjectiveId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DeletedAt")
                        .HasColumnType("datetime2");

                    b.Property<bool>("Finished")
                        .HasColumnType("bit");

                    b.Property<DateTime>("ModifedAt")
                        .HasColumnType("datetime2");

                    b.HasKey("ObjectiveId");

                    b.ToTable("Objectives");
                });

            modelBuilder.Entity("Balto.Domain.Aggregates.User.User", b =>
                {
                    b.Property<Guid>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DeletedAt")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsActivated")
                        .HasColumnType("bit");

                    b.Property<bool>("IsLeader")
                        .HasColumnType("bit");

                    b.Property<DateTime>("ModifedAt")
                        .HasColumnType("datetime2");

                    b.HasKey("UserId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Balto.Domain.Aggregates.Objective.Objective", b =>
                {
                    b.OwnsOne("Balto.Domain.Aggregates.Objective.ObjectiveDescription", "Description", b1 =>
                        {
                            b1.Property<Guid>("ObjectiveId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<string>("Value")
                                .HasColumnType("nvarchar(max)");

                            b1.HasKey("ObjectiveId");

                            b1.ToTable("Objectives");

                            b1.WithOwner()
                                .HasForeignKey("ObjectiveId");
                        });

                    b.OwnsOne("Balto.Domain.Aggregates.Objective.ObjectiveEndingDate", "EndingDate", b1 =>
                        {
                            b1.Property<Guid>("ObjectiveId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<DateTime>("Value")
                                .HasColumnType("datetime2");

                            b1.HasKey("ObjectiveId");

                            b1.ToTable("Objectives");

                            b1.WithOwner()
                                .HasForeignKey("ObjectiveId");
                        });

                    b.OwnsOne("Balto.Domain.Aggregates.Objective.ObjectiveFinishDate", "FinishDate", b1 =>
                        {
                            b1.Property<Guid>("ObjectiveId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<DateTime?>("Value")
                                .HasColumnType("datetime2");

                            b1.HasKey("ObjectiveId");

                            b1.ToTable("Objectives");

                            b1.WithOwner()
                                .HasForeignKey("ObjectiveId");
                        });

                    b.OwnsOne("Balto.Domain.Aggregates.Objective.ObjectiveId", "Id", b1 =>
                        {
                            b1.Property<Guid>("ObjectiveId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<Guid>("Value")
                                .HasColumnType("uniqueidentifier");

                            b1.HasKey("ObjectiveId");

                            b1.HasIndex("Value")
                                .IsUnique()
                                .HasFilter("[Id_Value] IS NOT NULL");

                            b1.ToTable("Objectives");

                            b1.WithOwner()
                                .HasForeignKey("ObjectiveId");
                        });

                    b.OwnsOne("Balto.Domain.Aggregates.Objective.ObjectiveOwnerId", "OwnerId", b1 =>
                        {
                            b1.Property<Guid>("ObjectiveId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<Guid>("Value")
                                .HasColumnType("uniqueidentifier");

                            b1.HasKey("ObjectiveId");

                            b1.ToTable("Objectives");

                            b1.WithOwner()
                                .HasForeignKey("ObjectiveId");
                        });

                    b.OwnsOne("Balto.Domain.Aggregates.Objective.ObjectivePeriodicity", "Periodicity", b1 =>
                        {
                            b1.Property<Guid>("ObjectiveId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<int>("Value")
                                .HasColumnType("int");

                            b1.HasKey("ObjectiveId");

                            b1.ToTable("Objectives");

                            b1.WithOwner()
                                .HasForeignKey("ObjectiveId");
                        });

                    b.OwnsOne("Balto.Domain.Aggregates.Objective.ObjectivePriority", "Priority", b1 =>
                        {
                            b1.Property<Guid>("ObjectiveId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<int>("Value")
                                .HasColumnType("int");

                            b1.HasKey("ObjectiveId");

                            b1.ToTable("Objectives");

                            b1.WithOwner()
                                .HasForeignKey("ObjectiveId");
                        });

                    b.OwnsOne("Balto.Domain.Aggregates.Objective.ObjectiveStartingDate", "StartingDate", b1 =>
                        {
                            b1.Property<Guid>("ObjectiveId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<DateTime>("Value")
                                .HasColumnType("datetime2");

                            b1.HasKey("ObjectiveId");

                            b1.ToTable("Objectives");

                            b1.WithOwner()
                                .HasForeignKey("ObjectiveId");
                        });

                    b.OwnsOne("Balto.Domain.Aggregates.Objective.ObjectiveTitle", "Title", b1 =>
                        {
                            b1.Property<Guid>("ObjectiveId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<string>("Value")
                                .HasColumnType("nvarchar(max)");

                            b1.HasKey("ObjectiveId");

                            b1.ToTable("Objectives");

                            b1.WithOwner()
                                .HasForeignKey("ObjectiveId");
                        });

                    b.Navigation("Description");

                    b.Navigation("EndingDate");

                    b.Navigation("FinishDate");

                    b.Navigation("Id");

                    b.Navigation("OwnerId");

                    b.Navigation("Periodicity");

                    b.Navigation("Priority");

                    b.Navigation("StartingDate");

                    b.Navigation("Title");
                });

            modelBuilder.Entity("Balto.Domain.Aggregates.User.User", b =>
                {
                    b.OwnsOne("Balto.Domain.Aggregates.User.UserColor", "Color", b1 =>
                        {
                            b1.Property<Guid>("UserId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<string>("Value")
                                .HasColumnType("nvarchar(max)");

                            b1.HasKey("UserId");

                            b1.ToTable("Users");

                            b1.WithOwner()
                                .HasForeignKey("UserId");
                        });

                    b.OwnsOne("Balto.Domain.Aggregates.User.UserEmail", "Email", b1 =>
                        {
                            b1.Property<Guid>("UserId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<string>("Value")
                                .HasColumnType("nvarchar(450)");

                            b1.HasKey("UserId");

                            b1.HasIndex("Value")
                                .IsUnique()
                                .HasFilter("[Email_Value] IS NOT NULL");

                            b1.ToTable("Users");

                            b1.WithOwner()
                                .HasForeignKey("UserId");
                        });

                    b.OwnsOne("Balto.Domain.Aggregates.User.UserId", "Id", b1 =>
                        {
                            b1.Property<Guid>("UserId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<Guid>("Value")
                                .HasColumnType("uniqueidentifier");

                            b1.HasKey("UserId");

                            b1.HasIndex("Value")
                                .IsUnique()
                                .HasFilter("[Id_Value] IS NOT NULL");

                            b1.ToTable("Users");

                            b1.WithOwner()
                                .HasForeignKey("UserId");
                        });

                    b.OwnsOne("Balto.Domain.Aggregates.User.UserLastLogin", "LastLogin", b1 =>
                        {
                            b1.Property<Guid>("UserId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<DateTime>("Date")
                                .HasColumnType("datetime2");

                            b1.Property<string>("IpAddress")
                                .HasColumnType("nvarchar(max)");

                            b1.HasKey("UserId");

                            b1.ToTable("Users");

                            b1.WithOwner()
                                .HasForeignKey("UserId");
                        });

                    b.OwnsOne("Balto.Domain.Aggregates.User.UserName", "Name", b1 =>
                        {
                            b1.Property<Guid>("UserId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<string>("Value")
                                .HasColumnType("nvarchar(max)");

                            b1.HasKey("UserId");

                            b1.ToTable("Users");

                            b1.WithOwner()
                                .HasForeignKey("UserId");
                        });

                    b.OwnsOne("Balto.Domain.Aggregates.User.UserPassword", "Password", b1 =>
                        {
                            b1.Property<Guid>("UserId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<string>("Value")
                                .HasColumnType("nvarchar(max)");

                            b1.HasKey("UserId");

                            b1.ToTable("Users");

                            b1.WithOwner()
                                .HasForeignKey("UserId");
                        });

                    b.OwnsOne("Balto.Domain.Aggregates.User.UserTeamId", "TeamId", b1 =>
                        {
                            b1.Property<Guid>("UserId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<Guid>("Value")
                                .HasColumnType("uniqueidentifier");

                            b1.HasKey("UserId");

                            b1.ToTable("Users");

                            b1.WithOwner()
                                .HasForeignKey("UserId");
                        });

                    b.OwnsMany("Balto.Domain.Aggregates.User.RefreshToken", "RefreshTokens", b1 =>
                        {
                            b1.Property<Guid>("RefreshTokenId")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("uniqueidentifier");

                            b1.Property<DateTime>("Created")
                                .HasColumnType("datetime2");

                            b1.Property<DateTime>("CreatedAt")
                                .HasColumnType("datetime2");

                            b1.Property<string>("CreatedByIp")
                                .HasColumnType("nvarchar(max)");

                            b1.Property<DateTime?>("DeletedAt")
                                .HasColumnType("datetime2");

                            b1.Property<DateTime>("Expires")
                                .HasColumnType("datetime2");

                            b1.Property<bool>("IsRevoked")
                                .HasColumnType("bit");

                            b1.Property<DateTime>("ModifedAt")
                                .HasColumnType("datetime2");

                            b1.Property<string>("ReplacedByToken")
                                .HasColumnType("nvarchar(max)");

                            b1.Property<DateTime?>("Revoked")
                                .HasColumnType("datetime2");

                            b1.Property<string>("RevokedByIp")
                                .HasColumnType("nvarchar(max)");

                            b1.Property<string>("Token")
                                .HasColumnType("nvarchar(max)");

                            b1.Property<Guid>("ownerId")
                                .HasColumnType("uniqueidentifier");

                            b1.HasKey("RefreshTokenId");

                            b1.HasIndex("ownerId");

                            b1.ToTable("RefreshToken");

                            b1.WithOwner()
                                .HasForeignKey("ownerId");

                            b1.OwnsOne("Balto.Domain.Aggregates.User.RefreshTokenId", "Id", b2 =>
                                {
                                    b2.Property<Guid>("RefreshTokenId")
                                        .HasColumnType("uniqueidentifier");

                                    b2.Property<Guid>("Value")
                                        .HasColumnType("uniqueidentifier");

                                    b2.HasKey("RefreshTokenId");

                                    b2.HasIndex("Value")
                                        .IsUnique()
                                        .HasFilter("[Id_Value] IS NOT NULL");

                                    b2.ToTable("RefreshToken");

                                    b2.WithOwner()
                                        .HasForeignKey("RefreshTokenId");
                                });

                            b1.Navigation("Id");
                        });

                    b.Navigation("Color");

                    b.Navigation("Email");

                    b.Navigation("Id");

                    b.Navigation("LastLogin");

                    b.Navigation("Name");

                    b.Navigation("Password");

                    b.Navigation("RefreshTokens");

                    b.Navigation("TeamId");
                });
#pragma warning restore 612, 618
        }
    }
}
