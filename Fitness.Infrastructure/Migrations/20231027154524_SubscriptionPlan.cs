using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Fitness.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class SubscriptionPlan : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SubscriptionPlanPaymentMethods_PaymentMethod_PaymentMethodId",
                table: "SubscriptionPlanPaymentMethods");

            migrationBuilder.DropForeignKey(
                name: "FK_SubscriptionPlans_PaymentPlans_PaymentPlanId",
                table: "SubscriptionPlans");

            migrationBuilder.DropForeignKey(
                name: "FK_SubscriptionPlans_Vat_AdminFeeVatId",
                table: "SubscriptionPlans");

            migrationBuilder.DropForeignKey(
                name: "FK_SubscriptionPlans_Vat_FreezeAvailableVatId",
                table: "SubscriptionPlans");

            migrationBuilder.DropTable(
                name: "AssignedSubscriptionPlanClubs");

            migrationBuilder.DropIndex(
                name: "IX_SubscriptionPlans_AdminFeeVatId",
                table: "SubscriptionPlans");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PaymentMethod",
                table: "PaymentMethod");

            migrationBuilder.DropColumn(
                name: "AdminFeeVatId",
                table: "SubscriptionPlans");

            migrationBuilder.DropColumn(
                name: "Fees",
                table: "SubscriptionPlans");

            migrationBuilder.RenameTable(
                name: "PaymentMethod",
                newName: "PaymentMethods");

            migrationBuilder.RenameColumn(
                name: "PaymentPlanId",
                table: "SubscriptionPlans",
                newName: "FreezeFeeVatId");

            migrationBuilder.RenameColumn(
                name: "PaymentInterval",
                table: "SubscriptionPlans",
                newName: "PaymentIntervalId");

            migrationBuilder.RenameColumn(
                name: "FreezeAvailableVatId",
                table: "SubscriptionPlans",
                newName: "AdministrationFeeVatId");

            migrationBuilder.RenameColumn(
                name: "CommitmentPeriod",
                table: "SubscriptionPlans",
                newName: "FeeType");

            migrationBuilder.RenameIndex(
                name: "IX_SubscriptionPlans_PaymentPlanId",
                table: "SubscriptionPlans",
                newName: "IX_SubscriptionPlans_FreezeFeeVatId");

            migrationBuilder.RenameIndex(
                name: "IX_SubscriptionPlans_FreezeAvailableVatId",
                table: "SubscriptionPlans",
                newName: "IX_SubscriptionPlans_AdministrationFeeVatId");

            migrationBuilder.AlterColumn<DateTime>(
                name: "StartDate",
                table: "SubscriptionPlans",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<bool>(
                name: "FreezeAvailable",
                table: "SubscriptionPlans",
                type: "bit",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldPrecision: 18,
                oldScale: 2,
                oldNullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "AddToNewClubs",
                table: "SubscriptionPlans",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "CommitmentPeriodId",
                table: "SubscriptionPlans",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<decimal>(
                name: "FreezeFee",
                table: "SubscriptionPlans",
                type: "decimal(18,2)",
                precision: 18,
                scale: 2,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CreatedBy",
                table: "SubscriptionPlanPaymentMethods",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedOn",
                table: "SubscriptionPlanPaymentMethods",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "DeletedBy",
                table: "SubscriptionPlanPaymentMethods",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedOn",
                table: "SubscriptionPlanPaymentMethods",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "SubscriptionPlanPaymentMethods",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "SubscriptionPlanPaymentMethods",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDelete",
                table: "SubscriptionPlanPaymentMethods",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "ModifiedBy",
                table: "SubscriptionPlanPaymentMethods",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedOn",
                table: "SubscriptionPlanPaymentMethods",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_PaymentMethods",
                table: "PaymentMethods",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "SubscriptionPlanAssignedClubs",
                columns: table => new
                {
                    SubscriptionPlanId = table.Column<int>(type: "int", nullable: false),
                    ClubId = table.Column<int>(type: "int", nullable: false),
                    Id = table.Column<int>(type: "int", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: true),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedBy = table.Column<int>(type: "int", nullable: true),
                    DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedBy = table.Column<int>(type: "int", nullable: true),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubscriptionPlanAssignedClubs", x => new { x.SubscriptionPlanId, x.ClubId });
                    table.ForeignKey(
                        name: "FK_SubscriptionPlanAssignedClubs_Clubs_ClubId",
                        column: x => x.ClubId,
                        principalTable: "Clubs",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_SubscriptionPlanAssignedClubs_SubscriptionPlans_SubscriptionPlanId",
                        column: x => x.SubscriptionPlanId,
                        principalTable: "SubscriptionPlans",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_SubscriptionPlans_CommitmentPeriodId",
                table: "SubscriptionPlans",
                column: "CommitmentPeriodId");

            migrationBuilder.CreateIndex(
                name: "IX_SubscriptionPlans_PaymentIntervalId",
                table: "SubscriptionPlans",
                column: "PaymentIntervalId");

            migrationBuilder.CreateIndex(
                name: "IX_SubscriptionPlanAssignedClubs_ClubId",
                table: "SubscriptionPlanAssignedClubs",
                column: "ClubId");

            migrationBuilder.AddForeignKey(
                name: "FK_SubscriptionPlanPaymentMethods_PaymentMethods_PaymentMethodId",
                table: "SubscriptionPlanPaymentMethods",
                column: "PaymentMethodId",
                principalTable: "PaymentMethods",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_SubscriptionPlans_PaymentIntervals_CommitmentPeriodId",
                table: "SubscriptionPlans",
                column: "CommitmentPeriodId",
                principalTable: "PaymentIntervals",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_SubscriptionPlans_PaymentIntervals_PaymentIntervalId",
                table: "SubscriptionPlans",
                column: "PaymentIntervalId",
                principalTable: "PaymentIntervals",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_SubscriptionPlans_Vat_AdministrationFeeVatId",
                table: "SubscriptionPlans",
                column: "AdministrationFeeVatId",
                principalTable: "Vat",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_SubscriptionPlans_Vat_FreezeFeeVatId",
                table: "SubscriptionPlans",
                column: "FreezeFeeVatId",
                principalTable: "Vat",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SubscriptionPlanPaymentMethods_PaymentMethods_PaymentMethodId",
                table: "SubscriptionPlanPaymentMethods");

            migrationBuilder.DropForeignKey(
                name: "FK_SubscriptionPlans_PaymentIntervals_CommitmentPeriodId",
                table: "SubscriptionPlans");

            migrationBuilder.DropForeignKey(
                name: "FK_SubscriptionPlans_PaymentIntervals_PaymentIntervalId",
                table: "SubscriptionPlans");

            migrationBuilder.DropForeignKey(
                name: "FK_SubscriptionPlans_Vat_AdministrationFeeVatId",
                table: "SubscriptionPlans");

            migrationBuilder.DropForeignKey(
                name: "FK_SubscriptionPlans_Vat_FreezeFeeVatId",
                table: "SubscriptionPlans");

            migrationBuilder.DropTable(
                name: "SubscriptionPlanAssignedClubs");

            migrationBuilder.DropIndex(
                name: "IX_SubscriptionPlans_CommitmentPeriodId",
                table: "SubscriptionPlans");

            migrationBuilder.DropIndex(
                name: "IX_SubscriptionPlans_PaymentIntervalId",
                table: "SubscriptionPlans");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PaymentMethods",
                table: "PaymentMethods");

            migrationBuilder.DropColumn(
                name: "AddToNewClubs",
                table: "SubscriptionPlans");

            migrationBuilder.DropColumn(
                name: "CommitmentPeriodId",
                table: "SubscriptionPlans");

            migrationBuilder.DropColumn(
                name: "FreezeFee",
                table: "SubscriptionPlans");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "SubscriptionPlanPaymentMethods");

            migrationBuilder.DropColumn(
                name: "CreatedOn",
                table: "SubscriptionPlanPaymentMethods");

            migrationBuilder.DropColumn(
                name: "DeletedBy",
                table: "SubscriptionPlanPaymentMethods");

            migrationBuilder.DropColumn(
                name: "DeletedOn",
                table: "SubscriptionPlanPaymentMethods");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "SubscriptionPlanPaymentMethods");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "SubscriptionPlanPaymentMethods");

            migrationBuilder.DropColumn(
                name: "IsDelete",
                table: "SubscriptionPlanPaymentMethods");

            migrationBuilder.DropColumn(
                name: "ModifiedBy",
                table: "SubscriptionPlanPaymentMethods");

            migrationBuilder.DropColumn(
                name: "ModifiedOn",
                table: "SubscriptionPlanPaymentMethods");

            migrationBuilder.RenameTable(
                name: "PaymentMethods",
                newName: "PaymentMethod");

            migrationBuilder.RenameColumn(
                name: "PaymentIntervalId",
                table: "SubscriptionPlans",
                newName: "PaymentInterval");

            migrationBuilder.RenameColumn(
                name: "FreezeFeeVatId",
                table: "SubscriptionPlans",
                newName: "PaymentPlanId");

            migrationBuilder.RenameColumn(
                name: "FeeType",
                table: "SubscriptionPlans",
                newName: "CommitmentPeriod");

            migrationBuilder.RenameColumn(
                name: "AdministrationFeeVatId",
                table: "SubscriptionPlans",
                newName: "FreezeAvailableVatId");

            migrationBuilder.RenameIndex(
                name: "IX_SubscriptionPlans_FreezeFeeVatId",
                table: "SubscriptionPlans",
                newName: "IX_SubscriptionPlans_PaymentPlanId");

            migrationBuilder.RenameIndex(
                name: "IX_SubscriptionPlans_AdministrationFeeVatId",
                table: "SubscriptionPlans",
                newName: "IX_SubscriptionPlans_FreezeAvailableVatId");

            migrationBuilder.AlterColumn<DateTime>(
                name: "StartDate",
                table: "SubscriptionPlans",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "FreezeAvailable",
                table: "SubscriptionPlans",
                type: "decimal(18,2)",
                precision: 18,
                scale: 2,
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "AdminFeeVatId",
                table: "SubscriptionPlans",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Fees",
                table: "SubscriptionPlans",
                type: "int",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_PaymentMethod",
                table: "PaymentMethod",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "AssignedSubscriptionPlanClubs",
                columns: table => new
                {
                    SubscriptionPlanId = table.Column<int>(type: "int", nullable: false),
                    ClubId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AssignedSubscriptionPlanClubs", x => new { x.SubscriptionPlanId, x.ClubId });
                    table.ForeignKey(
                        name: "FK_AssignedSubscriptionPlanClubs_Clubs_ClubId",
                        column: x => x.ClubId,
                        principalTable: "Clubs",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AssignedSubscriptionPlanClubs_SubscriptionPlans_SubscriptionPlanId",
                        column: x => x.SubscriptionPlanId,
                        principalTable: "SubscriptionPlans",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_SubscriptionPlans_AdminFeeVatId",
                table: "SubscriptionPlans",
                column: "AdminFeeVatId");

            migrationBuilder.CreateIndex(
                name: "IX_AssignedSubscriptionPlanClubs_ClubId",
                table: "AssignedSubscriptionPlanClubs",
                column: "ClubId");

            migrationBuilder.AddForeignKey(
                name: "FK_SubscriptionPlanPaymentMethods_PaymentMethod_PaymentMethodId",
                table: "SubscriptionPlanPaymentMethods",
                column: "PaymentMethodId",
                principalTable: "PaymentMethod",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_SubscriptionPlans_PaymentPlans_PaymentPlanId",
                table: "SubscriptionPlans",
                column: "PaymentPlanId",
                principalTable: "PaymentPlans",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_SubscriptionPlans_Vat_AdminFeeVatId",
                table: "SubscriptionPlans",
                column: "AdminFeeVatId",
                principalTable: "Vat",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_SubscriptionPlans_Vat_FreezeAvailableVatId",
                table: "SubscriptionPlans",
                column: "FreezeAvailableVatId",
                principalTable: "Vat",
                principalColumn: "Id");
        }
    }
}
