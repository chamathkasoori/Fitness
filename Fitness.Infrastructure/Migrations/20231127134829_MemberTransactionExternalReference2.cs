using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Fitness.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class MemberTransactionExternalReference2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SubscriptionPlanTransactions_Members_MemberId",
                table: "SubscriptionPlanTransactions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SubscriptionPlanTransactions",
                table: "SubscriptionPlanTransactions");

            migrationBuilder.RenameTable(
                name: "SubscriptionPlanTransactions",
                newName: "MemberTransactionExternalReferences");

            migrationBuilder.RenameIndex(
                name: "IX_SubscriptionPlanTransactions_MemberId",
                table: "MemberTransactionExternalReferences",
                newName: "IX_MemberTransactionExternalReferences_MemberId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MemberTransactionExternalReferences",
                table: "MemberTransactionExternalReferences",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_MemberTransactionExternalReferences_Members_MemberId",
                table: "MemberTransactionExternalReferences",
                column: "MemberId",
                principalTable: "Members",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MemberTransactionExternalReferences_Members_MemberId",
                table: "MemberTransactionExternalReferences");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MemberTransactionExternalReferences",
                table: "MemberTransactionExternalReferences");

            migrationBuilder.RenameTable(
                name: "MemberTransactionExternalReferences",
                newName: "SubscriptionPlanTransactions");

            migrationBuilder.RenameIndex(
                name: "IX_MemberTransactionExternalReferences_MemberId",
                table: "SubscriptionPlanTransactions",
                newName: "IX_SubscriptionPlanTransactions_MemberId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SubscriptionPlanTransactions",
                table: "SubscriptionPlanTransactions",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_SubscriptionPlanTransactions_Members_MemberId",
                table: "SubscriptionPlanTransactions",
                column: "MemberId",
                principalTable: "Members",
                principalColumn: "Id");
        }
    }
}
