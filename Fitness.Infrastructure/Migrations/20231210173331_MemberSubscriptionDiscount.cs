using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Fitness.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class MemberSubscriptionDiscount : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MemberSubscriptions_SubscriptionPlanDiscounts_SubscriptionPlanDiscountId",
                table: "MemberSubscriptions");

            migrationBuilder.DropIndex(
                name: "IX_MemberSubscriptions_SubscriptionPlanDiscountId",
                table: "MemberSubscriptions");

            migrationBuilder.DropColumn(
                name: "SubscriptionPlanDiscountId",
                table: "MemberSubscriptions");

            migrationBuilder.CreateTable(
                name: "MemberSubscriptionDiscount",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MemberSubscriptionId = table.Column<int>(type: "int", nullable: false),
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
                    table.PrimaryKey("PK_MemberSubscriptionDiscount", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MemberSubscriptionDiscount_MemberSubscriptions_MemberSubscriptionId",
                        column: x => x.MemberSubscriptionId,
                        principalTable: "MemberSubscriptions",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_MemberSubscriptionDiscount_SubscriptionPlanDiscounts_SubscriptionPlanDiscountId",
                        column: x => x.SubscriptionPlanDiscountId,
                        principalTable: "SubscriptionPlanDiscounts",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_MemberSubscriptionDiscount_MemberSubscriptionId",
                table: "MemberSubscriptionDiscount",
                column: "MemberSubscriptionId");

            migrationBuilder.CreateIndex(
                name: "IX_MemberSubscriptionDiscount_SubscriptionPlanDiscountId",
                table: "MemberSubscriptionDiscount",
                column: "SubscriptionPlanDiscountId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MemberSubscriptionDiscount");

            migrationBuilder.AddColumn<int>(
                name: "SubscriptionPlanDiscountId",
                table: "MemberSubscriptions",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_MemberSubscriptions_SubscriptionPlanDiscountId",
                table: "MemberSubscriptions",
                column: "SubscriptionPlanDiscountId");

            migrationBuilder.AddForeignKey(
                name: "FK_MemberSubscriptions_SubscriptionPlanDiscounts_SubscriptionPlanDiscountId",
                table: "MemberSubscriptions",
                column: "SubscriptionPlanDiscountId",
                principalTable: "SubscriptionPlanDiscounts",
                principalColumn: "Id");
        }
    }
}
