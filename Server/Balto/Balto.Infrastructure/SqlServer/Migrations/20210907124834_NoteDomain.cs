using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Balto.Infrastructure.SqlServer.Migrations
{
    public partial class NoteDomain : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Notes",
                columns: table => new
                {
                    NoteId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title_Value = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Content_Value = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OwnerId_Value = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Public = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Id_Value = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notes", x => x.NoteId);
                });

            migrationBuilder.CreateTable(
                name: "NoteContributor",
                columns: table => new
                {
                    NoteContributorId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    noteId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Id_Value = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NoteContributor", x => x.NoteContributorId);
                    table.ForeignKey(
                        name: "FK_NoteContributor_Notes_noteId",
                        column: x => x.noteId,
                        principalTable: "Notes",
                        principalColumn: "NoteId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_NoteContributor_Id_Value",
                table: "NoteContributor",
                column: "Id_Value",
                unique: true,
                filter: "[Id_Value] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_NoteContributor_noteId",
                table: "NoteContributor",
                column: "noteId");

            migrationBuilder.CreateIndex(
                name: "IX_Notes_Id_Value",
                table: "Notes",
                column: "Id_Value",
                unique: true,
                filter: "[Id_Value] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "NoteContributor");

            migrationBuilder.DropTable(
                name: "Notes");
        }
    }
}
