using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Fitness.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class SubscriptionPlanSubscriptionPlanAddon : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<bool>(
                name: "AdditionalPaymentPlan",
                table: "SubscriptionPlans",
                type: "bit",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "SubscriptionPlanSubscriptionPlanAddons",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SubscriptionPlanId = table.Column<int>(type: "int", nullable: false),
                    SubscriptionPlanAddonId = table.Column<int>(type: "int", nullable: false),
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
                    table.PrimaryKey("PK_SubscriptionPlanSubscriptionPlanAddons", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SubscriptionPlanSubscriptionPlanAddons_SubscriptionPlanAddons_SubscriptionPlanAddonId",
                        column: x => x.SubscriptionPlanAddonId,
                        principalTable: "SubscriptionPlanAddons",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_SubscriptionPlanSubscriptionPlanAddons_SubscriptionPlans_SubscriptionPlanId",
                        column: x => x.SubscriptionPlanId,
                        principalTable: "SubscriptionPlans",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_SubscriptionPlanSubscriptionPlanAddons_SubscriptionPlanAddonId",
                table: "SubscriptionPlanSubscriptionPlanAddons",
                column: "SubscriptionPlanAddonId");

            migrationBuilder.CreateIndex(
                name: "IX_SubscriptionPlanSubscriptionPlanAddons_SubscriptionPlanId",
                table: "SubscriptionPlanSubscriptionPlanAddons",
                column: "SubscriptionPlanId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SubscriptionPlanSubscriptionPlanAddons");

            migrationBuilder.AlterColumn<bool>(
                name: "AdditionalPaymentPlan",
                table: "SubscriptionPlans",
                type: "bit",
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "bit");
        }
    }
}
