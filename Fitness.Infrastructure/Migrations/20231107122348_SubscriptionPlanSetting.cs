using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Fitness.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class SubscriptionPlanSetting : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SubscriptionPlanSubscriptionPlanSetting_SubscriptionPlanSetting_SubscriptionPlanSettingId",
                table: "SubscriptionPlanSubscriptionPlanSetting");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SubscriptionPlanSetting",
                table: "SubscriptionPlanSetting");

            migrationBuilder.RenameTable(
                name: "SubscriptionPlanSetting",
                newName: "SubscriptionPlanSettings");

            migrationBuilder.AddColumn<string>(
                name: "DropdownValues",
                table: "SubscriptionPlanSettings",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "IsDropdownVisible",
                table: "SubscriptionPlanSettings",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsValueVisible",
                table: "SubscriptionPlanSettings",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddPrimaryKey(
                name: "PK_SubscriptionPlanSettings",
                table: "SubscriptionPlanSettings",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_SubscriptionPlanSubscriptionPlanSetting_SubscriptionPlanSettings_SubscriptionPlanSettingId",
                table: "SubscriptionPlanSubscriptionPlanSetting",
                column: "SubscriptionPlanSettingId",
                principalTable: "SubscriptionPlanSettings",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SubscriptionPlanSubscriptionPlanSetting_SubscriptionPlanSettings_SubscriptionPlanSettingId",
                table: "SubscriptionPlanSubscriptionPlanSetting");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SubscriptionPlanSettings",
                table: "SubscriptionPlanSettings");

            migrationBuilder.DropColumn(
                name: "DropdownValues",
                table: "SubscriptionPlanSettings");

            migrationBuilder.DropColumn(
                name: "IsDropdownVisible",
                table: "SubscriptionPlanSettings");

            migrationBuilder.DropColumn(
                name: "IsValueVisible",
                table: "SubscriptionPlanSettings");

            migrationBuilder.RenameTable(
                name: "SubscriptionPlanSettings",
                newName: "SubscriptionPlanSetting");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SubscriptionPlanSetting",
                table: "SubscriptionPlanSetting",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_SubscriptionPlanSubscriptionPlanSetting_SubscriptionPlanSetting_SubscriptionPlanSettingId",
                table: "SubscriptionPlanSubscriptionPlanSetting",
                column: "SubscriptionPlanSettingId",
                principalTable: "SubscriptionPlanSetting",
                principalColumn: "Id");
        }
    }
}
