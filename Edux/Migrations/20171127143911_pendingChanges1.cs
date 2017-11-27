using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Edux.Migrations
{
    public partial class pendingChanges1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Fields_PropertyValues_PropertyValueId",
                table: "Fields");

            migrationBuilder.DropForeignKey(
                name: "FK_Properties_Properties_DataSourcePropertyId",
                table: "Properties");

            migrationBuilder.DropIndex(
                name: "IX_Properties_DataSourcePropertyId",
                table: "Properties");

            migrationBuilder.DropIndex(
                name: "IX_Fields_PropertyValueId",
                table: "Fields");

            migrationBuilder.DropColumn(
                name: "PropertyValueId",
                table: "Fields");

            migrationBuilder.DropColumn(
                name: "Tab",
                table: "Fields");

            migrationBuilder.DropColumn(
                name: "OnLoad",
                table: "Entities");

            migrationBuilder.DropColumn(
                name: "EntityName",
                table: "DataTables");

            migrationBuilder.AlterColumn<string>(
                name: "DataSourcePropertyId",
                table: "Properties",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DataSourcePropertyId2",
                table: "Properties",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DataSourcePropertyId3",
                table: "Properties",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EntityId1",
                table: "Properties",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EntityId2",
                table: "Properties",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DataSourceEntityId",
                table: "Parameters",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DataSourcePropertyId",
                table: "Parameters",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DataSourcePropertyId2",
                table: "Parameters",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DataSourcePropertyId3",
                table: "Parameters",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OptionLabel",
                table: "Parameters",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ParameterType",
                table: "Parameters",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "PresetValues",
                table: "Parameters",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Scripts",
                table: "Pages",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EntityId",
                table: "Forms",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Scripts",
                table: "Forms",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DataTableId",
                table: "Fields",
                type: "nvarchar(450)",
                maxLength: 200,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EditableToRoles",
                table: "Fields",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FieldSet",
                table: "Fields",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "InvisibleToRoles",
                table: "Fields",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsReadOnly",
                table: "Fields",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsVisible",
                table: "Fields",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "ReadOnlyToRoles",
                table: "Fields",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TabId",
                table: "Fields",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "VisibleToRoles",
                table: "Fields",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EntityId",
                table: "DataTables",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CssClass",
                table: "Columns",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsVisible",
                table: "Columns",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "AllowAll",
                table: "AspNetRoles",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "AllowCreate",
                table: "AspNetRoles",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "AllowDelete",
                table: "AspNetRoles",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "AllowRead",
                table: "AspNetRoles",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "AllowSpecial",
                table: "AspNetRoles",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "AllowUpdate",
                table: "AspNetRoles",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "RoleGroupId",
                table: "AspNetRoles",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "RoleGroups",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    AppTenantId = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedBy = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoleGroups", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Tabs",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    AppTenantId = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    IsDisabled = table.Column<bool>(type: "bit", nullable: false),
                    IsInvisible = table.Column<bool>(type: "bit", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedBy = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    VisibleToRoles = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tabs", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Properties_EntityId1",
                table: "Properties",
                column: "EntityId1");

            migrationBuilder.CreateIndex(
                name: "IX_Properties_EntityId2",
                table: "Properties",
                column: "EntityId2");

            migrationBuilder.CreateIndex(
                name: "IX_Parameters_DataSourceEntityId",
                table: "Parameters",
                column: "DataSourceEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_Parameters_DataSourcePropertyId",
                table: "Parameters",
                column: "DataSourcePropertyId");

            migrationBuilder.CreateIndex(
                name: "IX_Parameters_DataSourcePropertyId2",
                table: "Parameters",
                column: "DataSourcePropertyId2");

            migrationBuilder.CreateIndex(
                name: "IX_Parameters_DataSourcePropertyId3",
                table: "Parameters",
                column: "DataSourcePropertyId3");

            migrationBuilder.CreateIndex(
                name: "IX_Forms_EntityId",
                table: "Forms",
                column: "EntityId");

            migrationBuilder.CreateIndex(
                name: "IX_Fields_DataTableId",
                table: "Fields",
                column: "DataTableId");

            migrationBuilder.CreateIndex(
                name: "IX_Fields_TabId",
                table: "Fields",
                column: "TabId");

            migrationBuilder.CreateIndex(
                name: "IX_DataTables_EntityId",
                table: "DataTables",
                column: "EntityId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoles_RoleGroupId",
                table: "AspNetRoles",
                column: "RoleGroupId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetRoles_RoleGroups_RoleGroupId",
                table: "AspNetRoles",
                column: "RoleGroupId",
                principalTable: "RoleGroups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_DataTables_Entities_EntityId",
                table: "DataTables",
                column: "EntityId",
                principalTable: "Entities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Fields_DataTables_DataTableId",
                table: "Fields",
                column: "DataTableId",
                principalTable: "DataTables",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Fields_Tabs_TabId",
                table: "Fields",
                column: "TabId",
                principalTable: "Tabs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Forms_Entities_EntityId",
                table: "Forms",
                column: "EntityId",
                principalTable: "Entities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Parameters_Entities_DataSourceEntityId",
                table: "Parameters",
                column: "DataSourceEntityId",
                principalTable: "Entities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Parameters_Properties_DataSourcePropertyId",
                table: "Parameters",
                column: "DataSourcePropertyId",
                principalTable: "Properties",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Parameters_Properties_DataSourcePropertyId2",
                table: "Parameters",
                column: "DataSourcePropertyId2",
                principalTable: "Properties",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Parameters_Properties_DataSourcePropertyId3",
                table: "Parameters",
                column: "DataSourcePropertyId3",
                principalTable: "Properties",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Properties_Entities_EntityId1",
                table: "Properties",
                column: "EntityId1",
                principalTable: "Entities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Properties_Entities_EntityId2",
                table: "Properties",
                column: "EntityId2",
                principalTable: "Entities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetRoles_RoleGroups_RoleGroupId",
                table: "AspNetRoles");

            migrationBuilder.DropForeignKey(
                name: "FK_DataTables_Entities_EntityId",
                table: "DataTables");

            migrationBuilder.DropForeignKey(
                name: "FK_Fields_DataTables_DataTableId",
                table: "Fields");

            migrationBuilder.DropForeignKey(
                name: "FK_Fields_Tabs_TabId",
                table: "Fields");

            migrationBuilder.DropForeignKey(
                name: "FK_Forms_Entities_EntityId",
                table: "Forms");

            migrationBuilder.DropForeignKey(
                name: "FK_Parameters_Entities_DataSourceEntityId",
                table: "Parameters");

            migrationBuilder.DropForeignKey(
                name: "FK_Parameters_Properties_DataSourcePropertyId",
                table: "Parameters");

            migrationBuilder.DropForeignKey(
                name: "FK_Parameters_Properties_DataSourcePropertyId2",
                table: "Parameters");

            migrationBuilder.DropForeignKey(
                name: "FK_Parameters_Properties_DataSourcePropertyId3",
                table: "Parameters");

            migrationBuilder.DropForeignKey(
                name: "FK_Properties_Entities_EntityId1",
                table: "Properties");

            migrationBuilder.DropForeignKey(
                name: "FK_Properties_Entities_EntityId2",
                table: "Properties");

            migrationBuilder.DropTable(
                name: "RoleGroups");

            migrationBuilder.DropTable(
                name: "Tabs");

            migrationBuilder.DropIndex(
                name: "IX_Properties_EntityId1",
                table: "Properties");

            migrationBuilder.DropIndex(
                name: "IX_Properties_EntityId2",
                table: "Properties");

            migrationBuilder.DropIndex(
                name: "IX_Parameters_DataSourceEntityId",
                table: "Parameters");

            migrationBuilder.DropIndex(
                name: "IX_Parameters_DataSourcePropertyId",
                table: "Parameters");

            migrationBuilder.DropIndex(
                name: "IX_Parameters_DataSourcePropertyId2",
                table: "Parameters");

            migrationBuilder.DropIndex(
                name: "IX_Parameters_DataSourcePropertyId3",
                table: "Parameters");

            migrationBuilder.DropIndex(
                name: "IX_Forms_EntityId",
                table: "Forms");

            migrationBuilder.DropIndex(
                name: "IX_Fields_DataTableId",
                table: "Fields");

            migrationBuilder.DropIndex(
                name: "IX_Fields_TabId",
                table: "Fields");

            migrationBuilder.DropIndex(
                name: "IX_DataTables_EntityId",
                table: "DataTables");

            migrationBuilder.DropIndex(
                name: "IX_AspNetRoles_RoleGroupId",
                table: "AspNetRoles");

            migrationBuilder.DropColumn(
                name: "DataSourcePropertyId2",
                table: "Properties");

            migrationBuilder.DropColumn(
                name: "DataSourcePropertyId3",
                table: "Properties");

            migrationBuilder.DropColumn(
                name: "EntityId1",
                table: "Properties");

            migrationBuilder.DropColumn(
                name: "EntityId2",
                table: "Properties");

            migrationBuilder.DropColumn(
                name: "DataSourceEntityId",
                table: "Parameters");

            migrationBuilder.DropColumn(
                name: "DataSourcePropertyId",
                table: "Parameters");

            migrationBuilder.DropColumn(
                name: "DataSourcePropertyId2",
                table: "Parameters");

            migrationBuilder.DropColumn(
                name: "DataSourcePropertyId3",
                table: "Parameters");

            migrationBuilder.DropColumn(
                name: "OptionLabel",
                table: "Parameters");

            migrationBuilder.DropColumn(
                name: "ParameterType",
                table: "Parameters");

            migrationBuilder.DropColumn(
                name: "PresetValues",
                table: "Parameters");

            migrationBuilder.DropColumn(
                name: "Scripts",
                table: "Pages");

            migrationBuilder.DropColumn(
                name: "EntityId",
                table: "Forms");

            migrationBuilder.DropColumn(
                name: "Scripts",
                table: "Forms");

            migrationBuilder.DropColumn(
                name: "DataTableId",
                table: "Fields");

            migrationBuilder.DropColumn(
                name: "EditableToRoles",
                table: "Fields");

            migrationBuilder.DropColumn(
                name: "FieldSet",
                table: "Fields");

            migrationBuilder.DropColumn(
                name: "InvisibleToRoles",
                table: "Fields");

            migrationBuilder.DropColumn(
                name: "IsReadOnly",
                table: "Fields");

            migrationBuilder.DropColumn(
                name: "IsVisible",
                table: "Fields");

            migrationBuilder.DropColumn(
                name: "ReadOnlyToRoles",
                table: "Fields");

            migrationBuilder.DropColumn(
                name: "TabId",
                table: "Fields");

            migrationBuilder.DropColumn(
                name: "VisibleToRoles",
                table: "Fields");

            migrationBuilder.DropColumn(
                name: "EntityId",
                table: "DataTables");

            migrationBuilder.DropColumn(
                name: "CssClass",
                table: "Columns");

            migrationBuilder.DropColumn(
                name: "IsVisible",
                table: "Columns");

            migrationBuilder.DropColumn(
                name: "AllowAll",
                table: "AspNetRoles");

            migrationBuilder.DropColumn(
                name: "AllowCreate",
                table: "AspNetRoles");

            migrationBuilder.DropColumn(
                name: "AllowDelete",
                table: "AspNetRoles");

            migrationBuilder.DropColumn(
                name: "AllowRead",
                table: "AspNetRoles");

            migrationBuilder.DropColumn(
                name: "AllowSpecial",
                table: "AspNetRoles");

            migrationBuilder.DropColumn(
                name: "AllowUpdate",
                table: "AspNetRoles");

            migrationBuilder.DropColumn(
                name: "RoleGroupId",
                table: "AspNetRoles");

            migrationBuilder.AlterColumn<string>(
                name: "DataSourcePropertyId",
                table: "Properties",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PropertyValueId",
                table: "Fields",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Tab",
                table: "Fields",
                maxLength: 200,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OnLoad",
                table: "Entities",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EntityName",
                table: "DataTables",
                maxLength: 200,
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Properties_DataSourcePropertyId",
                table: "Properties",
                column: "DataSourcePropertyId");

            migrationBuilder.CreateIndex(
                name: "IX_Fields_PropertyValueId",
                table: "Fields",
                column: "PropertyValueId");

            migrationBuilder.AddForeignKey(
                name: "FK_Fields_PropertyValues_PropertyValueId",
                table: "Fields",
                column: "PropertyValueId",
                principalTable: "PropertyValues",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Properties_Properties_DataSourcePropertyId",
                table: "Properties",
                column: "DataSourcePropertyId",
                principalTable: "Properties",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
