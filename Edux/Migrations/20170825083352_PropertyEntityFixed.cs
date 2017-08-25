using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Edux.Migrations
{
    public partial class PropertyEntityFixed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Properties_Entities_EntityId",
                table: "Properties");

            migrationBuilder.DropColumn(
                name: "DataSourceEntity",
                table: "Properties");

            migrationBuilder.DropColumn(
                name: "DataSourceProperty",
                table: "Properties");

            migrationBuilder.AddColumn<string>(
                name: "DataSourceEntityId",
                table: "Properties",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DataSourcePropertyId",
                table: "Properties",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Properties_DataSourceEntityId",
                table: "Properties",
                column: "DataSourceEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_Properties_DataSourcePropertyId",
                table: "Properties",
                column: "DataSourcePropertyId");

            migrationBuilder.AddForeignKey(
                name: "FK_Properties_Entities_DataSourceEntityId",
                table: "Properties",
                column: "DataSourceEntityId",
                principalTable: "Entities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Properties_Properties_DataSourcePropertyId",
                table: "Properties",
                column: "DataSourcePropertyId",
                principalTable: "Properties",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Properties_Entities_EntityId",
                table: "Properties",
                column: "EntityId",
                principalTable: "Entities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Properties_Entities_DataSourceEntityId",
                table: "Properties");

            migrationBuilder.DropForeignKey(
                name: "FK_Properties_Properties_DataSourcePropertyId",
                table: "Properties");

            migrationBuilder.DropForeignKey(
                name: "FK_Properties_Entities_EntityId",
                table: "Properties");

            migrationBuilder.DropIndex(
                name: "IX_Properties_DataSourceEntityId",
                table: "Properties");

            migrationBuilder.DropIndex(
                name: "IX_Properties_DataSourcePropertyId",
                table: "Properties");

            migrationBuilder.DropColumn(
                name: "DataSourceEntityId",
                table: "Properties");

            migrationBuilder.DropColumn(
                name: "DataSourcePropertyId",
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

            migrationBuilder.AddForeignKey(
                name: "FK_Properties_Entities_EntityId",
                table: "Properties",
                column: "EntityId",
                principalTable: "Entities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
