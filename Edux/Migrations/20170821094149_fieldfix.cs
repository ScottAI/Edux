using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Edux.Migrations
{
    public partial class fieldfix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Row",
                table: "Fields",
                maxLength: 200,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 200,
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Col",
                table: "Fields",
                maxLength: 200,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 200,
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Row",
                table: "Fields",
                maxLength: 200,
                nullable: true,
                oldClrType: typeof(int),
                oldMaxLength: 200);

            migrationBuilder.AlterColumn<string>(
                name: "Col",
                table: "Fields",
                maxLength: 200,
                nullable: true,
                oldClrType: typeof(int),
                oldMaxLength: 200);
        }
    }
}
