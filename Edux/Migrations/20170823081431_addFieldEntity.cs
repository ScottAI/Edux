using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Edux.Migrations
{
    public partial class addFieldEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "EntityId",
                table: "Fields",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Fields_EntityId",
                table: "Fields",
                column: "EntityId");

            migrationBuilder.AddForeignKey(
                name: "FK_Fields_Entities_EntityId",
                table: "Fields",
                column: "EntityId",
                principalTable: "Entities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Fields_Entities_EntityId",
                table: "Fields");

            migrationBuilder.DropIndex(
                name: "IX_Fields_EntityId",
                table: "Fields");

            migrationBuilder.DropColumn(
                name: "EntityId",
                table: "Fields");
        }
    }
}
