using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Fitness.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class SubscriptionPlanAccessRuleId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AccessRuleId",
                table: "SubscriptionPlans",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_SubscriptionPlans_AccessRuleId",
                table: "SubscriptionPlans",
                column: "AccessRuleId");

            migrationBuilder.AddForeignKey(
                name: "FK_SubscriptionPlans_AccessRules_AccessRuleId",
                table: "SubscriptionPlans",
                column: "AccessRuleId",
                principalTable: "AccessRules",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SubscriptionPlans_AccessRules_AccessRuleId",
                table: "SubscriptionPlans");

            migrationBuilder.DropIndex(
                name: "IX_SubscriptionPlans_AccessRuleId",
                table: "SubscriptionPlans");

            migrationBuilder.DropColumn(
                name: "AccessRuleId",
                table: "SubscriptionPlans");
        }
    }
}
