using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Balto.Infrastructure.SqlServer.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Objectives",
                columns: table => new
                {
                    ObjectiveId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title_Value = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description_Value = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Priority_Value = table.Column<int>(type: "int", nullable: true),
                    Daily = table.Column<bool>(type: "bit", nullable: false),
                    StartingDate_Value = table.Column<DateTime>(type: "datetime2", nullable: true),
                    EndingDate_Value = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Finished = table.Column<bool>(type: "bit", nullable: false),
                    FinishDate_Value = table.Column<DateTime>(type: "datetime2", nullable: true),
                    OwnerId_Value = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Id_Value = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Objectives", x => x.ObjectiveId);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name_Value = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email_Value = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Password_Value = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TeamId_Value = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Color_Value = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastLogin_IpAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastLogin_Date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsLeader = table.Column<bool>(type: "bit", nullable: false),
                    IsActivated = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Id_Value = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "RefreshToken",
                columns: table => new
                {
                    RefreshTokenId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Token = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Expires = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedByIp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Revoked = table.Column<DateTime>(type: "datetime2", nullable: true),
                    RevokedByIp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsRevoked = table.Column<bool>(type: "bit", nullable: false),
                    ReplacedByToken = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ownerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Id_Value = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RefreshToken", x => x.RefreshTokenId);
                    table.ForeignKey(
                        name: "FK_RefreshToken_Users_ownerId",
                        column: x => x.ownerId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RefreshToken_Id_Value",
                table: "RefreshToken",
                column: "Id_Value",
                unique: true,
                filter: "[Id_Value] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_RefreshToken_ownerId",
                table: "RefreshToken",
                column: "ownerId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_Email_Value",
                table: "Users",
                column: "Email_Value",
                unique: true,
                filter: "[Email_Value] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Users_Id_Value",
                table: "Users",
                column: "Id_Value",
                unique: true,
                filter: "[Id_Value] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Objectives");

            migrationBuilder.DropTable(
                name: "RefreshToken");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
