using Microsoft.EntityFrameworkCore.Migrations;

namespace Balto.Repository.Migrations
{
    public partial class UserAddFinishedEntry : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "UserAddedId",
                table: "ProjectTableEntries",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "UserFinishedId",
                table: "ProjectTableEntries",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProjectTableEntries_UserAddedId",
                table: "ProjectTableEntries",
                column: "UserAddedId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectTableEntries_UserFinishedId",
                table: "ProjectTableEntries",
                column: "UserFinishedId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectTableEntries_Users_UserAddedId",
                table: "ProjectTableEntries",
                column: "UserAddedId",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectTableEntries_Users_UserFinishedId",
                table: "ProjectTableEntries",
                column: "UserFinishedId",
                principalTable: "Users",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProjectTableEntries_Users_UserAddedId",
                table: "ProjectTableEntries");

            migrationBuilder.DropForeignKey(
                name: "FK_ProjectTableEntries_Users_UserFinishedId",
                table: "ProjectTableEntries");

            migrationBuilder.DropIndex(
                name: "IX_ProjectTableEntries_UserAddedId",
                table: "ProjectTableEntries");

            migrationBuilder.DropIndex(
                name: "IX_ProjectTableEntries_UserFinishedId",
                table: "ProjectTableEntries");

            migrationBuilder.DropColumn(
                name: "UserAddedId",
                table: "ProjectTableEntries");

            migrationBuilder.DropColumn(
                name: "UserFinishedId",
                table: "ProjectTableEntries");
        }
    }
}
