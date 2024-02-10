using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Fitness.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class SubscriptionPlanAddon : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SubscriptionPlanAddons",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GrossFee = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    GrossFeeVatId = table.Column<int>(type: "int", nullable: true),
                    ClubServiceTypeId = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: true),
                    PenaltyLimit = table.Column<int>(type: "int", nullable: true),
                    PenaltyFee = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    PenaltyFeeVatId = table.Column<int>(type: "int", nullable: true),
                    StartOnFirstDayOfMonth = table.Column<bool>(type: "bit", nullable: false),
                    EndOnLastDayOfMonth = table.Column<bool>(type: "bit", nullable: false),
                    AddAllProducts = table.Column<bool>(type: "bit", nullable: false),
                    NumberOfPeriodsForAddProducts = table.Column<int>(type: "int", nullable: true),
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
                    table.PrimaryKey("PK_SubscriptionPlanAddons", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SubscriptionPlanAddons_Vat_GrossFeeVatId",
                        column: x => x.GrossFeeVatId,
                        principalTable: "Vat",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_SubscriptionPlanAddons_Vat_PenaltyFeeVatId",
                        column: x => x.PenaltyFeeVatId,
                        principalTable: "Vat",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "SubscriptionPlanAddonClubs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SubscriptionPlanAddonId = table.Column<int>(type: "int", nullable: false),
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
                    table.PrimaryKey("PK_SubscriptionPlanAddonClubs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SubscriptionPlanAddonClubs_Clubs_ClubId",
                        column: x => x.ClubId,
                        principalTable: "Clubs",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_SubscriptionPlanAddonClubs_SubscriptionPlanAddons_SubscriptionPlanAddonId",
                        column: x => x.SubscriptionPlanAddonId,
                        principalTable: "SubscriptionPlanAddons",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_SubscriptionPlanAddonClubs_ClubId",
                table: "SubscriptionPlanAddonClubs",
                column: "ClubId");

            migrationBuilder.CreateIndex(
                name: "IX_SubscriptionPlanAddonClubs_SubscriptionPlanAddonId",
                table: "SubscriptionPlanAddonClubs",
                column: "SubscriptionPlanAddonId");

            migrationBuilder.CreateIndex(
                name: "IX_SubscriptionPlanAddons_GrossFeeVatId",
                table: "SubscriptionPlanAddons",
                column: "GrossFeeVatId");

            migrationBuilder.CreateIndex(
                name: "IX_SubscriptionPlanAddons_PenaltyFeeVatId",
                table: "SubscriptionPlanAddons",
                column: "PenaltyFeeVatId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SubscriptionPlanAddonClubs");

            migrationBuilder.DropTable(
                name: "SubscriptionPlanAddons");
        }
    }
}
