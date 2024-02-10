using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Fitness.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class SubscriptionPlanIsFreezeAvailable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SubscriptionPlans_Vat_FreezeFeeVatId",
                table: "SubscriptionPlans");

            migrationBuilder.DropIndex(
                name: "IX_SubscriptionPlans_FreezeFeeVatId",
                table: "SubscriptionPlans");

            migrationBuilder.DropColumn(
                name: "FreezeAvailable",
                table: "SubscriptionPlans");

            migrationBuilder.DropColumn(
                name: "FreezeFee",
                table: "SubscriptionPlans");

            migrationBuilder.DropColumn(
                name: "FreezeFeeVatId",
                table: "SubscriptionPlans");

            migrationBuilder.AddColumn<bool>(
                name: "IsFreezeAvailable",
                table: "SubscriptionPlans",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsFreezeAvailable",
                table: "SubscriptionPlans");

            migrationBuilder.AddColumn<bool>(
                name: "FreezeAvailable",
                table: "SubscriptionPlans",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "FreezeFee",
                table: "SubscriptionPlans",
                type: "decimal(18,2)",
                precision: 18,
                scale: 2,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "FreezeFeeVatId",
                table: "SubscriptionPlans",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_SubscriptionPlans_FreezeFeeVatId",
                table: "SubscriptionPlans",
                column: "FreezeFeeVatId");

            migrationBuilder.AddForeignKey(
                name: "FK_SubscriptionPlans_Vat_FreezeFeeVatId",
                table: "SubscriptionPlans",
                column: "FreezeFeeVatId",
                principalTable: "Vat",
                principalColumn: "Id");
        }
    }
}
