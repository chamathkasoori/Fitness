using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Fitness.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class MemberSubscriptionSubscriptionPlanDiscountId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SubscriptionPlanDiscountId",
                table: "MemberSubscriptions",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_MemberSubscriptions_SubscriptionPlanDiscountId",
                table: "MemberSubscriptions",
                column: "SubscriptionPlanDiscountId");

            migrationBuilder.AddForeignKey(
                name: "FK_MemberSubscriptions_SubscriptionPlans_SubscriptionPlanDiscountId",
                table: "MemberSubscriptions",
                column: "SubscriptionPlanDiscountId",
                principalTable: "SubscriptionPlans",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MemberSubscriptions_SubscriptionPlans_SubscriptionPlanDiscountId",
                table: "MemberSubscriptions");

            migrationBuilder.DropIndex(
                name: "IX_MemberSubscriptions_SubscriptionPlanDiscountId",
                table: "MemberSubscriptions");

            migrationBuilder.DropColumn(
                name: "SubscriptionPlanDiscountId",
                table: "MemberSubscriptions");
        }
    }
}
