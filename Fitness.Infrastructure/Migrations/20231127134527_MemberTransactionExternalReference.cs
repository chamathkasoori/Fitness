using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Fitness.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class MemberTransactionExternalReference : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SubscriptionPlanTransactions_SubscriptionPlans_SubscriptionPlanId",
                table: "SubscriptionPlanTransactions");

            migrationBuilder.DropIndex(
                name: "IX_SubscriptionPlanTransactions_SubscriptionPlanId",
                table: "SubscriptionPlanTransactions");

            migrationBuilder.AlterColumn<int>(
                name: "SubscriptionPlanId",
                table: "SubscriptionPlanTransactions",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "SubscriptionPlanId",
                table: "SubscriptionPlanTransactions",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_SubscriptionPlanTransactions_SubscriptionPlanId",
                table: "SubscriptionPlanTransactions",
                column: "SubscriptionPlanId");

            migrationBuilder.AddForeignKey(
                name: "FK_SubscriptionPlanTransactions_SubscriptionPlans_SubscriptionPlanId",
                table: "SubscriptionPlanTransactions",
                column: "SubscriptionPlanId",
                principalTable: "SubscriptionPlans",
                principalColumn: "Id");
        }
    }
}
