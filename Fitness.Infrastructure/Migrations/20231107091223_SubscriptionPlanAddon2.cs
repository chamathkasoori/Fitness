using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Fitness.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class SubscriptionPlanAddon2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SubscriptionPlanAddons_Vat_GrossFeeVatId",
                table: "SubscriptionPlanAddons");

            migrationBuilder.DropForeignKey(
                name: "FK_SubscriptionPlanAddons_Vat_PenaltyFeeVatId",
                table: "SubscriptionPlanAddons");

            migrationBuilder.DropIndex(
                name: "IX_SubscriptionPlanAddons_GrossFeeVatId",
                table: "SubscriptionPlanAddons");

            migrationBuilder.DropIndex(
                name: "IX_SubscriptionPlanAddons_PenaltyFeeVatId",
                table: "SubscriptionPlanAddons");

            migrationBuilder.DropColumn(
                name: "AddAllProducts",
                table: "SubscriptionPlanAddons");

            migrationBuilder.DropColumn(
                name: "ClubServiceTypeId",
                table: "SubscriptionPlanAddons");

            migrationBuilder.DropColumn(
                name: "EndOnLastDayOfMonth",
                table: "SubscriptionPlanAddons");

            migrationBuilder.DropColumn(
                name: "GrossFee",
                table: "SubscriptionPlanAddons");

            migrationBuilder.DropColumn(
                name: "GrossFeeVatId",
                table: "SubscriptionPlanAddons");

            migrationBuilder.DropColumn(
                name: "NumberOfPeriodsForAddProducts",
                table: "SubscriptionPlanAddons");

            migrationBuilder.DropColumn(
                name: "PenaltyFee",
                table: "SubscriptionPlanAddons");

            migrationBuilder.DropColumn(
                name: "PenaltyFeeVatId",
                table: "SubscriptionPlanAddons");

            migrationBuilder.DropColumn(
                name: "PenaltyLimit",
                table: "SubscriptionPlanAddons");

            migrationBuilder.RenameColumn(
                name: "StartOnFirstDayOfMonth",
                table: "SubscriptionPlanAddons",
                newName: "AddToAllClubs");

            migrationBuilder.AlterColumn<int>(
                name: "Quantity",
                table: "SubscriptionPlanAddons",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Type",
                table: "SubscriptionPlanAddons",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Type",
                table: "SubscriptionPlanAddons");

            migrationBuilder.RenameColumn(
                name: "AddToAllClubs",
                table: "SubscriptionPlanAddons",
                newName: "StartOnFirstDayOfMonth");

            migrationBuilder.AlterColumn<int>(
                name: "Quantity",
                table: "SubscriptionPlanAddons",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<bool>(
                name: "AddAllProducts",
                table: "SubscriptionPlanAddons",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "ClubServiceTypeId",
                table: "SubscriptionPlanAddons",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "EndOnLastDayOfMonth",
                table: "SubscriptionPlanAddons",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<decimal>(
                name: "GrossFee",
                table: "SubscriptionPlanAddons",
                type: "decimal(18,2)",
                precision: 18,
                scale: 2,
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<int>(
                name: "GrossFeeVatId",
                table: "SubscriptionPlanAddons",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "NumberOfPeriodsForAddProducts",
                table: "SubscriptionPlanAddons",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "PenaltyFee",
                table: "SubscriptionPlanAddons",
                type: "decimal(18,2)",
                precision: 18,
                scale: 2,
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<int>(
                name: "PenaltyFeeVatId",
                table: "SubscriptionPlanAddons",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PenaltyLimit",
                table: "SubscriptionPlanAddons",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_SubscriptionPlanAddons_GrossFeeVatId",
                table: "SubscriptionPlanAddons",
                column: "GrossFeeVatId");

            migrationBuilder.CreateIndex(
                name: "IX_SubscriptionPlanAddons_PenaltyFeeVatId",
                table: "SubscriptionPlanAddons",
                column: "PenaltyFeeVatId");

            migrationBuilder.AddForeignKey(
                name: "FK_SubscriptionPlanAddons_Vat_GrossFeeVatId",
                table: "SubscriptionPlanAddons",
                column: "GrossFeeVatId",
                principalTable: "Vat",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_SubscriptionPlanAddons_Vat_PenaltyFeeVatId",
                table: "SubscriptionPlanAddons",
                column: "PenaltyFeeVatId",
                principalTable: "Vat",
                principalColumn: "Id");
        }
    }
}
