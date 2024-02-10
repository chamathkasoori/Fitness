using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Fitness.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class MemberSubscriptionAddon2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MemberSubscriptionAddon_MemberSubscriptions_MemberSubscriptionId",
                table: "MemberSubscriptionAddon");

            migrationBuilder.DropForeignKey(
                name: "FK_MemberSubscriptionAddon_SubscriptionPlanAddons_SubscriptionPlanAddonId",
                table: "MemberSubscriptionAddon");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MemberSubscriptionAddon",
                table: "MemberSubscriptionAddon");

            migrationBuilder.RenameTable(
                name: "MemberSubscriptionAddon",
                newName: "MemberSubscriptionAddons");

            migrationBuilder.RenameIndex(
                name: "IX_MemberSubscriptionAddon_SubscriptionPlanAddonId",
                table: "MemberSubscriptionAddons",
                newName: "IX_MemberSubscriptionAddons_SubscriptionPlanAddonId");

            migrationBuilder.RenameIndex(
                name: "IX_MemberSubscriptionAddon_MemberSubscriptionId",
                table: "MemberSubscriptionAddons",
                newName: "IX_MemberSubscriptionAddons_MemberSubscriptionId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MemberSubscriptionAddons",
                table: "MemberSubscriptionAddons",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_MemberSubscriptionAddons_MemberSubscriptions_MemberSubscriptionId",
                table: "MemberSubscriptionAddons",
                column: "MemberSubscriptionId",
                principalTable: "MemberSubscriptions",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_MemberSubscriptionAddons_SubscriptionPlanAddons_SubscriptionPlanAddonId",
                table: "MemberSubscriptionAddons",
                column: "SubscriptionPlanAddonId",
                principalTable: "SubscriptionPlanAddons",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MemberSubscriptionAddons_MemberSubscriptions_MemberSubscriptionId",
                table: "MemberSubscriptionAddons");

            migrationBuilder.DropForeignKey(
                name: "FK_MemberSubscriptionAddons_SubscriptionPlanAddons_SubscriptionPlanAddonId",
                table: "MemberSubscriptionAddons");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MemberSubscriptionAddons",
                table: "MemberSubscriptionAddons");

            migrationBuilder.RenameTable(
                name: "MemberSubscriptionAddons",
                newName: "MemberSubscriptionAddon");

            migrationBuilder.RenameIndex(
                name: "IX_MemberSubscriptionAddons_SubscriptionPlanAddonId",
                table: "MemberSubscriptionAddon",
                newName: "IX_MemberSubscriptionAddon_SubscriptionPlanAddonId");

            migrationBuilder.RenameIndex(
                name: "IX_MemberSubscriptionAddons_MemberSubscriptionId",
                table: "MemberSubscriptionAddon",
                newName: "IX_MemberSubscriptionAddon_MemberSubscriptionId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MemberSubscriptionAddon",
                table: "MemberSubscriptionAddon",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_MemberSubscriptionAddon_MemberSubscriptions_MemberSubscriptionId",
                table: "MemberSubscriptionAddon",
                column: "MemberSubscriptionId",
                principalTable: "MemberSubscriptions",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_MemberSubscriptionAddon_SubscriptionPlanAddons_SubscriptionPlanAddonId",
                table: "MemberSubscriptionAddon",
                column: "SubscriptionPlanAddonId",
                principalTable: "SubscriptionPlanAddons",
                principalColumn: "Id");
        }
    }
}
