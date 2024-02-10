using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Fitness.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class MemberSubscriptionDiscount2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MemberSubscriptionDiscount_MemberSubscriptions_MemberSubscriptionId",
                table: "MemberSubscriptionDiscount");

            migrationBuilder.DropForeignKey(
                name: "FK_MemberSubscriptionDiscount_SubscriptionPlanDiscounts_SubscriptionPlanDiscountId",
                table: "MemberSubscriptionDiscount");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MemberSubscriptionDiscount",
                table: "MemberSubscriptionDiscount");

            migrationBuilder.RenameTable(
                name: "MemberSubscriptionDiscount",
                newName: "MemberSubscriptionDiscounts");

            migrationBuilder.RenameIndex(
                name: "IX_MemberSubscriptionDiscount_SubscriptionPlanDiscountId",
                table: "MemberSubscriptionDiscounts",
                newName: "IX_MemberSubscriptionDiscounts_SubscriptionPlanDiscountId");

            migrationBuilder.RenameIndex(
                name: "IX_MemberSubscriptionDiscount_MemberSubscriptionId",
                table: "MemberSubscriptionDiscounts",
                newName: "IX_MemberSubscriptionDiscounts_MemberSubscriptionId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MemberSubscriptionDiscounts",
                table: "MemberSubscriptionDiscounts",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_MemberSubscriptionDiscounts_MemberSubscriptions_MemberSubscriptionId",
                table: "MemberSubscriptionDiscounts",
                column: "MemberSubscriptionId",
                principalTable: "MemberSubscriptions",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_MemberSubscriptionDiscounts_SubscriptionPlanDiscounts_SubscriptionPlanDiscountId",
                table: "MemberSubscriptionDiscounts",
                column: "SubscriptionPlanDiscountId",
                principalTable: "SubscriptionPlanDiscounts",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MemberSubscriptionDiscounts_MemberSubscriptions_MemberSubscriptionId",
                table: "MemberSubscriptionDiscounts");

            migrationBuilder.DropForeignKey(
                name: "FK_MemberSubscriptionDiscounts_SubscriptionPlanDiscounts_SubscriptionPlanDiscountId",
                table: "MemberSubscriptionDiscounts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MemberSubscriptionDiscounts",
                table: "MemberSubscriptionDiscounts");

            migrationBuilder.RenameTable(
                name: "MemberSubscriptionDiscounts",
                newName: "MemberSubscriptionDiscount");

            migrationBuilder.RenameIndex(
                name: "IX_MemberSubscriptionDiscounts_SubscriptionPlanDiscountId",
                table: "MemberSubscriptionDiscount",
                newName: "IX_MemberSubscriptionDiscount_SubscriptionPlanDiscountId");

            migrationBuilder.RenameIndex(
                name: "IX_MemberSubscriptionDiscounts_MemberSubscriptionId",
                table: "MemberSubscriptionDiscount",
                newName: "IX_MemberSubscriptionDiscount_MemberSubscriptionId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MemberSubscriptionDiscount",
                table: "MemberSubscriptionDiscount",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_MemberSubscriptionDiscount_MemberSubscriptions_MemberSubscriptionId",
                table: "MemberSubscriptionDiscount",
                column: "MemberSubscriptionId",
                principalTable: "MemberSubscriptions",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_MemberSubscriptionDiscount_SubscriptionPlanDiscounts_SubscriptionPlanDiscountId",
                table: "MemberSubscriptionDiscount",
                column: "SubscriptionPlanDiscountId",
                principalTable: "SubscriptionPlanDiscounts",
                principalColumn: "Id");
        }
    }
}
