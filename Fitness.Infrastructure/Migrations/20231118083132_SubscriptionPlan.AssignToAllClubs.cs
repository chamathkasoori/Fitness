using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Fitness.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class SubscriptionPlanAssignToAllClubs : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Invoices_MemberSubscriptions_SubscriptionId",
                table: "Invoices");

            migrationBuilder.RenameColumn(
                name: "SubscriptionId",
                table: "Invoices",
                newName: "MemberSubscriptionId");

            migrationBuilder.RenameIndex(
                name: "IX_Invoices_SubscriptionId",
                table: "Invoices",
                newName: "IX_Invoices_MemberSubscriptionId");

            migrationBuilder.AddColumn<bool>(
                name: "AssignToAllClubs",
                table: "SubscriptionPlans",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddForeignKey(
                name: "FK_Invoices_MemberSubscriptions_MemberSubscriptionId",
                table: "Invoices",
                column: "MemberSubscriptionId",
                principalTable: "MemberSubscriptions",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Invoices_MemberSubscriptions_MemberSubscriptionId",
                table: "Invoices");

            migrationBuilder.DropColumn(
                name: "AssignToAllClubs",
                table: "SubscriptionPlans");

            migrationBuilder.RenameColumn(
                name: "MemberSubscriptionId",
                table: "Invoices",
                newName: "SubscriptionId");

            migrationBuilder.RenameIndex(
                name: "IX_Invoices_MemberSubscriptionId",
                table: "Invoices",
                newName: "IX_Invoices_SubscriptionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Invoices_MemberSubscriptions_SubscriptionId",
                table: "Invoices",
                column: "SubscriptionId",
                principalTable: "MemberSubscriptions",
                principalColumn: "Id");
        }
    }
}
