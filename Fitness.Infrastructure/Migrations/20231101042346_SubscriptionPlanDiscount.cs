﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Fitness.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class SubscriptionPlanDiscount : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SubscriptionPlanDiscounts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MembershipFeeDiscountApplied = table.Column<bool>(type: "bit", nullable: false),
                    MembershipFeeDiscountType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MembershipFeeDiscountAmount = table.Column<double>(type: "float", nullable: true),
                    MembershipFeeDiscountAfter = table.Column<int>(type: "int", nullable: true),
                    ModifyCommitmentTime = table.Column<bool>(type: "bit", nullable: false),
                    ModifyCommitmentTimeType = table.Column<int>(type: "int", nullable: true),
                    ModifyCommitmentTimeValue = table.Column<int>(type: "int", nullable: true),
                    ApplyToAllSubscriptionPlans = table.Column<bool>(type: "bit", nullable: false),
                    ApplyToAllClubs = table.Column<bool>(type: "bit", nullable: false),
                    OnlyForChoosenRoles = table.Column<bool>(type: "bit", nullable: false),
                    OnlyForChoosenSources = table.Column<bool>(type: "bit", nullable: false),
                    AvailableOnlyWithPromoCode = table.Column<bool>(type: "bit", nullable: false),
                    PromoCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AddAutomaticallyToAutomaticallyRenewedPlans = table.Column<bool>(type: "bit", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
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
                    table.PrimaryKey("PK_SubscriptionPlanDiscounts", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SubscriptionPlanDiscounts");
        }
    }
}
