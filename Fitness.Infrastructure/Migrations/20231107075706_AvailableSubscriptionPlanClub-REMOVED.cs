using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Fitness.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AvailableSubscriptionPlanClubREMOVED : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AvailableSubscriptionPlanClubs_Clubs_ClubId",
                table: "AvailableSubscriptionPlanClubs");

            migrationBuilder.DropForeignKey(
                name: "FK_AvailableSubscriptionPlanClubs_SubscriptionPlans_SubscriptionPlanId",
                table: "AvailableSubscriptionPlanClubs");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AvailableSubscriptionPlanClubs",
                table: "AvailableSubscriptionPlanClubs");

            migrationBuilder.RenameTable(
                name: "AvailableSubscriptionPlanClubs",
                newName: "AvailableSubscriptionPlanClub");

            migrationBuilder.RenameIndex(
                name: "IX_AvailableSubscriptionPlanClubs_ClubId",
                table: "AvailableSubscriptionPlanClub",
                newName: "IX_AvailableSubscriptionPlanClub_ClubId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AvailableSubscriptionPlanClub",
                table: "AvailableSubscriptionPlanClub",
                columns: new[] { "SubscriptionPlanId", "ClubId" });

            migrationBuilder.AddForeignKey(
                name: "FK_AvailableSubscriptionPlanClub_Clubs_ClubId",
                table: "AvailableSubscriptionPlanClub",
                column: "ClubId",
                principalTable: "Clubs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AvailableSubscriptionPlanClub_SubscriptionPlans_SubscriptionPlanId",
                table: "AvailableSubscriptionPlanClub",
                column: "SubscriptionPlanId",
                principalTable: "SubscriptionPlans",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AvailableSubscriptionPlanClub_Clubs_ClubId",
                table: "AvailableSubscriptionPlanClub");

            migrationBuilder.DropForeignKey(
                name: "FK_AvailableSubscriptionPlanClub_SubscriptionPlans_SubscriptionPlanId",
                table: "AvailableSubscriptionPlanClub");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AvailableSubscriptionPlanClub",
                table: "AvailableSubscriptionPlanClub");

            migrationBuilder.RenameTable(
                name: "AvailableSubscriptionPlanClub",
                newName: "AvailableSubscriptionPlanClubs");

            migrationBuilder.RenameIndex(
                name: "IX_AvailableSubscriptionPlanClub_ClubId",
                table: "AvailableSubscriptionPlanClubs",
                newName: "IX_AvailableSubscriptionPlanClubs_ClubId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AvailableSubscriptionPlanClubs",
                table: "AvailableSubscriptionPlanClubs",
                columns: new[] { "SubscriptionPlanId", "ClubId" });

            migrationBuilder.AddForeignKey(
                name: "FK_AvailableSubscriptionPlanClubs_Clubs_ClubId",
                table: "AvailableSubscriptionPlanClubs",
                column: "ClubId",
                principalTable: "Clubs",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AvailableSubscriptionPlanClubs_SubscriptionPlans_SubscriptionPlanId",
                table: "AvailableSubscriptionPlanClubs",
                column: "SubscriptionPlanId",
                principalTable: "SubscriptionPlans",
                principalColumn: "Id");
        }
    }
}
