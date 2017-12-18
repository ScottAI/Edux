using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Edux.Migrations
{
    public partial class tabChange : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDisabled",
                table: "Tabs");

            migrationBuilder.DropColumn(
                name: "IsInvisible",
                table: "Tabs");

           /* migrationBuilder.DropColumn(
                name: "Photo",
                table: "Parameters");*/

            migrationBuilder.AddColumn<string>(
                name: "EditableToRoles",
                table: "Tabs",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "InvisibleToRoles",
                table: "Tabs",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsReadOnly",
                table: "Tabs",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsVisible",
                table: "Tabs",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "ReadOnlyToRoles",
                table: "Tabs",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ComponentId",
                table: "Fields",
                type: "nvarchar(450)",
                maxLength: 200,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 200,
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EditableToRoles",
                table: "Tabs");

            migrationBuilder.DropColumn(
                name: "InvisibleToRoles",
                table: "Tabs");

            migrationBuilder.DropColumn(
                name: "IsReadOnly",
                table: "Tabs");

            migrationBuilder.DropColumn(
                name: "IsVisible",
                table: "Tabs");

            migrationBuilder.DropColumn(
                name: "ReadOnlyToRoles",
                table: "Tabs");

            migrationBuilder.AddColumn<bool>(
                name: "IsDisabled",
                table: "Tabs",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsInvisible",
                table: "Tabs",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Photo",
                table: "Parameters",
                maxLength: 200,
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ComponentId",
                table: "Fields",
                maxLength: 200,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldMaxLength: 200,
                oldNullable: true);
        }
    }
}
