using Microsoft.EntityFrameworkCore.Migrations;

namespace Balto.Repository.Migrations
{
    public partial class ObjectiveDailyProp : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Daily",
                table: "Objectives",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Daily",
                table: "Objectives");
        }
    }
}
