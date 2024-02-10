using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Fitness.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ProductVat : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductCategories_Icons_IconId",
                table: "ProductCategories");

            migrationBuilder.DropIndex(
                name: "IX_ProductCategories_IconId",
                table: "ProductCategories");

            migrationBuilder.DropColumn(
                name: "VAT",
                table: "Products");

            migrationBuilder.AddColumn<int>(
                name: "VatId",
                table: "Products",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "ContractSetting",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PaymentMethod = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PaymentIntervals = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CommitmentPeriod = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MaximumCancellationPeriod = table.Column<int>(type: "int", nullable: true),
                    FirstPaymentOnSignupDate = table.Column<bool>(type: "bit", nullable: true),
                    AutomaticallyEndContractAfterCommitmentPeriodReached3months = table.Column<bool>(type: "bit", nullable: true),
                    IsLimitedNumberOfVisitsInaPeriod = table.Column<bool>(type: "bit", nullable: true),
                    LimitedNumberOfVisitsInaPeriodType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LimitedNumberOfVisitsInaPeriod = table.Column<int>(type: "int", nullable: true),
                    LimitTheAbilityToChooseAcontractfortheClubMember = table.Column<bool>(type: "bit", nullable: true),
                    IsThePeriodAfterWhichYouCanRenewTheContract = table.Column<bool>(type: "bit", nullable: true),
                    ThePeriodAfterWhichYouCanRenewTheContract = table.Column<int>(type: "int", nullable: true),
                    PaymentIntervalOccursOnContractStartDayNoProdata = table.Column<bool>(type: "bit", nullable: true),
                    PaymentIntervalOccursOnContractSelectedDay = table.Column<bool>(type: "bit", nullable: true),
                    ContractEndProRata = table.Column<bool>(type: "bit", nullable: true),
                    AutomaticRenew = table.Column<bool>(type: "bit", nullable: true),
                    CanBeRenewed = table.Column<bool>(type: "bit", nullable: true),
                    PaidAllFreeze = table.Column<bool>(type: "bit", nullable: true),
                    CanPayForClassesUsingContract = table.Column<bool>(type: "bit", nullable: true),
                    ProrataType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DefaultPaymentPlanForEmployee = table.Column<bool>(type: "bit", nullable: true),
                    AgreementTemplate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TemplateofRules = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsEarlyTerminationFeeCharged = table.Column<bool>(type: "bit", nullable: true),
                    DynamicFeePart = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FixFeePart = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Tax = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    InstallmentPayisReqDurOnLineReg = table.Column<bool>(type: "bit", nullable: true),
                    PaymentInAdvanceForContract = table.Column<bool>(type: "bit", nullable: true),
                    UseAdvanceDuedateschemepolicyforcontractfees = table.Column<bool>(type: "bit", nullable: true),
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
                    table.PrimaryKey("PK_ContractSetting", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PaymentPlan",
                columns: table => new
                {
                    PaymentPlanId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Fees = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MembershipFee = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: true),
                    MembershipFeePer = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: true),
                    JoiningFee = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: true),
                    JoiningFeePer = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: true),
                    AdministrationFee = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: true),
                    AdministrationPer = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: true),
                    DepositValue = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: true),
                    FreezeAvailable = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: true),
                    FreezeAvailablePer = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: true),
                    AllowableDebit = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: true),
                    PenaltyforUnPaidInstallment = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: true),
                    PenaltyforUnPaidInstallmentPer = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: true),
                    PenaltyChargedAfterDays = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentPlan", x => x.PaymentPlanId);
                });

            migrationBuilder.CreateTable(
                name: "ProductAvailableApplications",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    ApplicationId = table.Column<int>(type: "int", nullable: false),
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
                    table.PrimaryKey("PK_ProductAvailableApplications", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductAvailableApplications_Applications_ApplicationId",
                        column: x => x.ApplicationId,
                        principalTable: "Applications",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ProductAvailableApplications_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "SubscriptionPlans",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    MinimumAge = table.Column<int>(type: "int", nullable: true),
                    MaximumAge = table.Column<int>(type: "int", nullable: true),
                    FiscalName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PlanType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AdditionalPaymentPlan = table.Column<bool>(type: "bit", nullable: true),
                    PaymentPlanId = table.Column<int>(type: "int", nullable: true),
                    ContractSettingsId = table.Column<int>(type: "int", nullable: true),
                    Visibility = table.Column<bool>(type: "bit", nullable: true),
                    Category = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TransferToClubOnContractStartDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: true),
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
                    table.PrimaryKey("PK_SubscriptionPlans", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SubscriptionPlans_ContractSetting_ContractSettingsId",
                        column: x => x.ContractSettingsId,
                        principalTable: "ContractSetting",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_SubscriptionPlans_PaymentPlan_PaymentPlanId",
                        column: x => x.PaymentPlanId,
                        principalTable: "PaymentPlan",
                        principalColumn: "PaymentPlanId");
                });

            migrationBuilder.CreateTable(
                name: "SubscriptionPlanApplications",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ApplicationId = table.Column<int>(type: "int", nullable: false),
                    SubscriptionPlanId = table.Column<int>(type: "int", nullable: false),
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
                    table.PrimaryKey("PK_SubscriptionPlanApplications", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SubscriptionPlanApplications_Applications_ApplicationId",
                        column: x => x.ApplicationId,
                        principalTable: "Applications",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_SubscriptionPlanApplications_SubscriptionPlans_SubscriptionPlanId",
                        column: x => x.SubscriptionPlanId,
                        principalTable: "SubscriptionPlans",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "SubscriptionPlanClub",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SubscriptionPlanId = table.Column<int>(type: "int", nullable: false),
                    ClubId = table.Column<int>(type: "int", nullable: false),
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
                    table.PrimaryKey("PK_SubscriptionPlanClub", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SubscriptionPlanClub_SubscriptionPlans_SubscriptionPlanId",
                        column: x => x.SubscriptionPlanId,
                        principalTable: "SubscriptionPlans",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Subscriptions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SubscriptionPlanId = table.Column<int>(type: "int", nullable: false),
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
                    table.PrimaryKey("PK_Subscriptions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Subscriptions_SubscriptionPlans_SubscriptionPlanId",
                        column: x => x.SubscriptionPlanId,
                        principalTable: "SubscriptionPlans",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Member",
                columns: table => new
                {
                    MemberId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    ClubId = table.Column<int>(type: "int", nullable: true),
                    PreferredLanguage = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Campaign = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MethodOfContact = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Rating = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LeadStatus = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserPayPreference = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MemberGoals = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HeightInCm = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: true),
                    WeightInKg = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: true),
                    PaymentSources = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FitnessGoals = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MedicalHistory = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PersonalIdentificationNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HasAcceptedTerms = table.Column<bool>(type: "bit", nullable: true),
                    SubscriptionId = table.Column<int>(type: "int", nullable: true),
                    MemberNo = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Member", x => x.MemberId);
                    table.ForeignKey(
                        name: "FK_Member_Subscriptions_SubscriptionId",
                        column: x => x.SubscriptionId,
                        principalTable: "Subscriptions",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Member_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Products_VatId",
                table: "Products",
                column: "VatId");

            migrationBuilder.CreateIndex(
                name: "IX_Member_SubscriptionId",
                table: "Member",
                column: "SubscriptionId");

            migrationBuilder.CreateIndex(
                name: "IX_Member_UserId",
                table: "Member",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductAvailableApplications_ApplicationId",
                table: "ProductAvailableApplications",
                column: "ApplicationId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductAvailableApplications_ProductId",
                table: "ProductAvailableApplications",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_SubscriptionPlanApplications_ApplicationId",
                table: "SubscriptionPlanApplications",
                column: "ApplicationId");

            migrationBuilder.CreateIndex(
                name: "IX_SubscriptionPlanApplications_SubscriptionPlanId",
                table: "SubscriptionPlanApplications",
                column: "SubscriptionPlanId");

            migrationBuilder.CreateIndex(
                name: "IX_SubscriptionPlanClub_SubscriptionPlanId",
                table: "SubscriptionPlanClub",
                column: "SubscriptionPlanId");

            migrationBuilder.CreateIndex(
                name: "IX_SubscriptionPlans_ContractSettingsId",
                table: "SubscriptionPlans",
                column: "ContractSettingsId");

            migrationBuilder.CreateIndex(
                name: "IX_SubscriptionPlans_PaymentPlanId",
                table: "SubscriptionPlans",
                column: "PaymentPlanId");

            migrationBuilder.CreateIndex(
                name: "IX_Subscriptions_SubscriptionPlanId",
                table: "Subscriptions",
                column: "SubscriptionPlanId");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Vat_VatId",
                table: "Products",
                column: "VatId",
                principalTable: "Vat",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_Vat_VatId",
                table: "Products");

            migrationBuilder.DropTable(
                name: "Member");

            migrationBuilder.DropTable(
                name: "ProductAvailableApplications");

            migrationBuilder.DropTable(
                name: "SubscriptionPlanApplications");

            migrationBuilder.DropTable(
                name: "SubscriptionPlanClub");

            migrationBuilder.DropTable(
                name: "Subscriptions");

            migrationBuilder.DropTable(
                name: "SubscriptionPlans");

            migrationBuilder.DropTable(
                name: "ContractSetting");

            migrationBuilder.DropTable(
                name: "PaymentPlan");

            migrationBuilder.DropIndex(
                name: "IX_Products_VatId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "VatId",
                table: "Products");

            migrationBuilder.AddColumn<string>(
                name: "VAT",
                table: "Products",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProductCategories_IconId",
                table: "ProductCategories",
                column: "IconId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductCategories_Icons_IconId",
                table: "ProductCategories",
                column: "IconId",
                principalTable: "Icons",
                principalColumn: "Id");
        }
    }
}
