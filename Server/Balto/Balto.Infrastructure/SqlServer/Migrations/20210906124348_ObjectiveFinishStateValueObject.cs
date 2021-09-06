using Microsoft.EntityFrameworkCore.Migrations;

namespace Balto.Infrastructure.SqlServer.Migrations
{
    public partial class ObjectiveFinishStateValueObject : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Finished",
                table: "Objectives");

            migrationBuilder.RenameColumn(
                name: "FinishDate_Value",
                table: "Objectives",
                newName: "FinishState_Date");

            migrationBuilder.AddColumn<bool>(
                name: "FinishState_State",
                table: "Objectives",
                type: "bit",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FinishState_State",
                table: "Objectives");

            migrationBuilder.RenameColumn(
                name: "FinishState_Date",
                table: "Objectives",
                newName: "FinishDate_Value");

            migrationBuilder.AddColumn<bool>(
                name: "Finished",
                table: "Objectives",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
