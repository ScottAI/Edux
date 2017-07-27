using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Edux.Migrations
{
    public partial class pageComponentsRemoved : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PageComponents");

            migrationBuilder.AddColumn<string>(
                name: "PageId",
                table: "Components",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Position",
                table: "Components",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Components_PageId",
                table: "Components",
                column: "PageId");

            migrationBuilder.AddForeignKey(
                name: "FK_Components_Pages_PageId",
                table: "Components",
                column: "PageId",
                principalTable: "Pages",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Components_Pages_PageId",
                table: "Components");

            migrationBuilder.DropIndex(
                name: "IX_Components_PageId",
                table: "Components");

            migrationBuilder.DropColumn(
                name: "PageId",
                table: "Components");

            migrationBuilder.DropColumn(
                name: "Position",
                table: "Components");

            migrationBuilder.CreateTable(
                name: "PageComponents",
                columns: table => new
                {
                    PageId = table.Column<string>(nullable: false),
                    ComponentId = table.Column<string>(nullable: false),
                    AppTenantId = table.Column<string>(maxLength: 200, nullable: true),
                    CreateDate = table.Column<DateTime>(nullable: false),
                    CreatedBy = table.Column<string>(maxLength: 200, nullable: true),
                    Id = table.Column<string>(nullable: true),
                    Position = table.Column<int>(nullable: false),
                    UpdateDate = table.Column<DateTime>(nullable: false),
                    UpdatedBy = table.Column<string>(maxLength: 200, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PageComponents", x => new { x.PageId, x.ComponentId });
                    table.ForeignKey(
                        name: "FK_PageComponents_Components_ComponentId",
                        column: x => x.ComponentId,
                        principalTable: "Components",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PageComponents_Pages_PageId",
                        column: x => x.PageId,
                        principalTable: "Pages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PageComponents_ComponentId",
                table: "PageComponents",
                column: "ComponentId");
        }
    }
}
