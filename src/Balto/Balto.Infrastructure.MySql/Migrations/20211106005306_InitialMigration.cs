using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Balto.Infrastructure.MySql.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "balto");

            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Goals",
                schema: "balto",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    OwnerId_Value = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci"),
                    Title_Value = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Description_Value = table.Column<string>(type: "varchar(300)", maxLength: 300, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Priority_Value = table.Column<int>(type: "int", nullable: true),
                    Color_Value = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    StartingDate_Value = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    Deadline_Value = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    IsRecurring_Value = table.Column<bool>(type: "tinyint(1)", nullable: true),
                    Status_Finished = table.Column<bool>(type: "tinyint(1)", nullable: true),
                    Status_FinishDate = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Goals", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Identities",
                schema: "balto",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Name_Value = table.Column<string>(type: "varchar(40)", maxLength: 40, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Email_Value = table.Column<string>(type: "varchar(255)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    PasswordHash_Value = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    LastLogin_IpAddress = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    LastLogin_Date = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    Role_Value = table.Column<int>(type: "int", nullable: true),
                    Activation_Value = table.Column<bool>(type: "tinyint(1)", nullable: true),
                    Color_Value = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    TeamId_Value = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci"),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Identities", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Projects",
                schema: "balto",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Title_Value = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    OwnerId_Value = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci"),
                    TicketToken_Value = table.Column<string>(type: "varchar(255)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Projects", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Token",
                schema: "balto",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    TokenHash = table.Column<string>(type: "varchar(255)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Expires = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    CreatedByIpAddress = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Revoked = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    RevokedByIpAddress = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    IsRevoked = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    ReplacedByTokenHash = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    identityId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Token", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Token_Identities_identityId",
                        column: x => x.identityId,
                        principalSchema: "balto",
                        principalTable: "Identities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "ProjectContributor",
                schema: "balto",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    IdentityId_Value = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci"),
                    Role_Value = table.Column<int>(type: "int", nullable: true),
                    projectId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectContributor", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProjectContributor_Projects_projectId",
                        column: x => x.projectId,
                        principalSchema: "balto",
                        principalTable: "Projects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "ProjectTable",
                schema: "balto",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Title_Value = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Color_Value = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    projectId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectTable", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProjectTable_Projects_projectId",
                        column: x => x.projectId,
                        principalSchema: "balto",
                        principalTable: "Projects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "ProjectTask",
                schema: "balto",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Title_Value = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Content_Value = table.Column<string>(type: "varchar(300)", maxLength: 300, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Color_Value = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CreatorId_Value = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci"),
                    AssignedContributorId_Value = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci"),
                    StartingDate_Value = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    Deadline_Value = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    Status_Finished = table.Column<bool>(type: "tinyint(1)", nullable: true),
                    Status_FinishDate = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    Status_FinishedBy = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci"),
                    Priority_Value = table.Column<int>(type: "int", nullable: true),
                    OrdinalNumber_Value = table.Column<int>(type: "int", nullable: true),
                    tableId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectTask", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProjectTask_ProjectTable_tableId",
                        column: x => x.tableId,
                        principalSchema: "balto",
                        principalTable: "ProjectTable",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Identities_Email_Value",
                schema: "balto",
                table: "Identities",
                column: "Email_Value",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProjectContributor_projectId",
                schema: "balto",
                table: "ProjectContributor",
                column: "projectId");

            migrationBuilder.CreateIndex(
                name: "IX_Projects_TicketToken_Value",
                schema: "balto",
                table: "Projects",
                column: "TicketToken_Value",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProjectTable_projectId",
                schema: "balto",
                table: "ProjectTable",
                column: "projectId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectTask_tableId",
                schema: "balto",
                table: "ProjectTask",
                column: "tableId");

            migrationBuilder.CreateIndex(
                name: "IX_Token_identityId",
                schema: "balto",
                table: "Token",
                column: "identityId");

            migrationBuilder.CreateIndex(
                name: "IX_Token_TokenHash",
                schema: "balto",
                table: "Token",
                column: "TokenHash",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Goals",
                schema: "balto");

            migrationBuilder.DropTable(
                name: "ProjectContributor",
                schema: "balto");

            migrationBuilder.DropTable(
                name: "ProjectTask",
                schema: "balto");

            migrationBuilder.DropTable(
                name: "Token",
                schema: "balto");

            migrationBuilder.DropTable(
                name: "ProjectTable",
                schema: "balto");

            migrationBuilder.DropTable(
                name: "Identities",
                schema: "balto");

            migrationBuilder.DropTable(
                name: "Projects",
                schema: "balto");
        }
    }
}
