using Microsoft.EntityFrameworkCore.Migrations;

namespace Balto.Infrastructure.SqlServer.Migrations
{
    public partial class ObjectivesPeriodicity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Daily",
                table: "Objectives");

            migrationBuilder.AddColumn<int>(
                name: "Periodicity_Value",
                table: "Objectives",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Objectives_Id_Value",
                table: "Objectives",
                column: "Id_Value",
                unique: true,
                filter: "[Id_Value] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Objectives_Id_Value",
                table: "Objectives");

            migrationBuilder.DropColumn(
                name: "Periodicity_Value",
                table: "Objectives");

            migrationBuilder.AddColumn<bool>(
                name: "Daily",
                table: "Objectives",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
