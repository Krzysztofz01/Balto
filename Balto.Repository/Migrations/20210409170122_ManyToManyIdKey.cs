using Microsoft.EntityFrameworkCore.Migrations;

namespace Balto.Repository.Migrations
{
    public partial class ManyToManyIdKey : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_ProjectReadWriteUsers",
                table: "ProjectReadWriteUsers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProjectReadOnlyUsers",
                table: "ProjectReadOnlyUsers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_NoteReadWrtieUsers",
                table: "NoteReadWrtieUsers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_NoteReadOnlyUsers",
                table: "NoteReadOnlyUsers");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProjectReadWriteUsers",
                table: "ProjectReadWriteUsers",
                columns: new[] { "Id", "ProjectId", "UserId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProjectReadOnlyUsers",
                table: "ProjectReadOnlyUsers",
                columns: new[] { "Id", "ProjectId", "UserId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_NoteReadWrtieUsers",
                table: "NoteReadWrtieUsers",
                columns: new[] { "Id", "NoteId", "UserId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_NoteReadOnlyUsers",
                table: "NoteReadOnlyUsers",
                columns: new[] { "Id", "NoteId", "UserId" });

            migrationBuilder.CreateIndex(
                name: "IX_ProjectReadWriteUsers_ProjectId",
                table: "ProjectReadWriteUsers",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectReadOnlyUsers_ProjectId",
                table: "ProjectReadOnlyUsers",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_NoteReadWrtieUsers_NoteId",
                table: "NoteReadWrtieUsers",
                column: "NoteId");

            migrationBuilder.CreateIndex(
                name: "IX_NoteReadOnlyUsers_NoteId",
                table: "NoteReadOnlyUsers",
                column: "NoteId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_ProjectReadWriteUsers",
                table: "ProjectReadWriteUsers");

            migrationBuilder.DropIndex(
                name: "IX_ProjectReadWriteUsers_ProjectId",
                table: "ProjectReadWriteUsers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProjectReadOnlyUsers",
                table: "ProjectReadOnlyUsers");

            migrationBuilder.DropIndex(
                name: "IX_ProjectReadOnlyUsers_ProjectId",
                table: "ProjectReadOnlyUsers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_NoteReadWrtieUsers",
                table: "NoteReadWrtieUsers");

            migrationBuilder.DropIndex(
                name: "IX_NoteReadWrtieUsers_NoteId",
                table: "NoteReadWrtieUsers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_NoteReadOnlyUsers",
                table: "NoteReadOnlyUsers");

            migrationBuilder.DropIndex(
                name: "IX_NoteReadOnlyUsers_NoteId",
                table: "NoteReadOnlyUsers");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProjectReadWriteUsers",
                table: "ProjectReadWriteUsers",
                columns: new[] { "ProjectId", "UserId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProjectReadOnlyUsers",
                table: "ProjectReadOnlyUsers",
                columns: new[] { "ProjectId", "UserId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_NoteReadWrtieUsers",
                table: "NoteReadWrtieUsers",
                columns: new[] { "NoteId", "UserId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_NoteReadOnlyUsers",
                table: "NoteReadOnlyUsers",
                columns: new[] { "NoteId", "UserId" });
        }
    }
}
