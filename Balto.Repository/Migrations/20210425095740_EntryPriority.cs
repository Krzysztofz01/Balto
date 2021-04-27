using Microsoft.EntityFrameworkCore.Migrations;

namespace Balto.Repository.Migrations
{
    public partial class EntryPriority : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Priority",
                table: "ProjectTableEntries",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Priority",
                table: "ProjectTableEntries");
        }
    }
}
