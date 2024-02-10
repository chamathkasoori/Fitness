using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Fitness.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class MemberSubscriptionAddon : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MemberSubscriptionAddon",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MemberSubscriptionId = table.Column<int>(type: "int", nullable: false),
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
                    table.PrimaryKey("PK_MemberSubscriptionAddon", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MemberSubscriptionAddon_MemberSubscriptions_MemberSubscriptionId",
                        column: x => x.MemberSubscriptionId,
                        principalTable: "MemberSubscriptions",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_MemberSubscriptionAddon_SubscriptionPlanAddons_SubscriptionPlanAddonId",
                        column: x => x.SubscriptionPlanAddonId,
                        principalTable: "SubscriptionPlanAddons",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_MemberSubscriptionAddon_MemberSubscriptionId",
                table: "MemberSubscriptionAddon",
                column: "MemberSubscriptionId");

            migrationBuilder.CreateIndex(
                name: "IX_MemberSubscriptionAddon_SubscriptionPlanAddonId",
                table: "MemberSubscriptionAddon",
                column: "SubscriptionPlanAddonId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MemberSubscriptionAddon");
        }
    }
}
