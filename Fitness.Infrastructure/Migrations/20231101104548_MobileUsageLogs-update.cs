using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Fitness.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class MobileUsageLogsupdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CreatedBy",
                table: "MobileUsageLogs",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedOn",
                table: "MobileUsageLogs",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "DeletedBy",
                table: "MobileUsageLogs",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedOn",
                table: "MobileUsageLogs",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "MobileUsageLogs",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDelete",
                table: "MobileUsageLogs",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "ModifiedBy",
                table: "MobileUsageLogs",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedOn",
                table: "MobileUsageLogs",
                type: "datetime2",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "MobileUsageLogs");

            migrationBuilder.DropColumn(
                name: "CreatedOn",
                table: "MobileUsageLogs");

            migrationBuilder.DropColumn(
                name: "DeletedBy",
                table: "MobileUsageLogs");

            migrationBuilder.DropColumn(
                name: "DeletedOn",
                table: "MobileUsageLogs");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "MobileUsageLogs");

            migrationBuilder.DropColumn(
                name: "IsDelete",
                table: "MobileUsageLogs");

            migrationBuilder.DropColumn(
                name: "ModifiedBy",
                table: "MobileUsageLogs");

            migrationBuilder.DropColumn(
                name: "ModifiedOn",
                table: "MobileUsageLogs");
        }
    }
}
