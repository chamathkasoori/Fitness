using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Fitness.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class SubscriptionPlanSubscriptionPlanSettingIsChecked : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DropdownValue",
                table: "SubscriptionPlanSubscriptionPlanSetting",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "IsChecked",
                table: "SubscriptionPlanSubscriptionPlanSetting",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DropdownValue",
                table: "SubscriptionPlanSubscriptionPlanSetting");

            migrationBuilder.DropColumn(
                name: "IsChecked",
                table: "SubscriptionPlanSubscriptionPlanSetting");
        }
    }
}
