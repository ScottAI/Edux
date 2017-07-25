using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Edux.Migrations
{
    public partial class Media : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Media",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    AppTenantId = table.Column<string>(maxLength: 200, nullable: true),
                    ContentType = table.Column<string>(maxLength: 200, nullable: true),
                    CreateDate = table.Column<DateTime>(nullable: false),
                    CreatedBy = table.Column<string>(maxLength: 200, nullable: true),
                    Description = table.Column<string>(nullable: true),
                    Extension = table.Column<string>(maxLength: 200, nullable: true),
                    FilePath = table.Column<string>(maxLength: 200, nullable: true),
                    FileSize = table.Column<float>(nullable: false),
                    Month = table.Column<int>(nullable: false),
                    Name = table.Column<string>(maxLength: 200, nullable: false),
                    UpdateDate = table.Column<DateTime>(nullable: false),
                    UpdatedBy = table.Column<string>(maxLength: 200, nullable: true),
                    Year = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Media", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Media");
        }
    }
}
