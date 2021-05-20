using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Balto.Repository.Migrations
{
    public partial class EntryDatesAndColors : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Color",
                table: "Users",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Color",
                table: "Teams",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "EndingDate",
                table: "ProjectTableEntries",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "FinishDate",
                table: "ProjectTableEntries",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Notify",
                table: "ProjectTableEntries",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "StartingDate",
                table: "ProjectTableEntries",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "Notify",
                table: "Objectives",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Color",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Color",
                table: "Teams");

            migrationBuilder.DropColumn(
                name: "EndingDate",
                table: "ProjectTableEntries");

            migrationBuilder.DropColumn(
                name: "FinishDate",
                table: "ProjectTableEntries");

            migrationBuilder.DropColumn(
                name: "Notify",
                table: "ProjectTableEntries");

            migrationBuilder.DropColumn(
                name: "StartingDate",
                table: "ProjectTableEntries");

            migrationBuilder.DropColumn(
                name: "Notify",
                table: "Objectives");
        }
    }
}
