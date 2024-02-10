﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Fitness.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class SubscriptionPlanDiscountSubscriptionPlan : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SubscriptionPlanSubscriptionPlanDiscounts");

            migrationBuilder.CreateTable(
                name: "SubscriptionPlanDiscountSubscriptionPlans",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SubscriptionPlanId = table.Column<int>(type: "int", nullable: false),
                    SubscriptionPlanDiscountId = table.Column<int>(type: "int", nullable: false),
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
                    table.PrimaryKey("PK_SubscriptionPlanDiscountSubscriptionPlans", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SubscriptionPlanDiscountSubscriptionPlans_SubscriptionPlanDiscounts_SubscriptionPlanDiscountId",
                        column: x => x.SubscriptionPlanDiscountId,
                        principalTable: "SubscriptionPlanDiscounts",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_SubscriptionPlanDiscountSubscriptionPlans_SubscriptionPlans_SubscriptionPlanId",
                        column: x => x.SubscriptionPlanId,
                        principalTable: "SubscriptionPlans",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_SubscriptionPlanDiscountSubscriptionPlans_SubscriptionPlanDiscountId",
                table: "SubscriptionPlanDiscountSubscriptionPlans",
                column: "SubscriptionPlanDiscountId");

            migrationBuilder.CreateIndex(
                name: "IX_SubscriptionPlanDiscountSubscriptionPlans_SubscriptionPlanId",
                table: "SubscriptionPlanDiscountSubscriptionPlans",
                column: "SubscriptionPlanId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SubscriptionPlanDiscountSubscriptionPlans");

            migrationBuilder.CreateTable(
                name: "SubscriptionPlanSubscriptionPlanDiscounts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SubscriptionPlanDiscountId = table.Column<int>(type: "int", nullable: false),
                    SubscriptionPlanId = table.Column<int>(type: "int", nullable: false),
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
                    table.PrimaryKey("PK_SubscriptionPlanSubscriptionPlanDiscounts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SubscriptionPlanSubscriptionPlanDiscounts_SubscriptionPlanDiscounts_SubscriptionPlanDiscountId",
                        column: x => x.SubscriptionPlanDiscountId,
                        principalTable: "SubscriptionPlanDiscounts",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_SubscriptionPlanSubscriptionPlanDiscounts_SubscriptionPlans_SubscriptionPlanId",
                        column: x => x.SubscriptionPlanId,
                        principalTable: "SubscriptionPlans",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_SubscriptionPlanSubscriptionPlanDiscounts_SubscriptionPlanDiscountId",
                table: "SubscriptionPlanSubscriptionPlanDiscounts",
                column: "SubscriptionPlanDiscountId");

            migrationBuilder.CreateIndex(
                name: "IX_SubscriptionPlanSubscriptionPlanDiscounts_SubscriptionPlanId",
                table: "SubscriptionPlanSubscriptionPlanDiscounts",
                column: "SubscriptionPlanId");
        }
    }
}
