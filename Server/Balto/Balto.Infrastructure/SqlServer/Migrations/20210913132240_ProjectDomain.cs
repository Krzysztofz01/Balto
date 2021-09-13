using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Balto.Infrastructure.SqlServer.Migrations
{
    public partial class ProjectDomain : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "balto");

            migrationBuilder.RenameTable(
                name: "Users",
                newName: "Users",
                newSchema: "balto");

            migrationBuilder.RenameTable(
                name: "RefreshToken",
                newName: "RefreshToken",
                newSchema: "balto");

            migrationBuilder.RenameTable(
                name: "Objectives",
                newName: "Objectives",
                newSchema: "balto");

            migrationBuilder.RenameTable(
                name: "Notes",
                newName: "Notes",
                newSchema: "balto");

            migrationBuilder.RenameTable(
                name: "NoteContributor",
                newName: "NoteContributor",
                newSchema: "balto");

            migrationBuilder.CreateTable(
                name: "Projects",
                schema: "balto",
                columns: table => new
                {
                    ProjectId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title_Value = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OwnerId_Value = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    TicketToken_Value = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Id_Value = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Projects", x => x.ProjectId);
                });

            migrationBuilder.CreateTable(
                name: "ProjectContributor",
                schema: "balto",
                columns: table => new
                {
                    ProjectContributorId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    projectId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Id_Value = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectContributor", x => x.ProjectContributorId);
                    table.ForeignKey(
                        name: "FK_ProjectContributor_Projects_projectId",
                        column: x => x.projectId,
                        principalSchema: "balto",
                        principalTable: "Projects",
                        principalColumn: "ProjectId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProjectTable",
                schema: "balto",
                columns: table => new
                {
                    TableId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title_Value = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Color_Value = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    projectId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Id_Value = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectTable", x => x.TableId);
                    table.ForeignKey(
                        name: "FK_ProjectTable_Projects_projectId",
                        column: x => x.projectId,
                        principalSchema: "balto",
                        principalTable: "Projects",
                        principalColumn: "ProjectId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProjectTableCard",
                schema: "balto",
                columns: table => new
                {
                    CardId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title_Value = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Content_Value = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Color_Value = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatorId_Value = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    StartingDate_Value = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Deadline_Date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Deadline_UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Deadline_Notify = table.Column<bool>(type: "bit", nullable: true),
                    Finished_Finished = table.Column<bool>(type: "bit", nullable: true),
                    Finished_FinishedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Finished_FinishedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Priority_Value = table.Column<int>(type: "int", nullable: true),
                    OrdinalNumber = table.Column<int>(type: "int", nullable: false),
                    tableId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Id_Value = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectTableCard", x => x.CardId);
                    table.ForeignKey(
                        name: "FK_ProjectTableCard_ProjectTable_tableId",
                        column: x => x.tableId,
                        principalSchema: "balto",
                        principalTable: "ProjectTable",
                        principalColumn: "TableId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProjectTableCardComment",
                schema: "balto",
                columns: table => new
                {
                    CommentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Content_Value = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatorId_Value = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreateDate_Value = table.Column<DateTime>(type: "datetime2", nullable: true),
                    cardId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Id_Value = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectTableCardComment", x => x.CommentId);
                    table.ForeignKey(
                        name: "FK_ProjectTableCardComment_ProjectTableCard_cardId",
                        column: x => x.cardId,
                        principalSchema: "balto",
                        principalTable: "ProjectTableCard",
                        principalColumn: "CardId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProjectContributor_Id_Value",
                schema: "balto",
                table: "ProjectContributor",
                column: "Id_Value",
                unique: true,
                filter: "[Id_Value] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectContributor_projectId",
                schema: "balto",
                table: "ProjectContributor",
                column: "projectId");

            migrationBuilder.CreateIndex(
                name: "IX_Projects_Id_Value",
                schema: "balto",
                table: "Projects",
                column: "Id_Value",
                unique: true,
                filter: "[Id_Value] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Projects_TicketToken_Value",
                schema: "balto",
                table: "Projects",
                column: "TicketToken_Value",
                unique: true,
                filter: "[TicketToken_Value] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectTable_Id_Value",
                schema: "balto",
                table: "ProjectTable",
                column: "Id_Value",
                unique: true,
                filter: "[Id_Value] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectTable_projectId",
                schema: "balto",
                table: "ProjectTable",
                column: "projectId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectTableCard_Id_Value",
                schema: "balto",
                table: "ProjectTableCard",
                column: "Id_Value",
                unique: true,
                filter: "[Id_Value] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectTableCard_tableId",
                schema: "balto",
                table: "ProjectTableCard",
                column: "tableId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectTableCardComment_cardId",
                schema: "balto",
                table: "ProjectTableCardComment",
                column: "cardId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectTableCardComment_Id_Value",
                schema: "balto",
                table: "ProjectTableCardComment",
                column: "Id_Value",
                unique: true,
                filter: "[Id_Value] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProjectContributor",
                schema: "balto");

            migrationBuilder.DropTable(
                name: "ProjectTableCardComment",
                schema: "balto");

            migrationBuilder.DropTable(
                name: "ProjectTableCard",
                schema: "balto");

            migrationBuilder.DropTable(
                name: "ProjectTable",
                schema: "balto");

            migrationBuilder.DropTable(
                name: "Projects",
                schema: "balto");

            migrationBuilder.RenameTable(
                name: "Users",
                schema: "balto",
                newName: "Users");

            migrationBuilder.RenameTable(
                name: "RefreshToken",
                schema: "balto",
                newName: "RefreshToken");

            migrationBuilder.RenameTable(
                name: "Objectives",
                schema: "balto",
                newName: "Objectives");

            migrationBuilder.RenameTable(
                name: "Notes",
                schema: "balto",
                newName: "Notes");

            migrationBuilder.RenameTable(
                name: "NoteContributor",
                schema: "balto",
                newName: "NoteContributor");
        }
    }
}
