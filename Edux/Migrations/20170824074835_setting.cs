using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Edux.Migrations
{
    public partial class setting : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Settings",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    AppTenantId = table.Column<string>(maxLength: 200, nullable: true),
                    ComponentViews = table.Column<string>(nullable: true),
                    CreateDate = table.Column<DateTime>(nullable: false),
                    CreatedBy = table.Column<string>(maxLength: 200, nullable: true),
                    LayoutViews = table.Column<string>(nullable: true),
                    PageViews = table.Column<string>(nullable: true),
                    SmtpHost = table.Column<string>(maxLength: 200, nullable: true),
                    SmtpPassword = table.Column<string>(maxLength: 200, nullable: true),
                    SmtpPort = table.Column<string>(maxLength: 200, nullable: true),
                    SmtpUseSSL = table.Column<bool>(nullable: false),
                    SmtpUserName = table.Column<string>(maxLength: 200, nullable: true),
                    UpdateDate = table.Column<DateTime>(nullable: false),
                    UpdatedBy = table.Column<string>(maxLength: 200, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Settings", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Settings");
        }
    }
}
