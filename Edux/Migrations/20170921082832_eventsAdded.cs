using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Edux.Migrations
{
    public partial class eventsAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CssClass",
                table: "Fields",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OnChange",
                table: "Fields",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OnClick",
                table: "Fields",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OnLoad",
                table: "Entities",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CssClass",
                table: "Fields");

            migrationBuilder.DropColumn(
                name: "OnChange",
                table: "Fields");

            migrationBuilder.DropColumn(
                name: "OnClick",
                table: "Fields");

            migrationBuilder.DropColumn(
                name: "OnLoad",
                table: "Entities");
        }
    }
}
