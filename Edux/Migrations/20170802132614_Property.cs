using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Edux.Migrations
{
    public partial class Property : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DataType",
                table: "Properties");

            migrationBuilder.AddColumn<string>(
                name: "DataSourceEntity",
                table: "Properties",
                maxLength: 200,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DataSourceProperty",
                table: "Properties",
                maxLength: 200,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DefaultValue",
                table: "Properties",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DisplayFormat",
                table: "Properties",
                maxLength: 200,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DataSourceEntity",
                table: "Properties");

            migrationBuilder.DropColumn(
                name: "DataSourceProperty",
                table: "Properties");

            migrationBuilder.DropColumn(
                name: "DefaultValue",
                table: "Properties");

            migrationBuilder.DropColumn(
                name: "DisplayFormat",
                table: "Properties");

            migrationBuilder.AddColumn<int>(
                name: "DataType",
                table: "Properties",
                nullable: true);
        }
    }
}
