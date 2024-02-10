using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Fitness.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AccessRuleItemAssignedClub : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AccessRuleItemAssignedClubs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AccessRuleItemId = table.Column<int>(type: "int", nullable: false),
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
                    table.PrimaryKey("PK_AccessRuleItemAssignedClubs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AccessRuleItemAssignedClubs_AccessRuleItems_AccessRuleItemId",
                        column: x => x.AccessRuleItemId,
                        principalTable: "AccessRuleItems",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AccessRuleItemAssignedClubs_Clubs_ClubId",
                        column: x => x.ClubId,
                        principalTable: "Clubs",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_AccessRuleItemAssignedClubs_AccessRuleItemId",
                table: "AccessRuleItemAssignedClubs",
                column: "AccessRuleItemId");

            migrationBuilder.CreateIndex(
                name: "IX_AccessRuleItemAssignedClubs_ClubId",
                table: "AccessRuleItemAssignedClubs",
                column: "ClubId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AccessRuleItemAssignedClubs");
        }
    }
}
