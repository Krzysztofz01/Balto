using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Balto.Infrastructure.MySql.Migrations
{
    public partial class NoteAggregateImplementation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Notes",
                schema: "balto",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Title_Value = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Content_Value = table.Column<string>(type: "MEDIUMTEXT", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    OwnerId_Value = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci"),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notes", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "NoteContributor",
                schema: "balto",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    IdentityId_Value = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci"),
                    AccessRole_Value = table.Column<int>(type: "int", nullable: true),
                    noteId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NoteContributor", x => x.Id);
                    table.ForeignKey(
                        name: "FK_NoteContributor_Notes_noteId",
                        column: x => x.noteId,
                        principalSchema: "balto",
                        principalTable: "Notes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "NoteSnapshot",
                schema: "balto",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Content_Value = table.Column<string>(type: "MEDIUMTEXT", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CreationDate_Value = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    noteId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NoteSnapshot", x => x.Id);
                    table.ForeignKey(
                        name: "FK_NoteSnapshot_Notes_noteId",
                        column: x => x.noteId,
                        principalSchema: "balto",
                        principalTable: "Notes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "NoteTag",
                schema: "balto",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    TagId_Value = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci"),
                    noteId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NoteTag", x => x.Id);
                    table.ForeignKey(
                        name: "FK_NoteTag_Notes_noteId",
                        column: x => x.noteId,
                        principalSchema: "balto",
                        principalTable: "Notes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_NoteContributor_noteId",
                schema: "balto",
                table: "NoteContributor",
                column: "noteId");

            migrationBuilder.CreateIndex(
                name: "IX_NoteSnapshot_noteId",
                schema: "balto",
                table: "NoteSnapshot",
                column: "noteId");

            migrationBuilder.CreateIndex(
                name: "IX_NoteTag_noteId",
                schema: "balto",
                table: "NoteTag",
                column: "noteId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "NoteContributor",
                schema: "balto");

            migrationBuilder.DropTable(
                name: "NoteSnapshot",
                schema: "balto");

            migrationBuilder.DropTable(
                name: "NoteTag",
                schema: "balto");

            migrationBuilder.DropTable(
                name: "Notes",
                schema: "balto");
        }
    }
}
