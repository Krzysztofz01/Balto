using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Balto.Repository.Migrations
{
    public partial class AllBaseEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "ProjectTableEntries",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "Finished",
                table: "ProjectTableEntries",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AlterColumn<DateTime>(
                name: "EditDate",
                table: "ProjectTableEntries",
                nullable: false,
                defaultValueSql: "getdate()",
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<string>(
                name: "Content",
                table: "ProjectTableEntries",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "AddDate",
                table: "ProjectTableEntries",
                nullable: false,
                defaultValueSql: "getdate()",
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AddColumn<DateTime>(
                name: "AddDate",
                table: "ProjectReadWriteUsers",
                nullable: false,
                defaultValueSql: "getdate()");

            migrationBuilder.AddColumn<DateTime>(
                name: "EditDate",
                table: "ProjectReadWriteUsers",
                nullable: false,
                defaultValueSql: "getdate()");

            migrationBuilder.AddColumn<long>(
                name: "Id",
                table: "ProjectReadWriteUsers",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<DateTime>(
                name: "AddDate",
                table: "ProjectReadOnlyUsers",
                nullable: false,
                defaultValueSql: "getdate()");

            migrationBuilder.AddColumn<DateTime>(
                name: "EditDate",
                table: "ProjectReadOnlyUsers",
                nullable: false,
                defaultValueSql: "getdate()");

            migrationBuilder.AddColumn<long>(
                name: "Id",
                table: "ProjectReadOnlyUsers",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<DateTime>(
                name: "AddDate",
                table: "NoteReadWrtieUsers",
                nullable: false,
                defaultValueSql: "getdate()");

            migrationBuilder.AddColumn<DateTime>(
                name: "EditDate",
                table: "NoteReadWrtieUsers",
                nullable: false,
                defaultValueSql: "getdate()");

            migrationBuilder.AddColumn<long>(
                name: "Id",
                table: "NoteReadWrtieUsers",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<DateTime>(
                name: "AddDate",
                table: "NoteReadOnlyUsers",
                nullable: false,
                defaultValueSql: "getdate()");

            migrationBuilder.AddColumn<DateTime>(
                name: "EditDate",
                table: "NoteReadOnlyUsers",
                nullable: false,
                defaultValueSql: "getdate()");

            migrationBuilder.AddColumn<long>(
                name: "Id",
                table: "NoteReadOnlyUsers",
                nullable: false,
                defaultValue: 0L);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AddDate",
                table: "ProjectReadWriteUsers");

            migrationBuilder.DropColumn(
                name: "EditDate",
                table: "ProjectReadWriteUsers");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "ProjectReadWriteUsers");

            migrationBuilder.DropColumn(
                name: "AddDate",
                table: "ProjectReadOnlyUsers");

            migrationBuilder.DropColumn(
                name: "EditDate",
                table: "ProjectReadOnlyUsers");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "ProjectReadOnlyUsers");

            migrationBuilder.DropColumn(
                name: "AddDate",
                table: "NoteReadWrtieUsers");

            migrationBuilder.DropColumn(
                name: "EditDate",
                table: "NoteReadWrtieUsers");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "NoteReadWrtieUsers");

            migrationBuilder.DropColumn(
                name: "AddDate",
                table: "NoteReadOnlyUsers");

            migrationBuilder.DropColumn(
                name: "EditDate",
                table: "NoteReadOnlyUsers");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "NoteReadOnlyUsers");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "ProjectTableEntries",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<bool>(
                name: "Finished",
                table: "ProjectTableEntries",
                type: "bit",
                nullable: false,
                oldClrType: typeof(bool),
                oldDefaultValue: false);

            migrationBuilder.AlterColumn<DateTime>(
                name: "EditDate",
                table: "ProjectTableEntries",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldDefaultValueSql: "getdate()");

            migrationBuilder.AlterColumn<string>(
                name: "Content",
                table: "ProjectTableEntries",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "AddDate",
                table: "ProjectTableEntries",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldDefaultValueSql: "getdate()");
        }
    }
}
