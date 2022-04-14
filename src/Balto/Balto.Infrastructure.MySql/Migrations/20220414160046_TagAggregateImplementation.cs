using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Balto.Infrastructure.MySql.Migrations
{
    public partial class TagAggregateImplementation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "GoalTag",
                schema: "balto",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    TagId_Value = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci"),
                    goalId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GoalTag", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GoalTag_Goals_goalId",
                        column: x => x.goalId,
                        principalSchema: "balto",
                        principalTable: "Goals",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "ProjectTag",
                schema: "balto",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    TagId_Value = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci"),
                    taskId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectTag", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProjectTag_ProjectTask_taskId",
                        column: x => x.taskId,
                        principalSchema: "balto",
                        principalTable: "ProjectTask",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Tags",
                schema: "balto",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Title_Value = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Color_Value = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tags", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_GoalTag_goalId",
                schema: "balto",
                table: "GoalTag",
                column: "goalId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectTag_taskId",
                schema: "balto",
                table: "ProjectTag",
                column: "taskId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GoalTag",
                schema: "balto");

            migrationBuilder.DropTable(
                name: "ProjectTag",
                schema: "balto");

            migrationBuilder.DropTable(
                name: "Tags",
                schema: "balto");
        }
    }
}
