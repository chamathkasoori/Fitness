using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Fitness.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class SubscriptionPlanDiscount3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MembershipFeeDiscountAfter",
                table: "SubscriptionPlanDiscounts");

            migrationBuilder.RenameColumn(
                name: "MembershipFeeDiscountAmount",
                table: "SubscriptionPlanDiscounts",
                newName: "MembershipFeeDiscountValue");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "MembershipFeeDiscountValue",
                table: "SubscriptionPlanDiscounts",
                newName: "MembershipFeeDiscountAmount");

            migrationBuilder.AddColumn<int>(
                name: "MembershipFeeDiscountAfter",
                table: "SubscriptionPlanDiscounts",
                type: "int",
                nullable: true);
        }
    }
}
