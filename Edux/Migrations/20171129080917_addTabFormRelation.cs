using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Edux.Migrations
{
    public partial class addTabFormRelation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FormId",
                table: "Tabs",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "DataTableId",
                table: "Fields",
                type: "nvarchar(450)",
                maxLength: 200,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 200,
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Tabs_FormId",
                table: "Tabs",
                column: "FormId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tabs_Forms_FormId",
                table: "Tabs",
                column: "FormId",
                principalTable: "Forms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tabs_Forms_FormId",
                table: "Tabs");

            migrationBuilder.DropIndex(
                name: "IX_Tabs_FormId",
                table: "Tabs");

            migrationBuilder.DropColumn(
                name: "FormId",
                table: "Tabs");

            migrationBuilder.AlterColumn<string>(
                name: "DataTableId",
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
