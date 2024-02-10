using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Fitness.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddSubscriptionPlanTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SubscriptionPlans_ContractSetting_ContractSettingsId",
                table: "SubscriptionPlans");

            migrationBuilder.DropForeignKey(
                name: "FK_SubscriptionPlans_PaymentPlan_PaymentPlanId",
                table: "SubscriptionPlans");

            migrationBuilder.DropTable(
                name: "ContractSetting");

            migrationBuilder.DropTable(
                name: "SubscriptionPlanClub");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PaymentPlan",
                table: "PaymentPlan");

            migrationBuilder.DropColumn(
                name: "Category",
                table: "SubscriptionPlans");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "SubscriptionPlans");

            migrationBuilder.DropColumn(
                name: "TransferToClubOnContractStartDate",
                table: "SubscriptionPlans");

            migrationBuilder.DropColumn(
                name: "Visibility",
                table: "SubscriptionPlans");

            migrationBuilder.DropColumn(
                name: "AdministrationPer",
                table: "PaymentPlan");

            migrationBuilder.DropColumn(
                name: "FreezeAvailablePer",
                table: "PaymentPlan");

            migrationBuilder.DropColumn(
                name: "JoiningFeePer",
                table: "PaymentPlan");

            migrationBuilder.DropColumn(
                name: "MembershipFeePer",
                table: "PaymentPlan");

            migrationBuilder.DropColumn(
                name: "PenaltyforUnPaidInstallmentPer",
                table: "PaymentPlan");

            migrationBuilder.RenameTable(
                name: "PaymentPlan",
                newName: "PaymentPlans");

            migrationBuilder.RenameColumn(
                name: "ContractSettingsId",
                table: "SubscriptionPlans",
                newName: "PenaltyforUnPaidInstallmentVatId");

            migrationBuilder.RenameIndex(
                name: "IX_SubscriptionPlans_ContractSettingsId",
                table: "SubscriptionPlans",
                newName: "IX_SubscriptionPlans_PenaltyforUnPaidInstallmentVatId");

            migrationBuilder.RenameColumn(
                name: "PaymentPlanId",
                table: "PaymentPlans",
                newName: "Id");

            migrationBuilder.AlterColumn<int>(
                name: "PlanType",
                table: "SubscriptionPlans",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "AdminFeeVatId",
                table: "SubscriptionPlans",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "AdministrationFee",
                table: "SubscriptionPlans",
                type: "decimal(18,2)",
                precision: 18,
                scale: 2,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CommitmentPeriod",
                table: "SubscriptionPlans",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Fees",
                table: "SubscriptionPlans",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "FreezeAvailable",
                table: "SubscriptionPlans",
                type: "decimal(18,2)",
                precision: 18,
                scale: 2,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "FreezeAvailableVatId",
                table: "SubscriptionPlans",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "JoiningFee",
                table: "SubscriptionPlans",
                type: "decimal(18,2)",
                precision: 18,
                scale: 2,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "JoiningFeeVatId",
                table: "SubscriptionPlans",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MaximumCancellationDays",
                table: "SubscriptionPlans",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "MaximumCancellationMonths",
                table: "SubscriptionPlans",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<decimal>(
                name: "MembershipFee",
                table: "SubscriptionPlans",
                type: "decimal(18,2)",
                precision: 18,
                scale: 2,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MembershipFeeVatId",
                table: "SubscriptionPlans",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MinimumCancellationDays",
                table: "SubscriptionPlans",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "MinimumCancellationMonths",
                table: "SubscriptionPlans",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PaymentInterval",
                table: "SubscriptionPlans",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PenaltyChargedAfterDays",
                table: "SubscriptionPlans",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "PenaltyforUnPaidInstallment",
                table: "SubscriptionPlans",
                type: "decimal(18,2)",
                precision: 18,
                scale: 2,
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Fees",
                table: "PaymentPlans",
                type: "int",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "AdminFeeVatId",
                table: "PaymentPlans",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CreatedBy",
                table: "PaymentPlans",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedOn",
                table: "PaymentPlans",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "DeletedBy",
                table: "PaymentPlans",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedOn",
                table: "PaymentPlans",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "FreezeAvailableVatId",
                table: "PaymentPlans",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "PaymentPlans",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDelete",
                table: "PaymentPlans",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "JoiningFeeVatId",
                table: "PaymentPlans",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MembershipFeeVatId",
                table: "PaymentPlans",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ModifiedBy",
                table: "PaymentPlans",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedOn",
                table: "PaymentPlans",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PenaltyforUnPaidInstallmentVatId",
                table: "PaymentPlans",
                type: "int",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_PaymentPlans",
                table: "PaymentPlans",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "AssignedSubscriptionPlanClub",
                columns: table => new
                {
                    SubscriptionPlanId = table.Column<int>(type: "int", nullable: false),
                    ClubId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AssignedSubscriptionPlanClub", x => new { x.SubscriptionPlanId, x.ClubId });
                    table.ForeignKey(
                        name: "FK_AssignedSubscriptionPlanClub_Clubs_ClubId",
                        column: x => x.ClubId,
                        principalTable: "Clubs",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AssignedSubscriptionPlanClub_SubscriptionPlans_SubscriptionPlanId",
                        column: x => x.SubscriptionPlanId,
                        principalTable: "SubscriptionPlans",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "AvailableSubscriptionPlanClub",
                columns: table => new
                {
                    SubscriptionPlanId = table.Column<int>(type: "int", nullable: false),
                    ClubId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AvailableSubscriptionPlanClub", x => new { x.SubscriptionPlanId, x.ClubId });
                    table.ForeignKey(
                        name: "FK_AvailableSubscriptionPlanClub_Clubs_ClubId",
                        column: x => x.ClubId,
                        principalTable: "Clubs",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AvailableSubscriptionPlanClub_SubscriptionPlans_SubscriptionPlanId",
                        column: x => x.SubscriptionPlanId,
                        principalTable: "SubscriptionPlans",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "SubscriptionPlanPaymentMethod",
                columns: table => new
                {
                    SubscriptionPlanId = table.Column<int>(type: "int", nullable: false),
                    PaymentMethodId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubscriptionPlanPaymentMethod", x => new { x.SubscriptionPlanId, x.PaymentMethodId });
                    table.ForeignKey(
                        name: "FK_SubscriptionPlanPaymentMethod_Clubs_PaymentMethodId",
                        column: x => x.PaymentMethodId,
                        principalTable: "Clubs",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_SubscriptionPlanPaymentMethod_SubscriptionPlans_SubscriptionPlanId",
                        column: x => x.SubscriptionPlanId,
                        principalTable: "SubscriptionPlans",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "SubscriptionPlanVisibility",
                columns: table => new
                {
                    RoleId = table.Column<int>(type: "int", nullable: false),
                    SubscriptionPlanId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubscriptionPlanVisibility", x => new { x.SubscriptionPlanId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_SubscriptionPlanVisibility_Role_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Role",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_SubscriptionPlanVisibility_SubscriptionPlans_SubscriptionPlanId",
                        column: x => x.SubscriptionPlanId,
                        principalTable: "SubscriptionPlans",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Tag",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
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
                    table.PrimaryKey("PK_Tag", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SubscriptionPlanTag",
                columns: table => new
                {
                    SubscriptionPlansId = table.Column<int>(type: "int", nullable: false),
                    TagsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubscriptionPlanTag", x => new { x.SubscriptionPlansId, x.TagsId });
                    table.ForeignKey(
                        name: "FK_SubscriptionPlanTag_SubscriptionPlans_SubscriptionPlansId",
                        column: x => x.SubscriptionPlansId,
                        principalTable: "SubscriptionPlans",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_SubscriptionPlanTag_Tag_TagsId",
                        column: x => x.TagsId,
                        principalTable: "Tag",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_SubscriptionPlans_AdminFeeVatId",
                table: "SubscriptionPlans",
                column: "AdminFeeVatId");

            migrationBuilder.CreateIndex(
                name: "IX_SubscriptionPlans_FreezeAvailableVatId",
                table: "SubscriptionPlans",
                column: "FreezeAvailableVatId");

            migrationBuilder.CreateIndex(
                name: "IX_SubscriptionPlans_JoiningFeeVatId",
                table: "SubscriptionPlans",
                column: "JoiningFeeVatId");

            migrationBuilder.CreateIndex(
                name: "IX_SubscriptionPlans_MembershipFeeVatId",
                table: "SubscriptionPlans",
                column: "MembershipFeeVatId");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentPlans_AdminFeeVatId",
                table: "PaymentPlans",
                column: "AdminFeeVatId");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentPlans_FreezeAvailableVatId",
                table: "PaymentPlans",
                column: "FreezeAvailableVatId");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentPlans_JoiningFeeVatId",
                table: "PaymentPlans",
                column: "JoiningFeeVatId");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentPlans_MembershipFeeVatId",
                table: "PaymentPlans",
                column: "MembershipFeeVatId");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentPlans_PenaltyforUnPaidInstallmentVatId",
                table: "PaymentPlans",
                column: "PenaltyforUnPaidInstallmentVatId");

            migrationBuilder.CreateIndex(
                name: "IX_AssignedSubscriptionPlanClub_ClubId",
                table: "AssignedSubscriptionPlanClub",
                column: "ClubId");

            migrationBuilder.CreateIndex(
                name: "IX_AvailableSubscriptionPlanClub_ClubId",
                table: "AvailableSubscriptionPlanClub",
                column: "ClubId");

            migrationBuilder.CreateIndex(
                name: "IX_SubscriptionPlanPaymentMethod_PaymentMethodId",
                table: "SubscriptionPlanPaymentMethod",
                column: "PaymentMethodId");

            migrationBuilder.CreateIndex(
                name: "IX_SubscriptionPlanTag_TagsId",
                table: "SubscriptionPlanTag",
                column: "TagsId");

            migrationBuilder.CreateIndex(
                name: "IX_SubscriptionPlanVisibility_RoleId",
                table: "SubscriptionPlanVisibility",
                column: "RoleId");

            migrationBuilder.AddForeignKey(
                name: "FK_PaymentPlans_Vat_AdminFeeVatId",
                table: "PaymentPlans",
                column: "AdminFeeVatId",
                principalTable: "Vat",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PaymentPlans_Vat_FreezeAvailableVatId",
                table: "PaymentPlans",
                column: "FreezeAvailableVatId",
                principalTable: "Vat",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PaymentPlans_Vat_JoiningFeeVatId",
                table: "PaymentPlans",
                column: "JoiningFeeVatId",
                principalTable: "Vat",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PaymentPlans_Vat_MembershipFeeVatId",
                table: "PaymentPlans",
                column: "MembershipFeeVatId",
                principalTable: "Vat",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PaymentPlans_Vat_PenaltyforUnPaidInstallmentVatId",
                table: "PaymentPlans",
                column: "PenaltyforUnPaidInstallmentVatId",
                principalTable: "Vat",
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

            migrationBuilder.AddForeignKey(
                name: "FK_SubscriptionPlans_Vat_JoiningFeeVatId",
                table: "SubscriptionPlans",
                column: "JoiningFeeVatId",
                principalTable: "Vat",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_SubscriptionPlans_Vat_MembershipFeeVatId",
                table: "SubscriptionPlans",
                column: "MembershipFeeVatId",
                principalTable: "Vat",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_SubscriptionPlans_Vat_PenaltyforUnPaidInstallmentVatId",
                table: "SubscriptionPlans",
                column: "PenaltyforUnPaidInstallmentVatId",
                principalTable: "Vat",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PaymentPlans_Vat_AdminFeeVatId",
                table: "PaymentPlans");

            migrationBuilder.DropForeignKey(
                name: "FK_PaymentPlans_Vat_FreezeAvailableVatId",
                table: "PaymentPlans");

            migrationBuilder.DropForeignKey(
                name: "FK_PaymentPlans_Vat_JoiningFeeVatId",
                table: "PaymentPlans");

            migrationBuilder.DropForeignKey(
                name: "FK_PaymentPlans_Vat_MembershipFeeVatId",
                table: "PaymentPlans");

            migrationBuilder.DropForeignKey(
                name: "FK_PaymentPlans_Vat_PenaltyforUnPaidInstallmentVatId",
                table: "PaymentPlans");

            migrationBuilder.DropForeignKey(
                name: "FK_SubscriptionPlans_PaymentPlans_PaymentPlanId",
                table: "SubscriptionPlans");

            migrationBuilder.DropForeignKey(
                name: "FK_SubscriptionPlans_Vat_AdminFeeVatId",
                table: "SubscriptionPlans");

            migrationBuilder.DropForeignKey(
                name: "FK_SubscriptionPlans_Vat_FreezeAvailableVatId",
                table: "SubscriptionPlans");

            migrationBuilder.DropForeignKey(
                name: "FK_SubscriptionPlans_Vat_JoiningFeeVatId",
                table: "SubscriptionPlans");

            migrationBuilder.DropForeignKey(
                name: "FK_SubscriptionPlans_Vat_MembershipFeeVatId",
                table: "SubscriptionPlans");

            migrationBuilder.DropForeignKey(
                name: "FK_SubscriptionPlans_Vat_PenaltyforUnPaidInstallmentVatId",
                table: "SubscriptionPlans");

            migrationBuilder.DropTable(
                name: "AssignedSubscriptionPlanClub");

            migrationBuilder.DropTable(
                name: "AvailableSubscriptionPlanClub");

            migrationBuilder.DropTable(
                name: "SubscriptionPlanPaymentMethod");

            migrationBuilder.DropTable(
                name: "SubscriptionPlanTag");

            migrationBuilder.DropTable(
                name: "SubscriptionPlanVisibility");

            migrationBuilder.DropTable(
                name: "Tag");

            migrationBuilder.DropIndex(
                name: "IX_SubscriptionPlans_AdminFeeVatId",
                table: "SubscriptionPlans");

            migrationBuilder.DropIndex(
                name: "IX_SubscriptionPlans_FreezeAvailableVatId",
                table: "SubscriptionPlans");

            migrationBuilder.DropIndex(
                name: "IX_SubscriptionPlans_JoiningFeeVatId",
                table: "SubscriptionPlans");

            migrationBuilder.DropIndex(
                name: "IX_SubscriptionPlans_MembershipFeeVatId",
                table: "SubscriptionPlans");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PaymentPlans",
                table: "PaymentPlans");

            migrationBuilder.DropIndex(
                name: "IX_PaymentPlans_AdminFeeVatId",
                table: "PaymentPlans");

            migrationBuilder.DropIndex(
                name: "IX_PaymentPlans_FreezeAvailableVatId",
                table: "PaymentPlans");

            migrationBuilder.DropIndex(
                name: "IX_PaymentPlans_JoiningFeeVatId",
                table: "PaymentPlans");

            migrationBuilder.DropIndex(
                name: "IX_PaymentPlans_MembershipFeeVatId",
                table: "PaymentPlans");

            migrationBuilder.DropIndex(
                name: "IX_PaymentPlans_PenaltyforUnPaidInstallmentVatId",
                table: "PaymentPlans");

            migrationBuilder.DropColumn(
                name: "AdminFeeVatId",
                table: "SubscriptionPlans");

            migrationBuilder.DropColumn(
                name: "AdministrationFee",
                table: "SubscriptionPlans");

            migrationBuilder.DropColumn(
                name: "CommitmentPeriod",
                table: "SubscriptionPlans");

            migrationBuilder.DropColumn(
                name: "Fees",
                table: "SubscriptionPlans");

            migrationBuilder.DropColumn(
                name: "FreezeAvailable",
                table: "SubscriptionPlans");

            migrationBuilder.DropColumn(
                name: "FreezeAvailableVatId",
                table: "SubscriptionPlans");

            migrationBuilder.DropColumn(
                name: "JoiningFee",
                table: "SubscriptionPlans");

            migrationBuilder.DropColumn(
                name: "JoiningFeeVatId",
                table: "SubscriptionPlans");

            migrationBuilder.DropColumn(
                name: "MaximumCancellationDays",
                table: "SubscriptionPlans");

            migrationBuilder.DropColumn(
                name: "MaximumCancellationMonths",
                table: "SubscriptionPlans");

            migrationBuilder.DropColumn(
                name: "MembershipFee",
                table: "SubscriptionPlans");

            migrationBuilder.DropColumn(
                name: "MembershipFeeVatId",
                table: "SubscriptionPlans");

            migrationBuilder.DropColumn(
                name: "MinimumCancellationDays",
                table: "SubscriptionPlans");

            migrationBuilder.DropColumn(
                name: "MinimumCancellationMonths",
                table: "SubscriptionPlans");

            migrationBuilder.DropColumn(
                name: "PaymentInterval",
                table: "SubscriptionPlans");

            migrationBuilder.DropColumn(
                name: "PenaltyChargedAfterDays",
                table: "SubscriptionPlans");

            migrationBuilder.DropColumn(
                name: "PenaltyforUnPaidInstallment",
                table: "SubscriptionPlans");

            migrationBuilder.DropColumn(
                name: "AdminFeeVatId",
                table: "PaymentPlans");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "PaymentPlans");

            migrationBuilder.DropColumn(
                name: "CreatedOn",
                table: "PaymentPlans");

            migrationBuilder.DropColumn(
                name: "DeletedBy",
                table: "PaymentPlans");

            migrationBuilder.DropColumn(
                name: "DeletedOn",
                table: "PaymentPlans");

            migrationBuilder.DropColumn(
                name: "FreezeAvailableVatId",
                table: "PaymentPlans");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "PaymentPlans");

            migrationBuilder.DropColumn(
                name: "IsDelete",
                table: "PaymentPlans");

            migrationBuilder.DropColumn(
                name: "JoiningFeeVatId",
                table: "PaymentPlans");

            migrationBuilder.DropColumn(
                name: "MembershipFeeVatId",
                table: "PaymentPlans");

            migrationBuilder.DropColumn(
                name: "ModifiedBy",
                table: "PaymentPlans");

            migrationBuilder.DropColumn(
                name: "ModifiedOn",
                table: "PaymentPlans");

            migrationBuilder.DropColumn(
                name: "PenaltyforUnPaidInstallmentVatId",
                table: "PaymentPlans");

            migrationBuilder.RenameTable(
                name: "PaymentPlans",
                newName: "PaymentPlan");

            migrationBuilder.RenameColumn(
                name: "PenaltyforUnPaidInstallmentVatId",
                table: "SubscriptionPlans",
                newName: "ContractSettingsId");

            migrationBuilder.RenameIndex(
                name: "IX_SubscriptionPlans_PenaltyforUnPaidInstallmentVatId",
                table: "SubscriptionPlans",
                newName: "IX_SubscriptionPlans_ContractSettingsId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "PaymentPlan",
                newName: "PaymentPlanId");

            migrationBuilder.AlterColumn<string>(
                name: "PlanType",
                table: "SubscriptionPlans",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<string>(
                name: "Category",
                table: "SubscriptionPlans",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "SubscriptionPlans",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TransferToClubOnContractStartDate",
                table: "SubscriptionPlans",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Visibility",
                table: "SubscriptionPlans",
                type: "bit",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Fees",
                table: "PaymentPlan",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "AdministrationPer",
                table: "PaymentPlan",
                type: "decimal(18,2)",
                precision: 18,
                scale: 2,
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "FreezeAvailablePer",
                table: "PaymentPlan",
                type: "decimal(18,2)",
                precision: 18,
                scale: 2,
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "JoiningFeePer",
                table: "PaymentPlan",
                type: "decimal(18,2)",
                precision: 18,
                scale: 2,
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "MembershipFeePer",
                table: "PaymentPlan",
                type: "decimal(18,2)",
                precision: 18,
                scale: 2,
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "PenaltyforUnPaidInstallmentPer",
                table: "PaymentPlan",
                type: "decimal(18,2)",
                precision: 18,
                scale: 2,
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_PaymentPlan",
                table: "PaymentPlan",
                column: "PaymentPlanId");

            migrationBuilder.CreateTable(
                name: "ContractSetting",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AgreementTemplate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AutomaticRenew = table.Column<bool>(type: "bit", nullable: true),
                    AutomaticallyEndContractAfterCommitmentPeriodReached3months = table.Column<bool>(type: "bit", nullable: true),
                    CanBeRenewed = table.Column<bool>(type: "bit", nullable: true),
                    CanPayForClassesUsingContract = table.Column<bool>(type: "bit", nullable: true),
                    CommitmentPeriod = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ContractEndProRata = table.Column<bool>(type: "bit", nullable: true),
                    CreatedBy = table.Column<int>(type: "int", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DefaultPaymentPlanForEmployee = table.Column<bool>(type: "bit", nullable: true),
                    DeletedBy = table.Column<int>(type: "int", nullable: true),
                    DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DynamicFeePart = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FirstPaymentOnSignupDate = table.Column<bool>(type: "bit", nullable: true),
                    FixFeePart = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    InstallmentPayisReqDurOnLineReg = table.Column<bool>(type: "bit", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false),
                    IsEarlyTerminationFeeCharged = table.Column<bool>(type: "bit", nullable: true),
                    IsLimitedNumberOfVisitsInaPeriod = table.Column<bool>(type: "bit", nullable: true),
                    IsThePeriodAfterWhichYouCanRenewTheContract = table.Column<bool>(type: "bit", nullable: true),
                    LimitTheAbilityToChooseAcontractfortheClubMember = table.Column<bool>(type: "bit", nullable: true),
                    LimitedNumberOfVisitsInaPeriod = table.Column<int>(type: "int", nullable: true),
                    LimitedNumberOfVisitsInaPeriodType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MaximumCancellationPeriod = table.Column<int>(type: "int", nullable: true),
                    ModifiedBy = table.Column<int>(type: "int", nullable: true),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    PaidAllFreeze = table.Column<bool>(type: "bit", nullable: true),
                    PaymentInAdvanceForContract = table.Column<bool>(type: "bit", nullable: true),
                    PaymentIntervalOccursOnContractSelectedDay = table.Column<bool>(type: "bit", nullable: true),
                    PaymentIntervalOccursOnContractStartDayNoProdata = table.Column<bool>(type: "bit", nullable: true),
                    PaymentIntervals = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PaymentMethod = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProrataType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Tax = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TemplateofRules = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ThePeriodAfterWhichYouCanRenewTheContract = table.Column<int>(type: "int", nullable: true),
                    UseAdvanceDuedateschemepolicyforcontractfees = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContractSetting", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SubscriptionPlanClub",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SubscriptionPlanId = table.Column<int>(type: "int", nullable: false),
                    ClubId = table.Column<int>(type: "int", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeletedBy = table.Column<int>(type: "int", nullable: true),
                    DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false),
                    ModifiedBy = table.Column<int>(type: "int", nullable: true),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubscriptionPlanClub", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SubscriptionPlanClub_SubscriptionPlans_SubscriptionPlanId",
                        column: x => x.SubscriptionPlanId,
                        principalTable: "SubscriptionPlans",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_SubscriptionPlanClub_SubscriptionPlanId",
                table: "SubscriptionPlanClub",
                column: "SubscriptionPlanId");

            migrationBuilder.AddForeignKey(
                name: "FK_SubscriptionPlans_ContractSetting_ContractSettingsId",
                table: "SubscriptionPlans",
                column: "ContractSettingsId",
                principalTable: "ContractSetting",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_SubscriptionPlans_PaymentPlan_PaymentPlanId",
                table: "SubscriptionPlans",
                column: "PaymentPlanId",
                principalTable: "PaymentPlan",
                principalColumn: "PaymentPlanId");
        }
    }
}
