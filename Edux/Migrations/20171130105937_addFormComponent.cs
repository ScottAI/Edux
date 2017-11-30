using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Edux.Migrations
{
    public partial class addFormComponent : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "DataTableId",
                table: "Fields",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 200,
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ComponentId",
                table: "Fields",
                type: "nvarchar(450)",
                maxLength: 200,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FormId",
                table: "Components",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Fields_ComponentId",
                table: "Fields",
                column: "ComponentId");

            migrationBuilder.CreateIndex(
                name: "IX_Components_FormId",
                table: "Components",
                column: "FormId");

            migrationBuilder.AddForeignKey(
                name: "FK_Components_Forms_FormId",
                table: "Components",
                column: "FormId",
                principalTable: "Forms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Fields_Components_ComponentId",
                table: "Fields",
                column: "ComponentId",
                principalTable: "Components",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Components_Forms_FormId",
                table: "Components");

            migrationBuilder.DropForeignKey(
                name: "FK_Fields_Components_ComponentId",
                table: "Fields");

            migrationBuilder.DropIndex(
                name: "IX_Fields_ComponentId",
                table: "Fields");

            migrationBuilder.DropIndex(
                name: "IX_Components_FormId",
                table: "Components");

            migrationBuilder.DropColumn(
                name: "ComponentId",
                table: "Fields");

            migrationBuilder.DropColumn(
                name: "FormId",
                table: "Components");

            migrationBuilder.AlterColumn<string>(
                name: "DataTableId",
                table: "Fields",
                maxLength: 200,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);
        }
    }
}
