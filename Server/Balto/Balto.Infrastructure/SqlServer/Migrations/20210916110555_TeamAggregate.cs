using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Balto.Infrastructure.SqlServer.Migrations
{
    public partial class TeamAggregate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Teams",
                schema: "balto",
                columns: table => new
                {
                    TeamId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name_Value = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Color_Value = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Id_Value = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Teams", x => x.TeamId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Teams_Id_Value",
                schema: "balto",
                table: "Teams",
                column: "Id_Value",
                unique: true,
                filter: "[Id_Value] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Teams",
                schema: "balto");
        }
    }
}
