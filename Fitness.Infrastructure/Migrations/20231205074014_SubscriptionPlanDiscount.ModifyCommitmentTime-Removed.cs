using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Fitness.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class SubscriptionPlanDiscountModifyCommitmentTimeRemoved : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ModifyCommitmentTime",
                table: "SubscriptionPlanDiscounts");

            migrationBuilder.DropColumn(
                name: "ModifyCommitmentTimeType",
                table: "SubscriptionPlanDiscounts");

            migrationBuilder.DropColumn(
                name: "ModifyCommitmentTimeValue",
                table: "SubscriptionPlanDiscounts");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "ModifyCommitmentTime",
                table: "SubscriptionPlanDiscounts",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "ModifyCommitmentTimeType",
                table: "SubscriptionPlanDiscounts",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ModifyCommitmentTimeValue",
                table: "SubscriptionPlanDiscounts",
                type: "int",
                nullable: true);
        }
    }
}
