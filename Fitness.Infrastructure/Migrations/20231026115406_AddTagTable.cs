using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Fitness.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddTagTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AssignedSubscriptionPlanClub_Clubs_ClubId",
                table: "AssignedSubscriptionPlanClub");

            migrationBuilder.DropForeignKey(
                name: "FK_AssignedSubscriptionPlanClub_SubscriptionPlans_SubscriptionPlanId",
                table: "AssignedSubscriptionPlanClub");

            migrationBuilder.DropForeignKey(
                name: "FK_AvailableSubscriptionPlanClub_Clubs_ClubId",
                table: "AvailableSubscriptionPlanClub");

            migrationBuilder.DropForeignKey(
                name: "FK_AvailableSubscriptionPlanClub_SubscriptionPlans_SubscriptionPlanId",
                table: "AvailableSubscriptionPlanClub");

            migrationBuilder.DropForeignKey(
                name: "FK_SubscriptionPlanVisibility_Role_RoleId",
                table: "SubscriptionPlanVisibility");

            migrationBuilder.DropForeignKey(
                name: "FK_SubscriptionPlanVisibility_SubscriptionPlans_SubscriptionPlanId",
                table: "SubscriptionPlanVisibility");

            migrationBuilder.DropTable(
                name: "SubscriptionPlanTag");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Tag",
                table: "Tag");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SubscriptionPlanVisibility",
                table: "SubscriptionPlanVisibility");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AvailableSubscriptionPlanClub",
                table: "AvailableSubscriptionPlanClub");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AssignedSubscriptionPlanClub",
                table: "AssignedSubscriptionPlanClub");

            migrationBuilder.RenameTable(
                name: "Tag",
                newName: "Tags");

            migrationBuilder.RenameTable(
                name: "SubscriptionPlanVisibility",
                newName: "SubscriptionPlanVisibilities");

            migrationBuilder.RenameTable(
                name: "AvailableSubscriptionPlanClub",
                newName: "AvailableSubscriptionPlanClubs");

            migrationBuilder.RenameTable(
                name: "AssignedSubscriptionPlanClub",
                newName: "AssignedSubscriptionPlanClubs");

            migrationBuilder.RenameIndex(
                name: "IX_SubscriptionPlanVisibility_RoleId",
                table: "SubscriptionPlanVisibilities",
                newName: "IX_SubscriptionPlanVisibilities_RoleId");

            migrationBuilder.RenameIndex(
                name: "IX_AvailableSubscriptionPlanClub_ClubId",
                table: "AvailableSubscriptionPlanClubs",
                newName: "IX_AvailableSubscriptionPlanClubs_ClubId");

            migrationBuilder.RenameIndex(
                name: "IX_AssignedSubscriptionPlanClub_ClubId",
                table: "AssignedSubscriptionPlanClubs",
                newName: "IX_AssignedSubscriptionPlanClubs_ClubId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Tags",
                table: "Tags",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SubscriptionPlanVisibilities",
                table: "SubscriptionPlanVisibilities",
                columns: new[] { "SubscriptionPlanId", "RoleId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_AvailableSubscriptionPlanClubs",
                table: "AvailableSubscriptionPlanClubs",
                columns: new[] { "SubscriptionPlanId", "ClubId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_AssignedSubscriptionPlanClubs",
                table: "AssignedSubscriptionPlanClubs",
                columns: new[] { "SubscriptionPlanId", "ClubId" });

            migrationBuilder.CreateTable(
                name: "SubscriptionPlanTags",
                columns: table => new
                {
                    SubscriptionPlanId = table.Column<int>(type: "int", nullable: false),
                    TagId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubscriptionPlanTags", x => new { x.SubscriptionPlanId, x.TagId });
                    table.ForeignKey(
                        name: "FK_SubscriptionPlanTags_SubscriptionPlans_SubscriptionPlanId",
                        column: x => x.SubscriptionPlanId,
                        principalTable: "SubscriptionPlans",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_SubscriptionPlanTags_Tags_TagId",
                        column: x => x.TagId,
                        principalTable: "Tags",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_SubscriptionPlanTags_TagId",
                table: "SubscriptionPlanTags",
                column: "TagId");

            migrationBuilder.AddForeignKey(
                name: "FK_AssignedSubscriptionPlanClubs_Clubs_ClubId",
                table: "AssignedSubscriptionPlanClubs",
                column: "ClubId",
                principalTable: "Clubs",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AssignedSubscriptionPlanClubs_SubscriptionPlans_SubscriptionPlanId",
                table: "AssignedSubscriptionPlanClubs",
                column: "SubscriptionPlanId",
                principalTable: "SubscriptionPlans",
                principalColumn: "Id");

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

            migrationBuilder.AddForeignKey(
                name: "FK_SubscriptionPlanVisibilities_Role_RoleId",
                table: "SubscriptionPlanVisibilities",
                column: "RoleId",
                principalTable: "Role",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_SubscriptionPlanVisibilities_SubscriptionPlans_SubscriptionPlanId",
                table: "SubscriptionPlanVisibilities",
                column: "SubscriptionPlanId",
                principalTable: "SubscriptionPlans",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AssignedSubscriptionPlanClubs_Clubs_ClubId",
                table: "AssignedSubscriptionPlanClubs");

            migrationBuilder.DropForeignKey(
                name: "FK_AssignedSubscriptionPlanClubs_SubscriptionPlans_SubscriptionPlanId",
                table: "AssignedSubscriptionPlanClubs");

            migrationBuilder.DropForeignKey(
                name: "FK_AvailableSubscriptionPlanClubs_Clubs_ClubId",
                table: "AvailableSubscriptionPlanClubs");

            migrationBuilder.DropForeignKey(
                name: "FK_AvailableSubscriptionPlanClubs_SubscriptionPlans_SubscriptionPlanId",
                table: "AvailableSubscriptionPlanClubs");

            migrationBuilder.DropForeignKey(
                name: "FK_SubscriptionPlanVisibilities_Role_RoleId",
                table: "SubscriptionPlanVisibilities");

            migrationBuilder.DropForeignKey(
                name: "FK_SubscriptionPlanVisibilities_SubscriptionPlans_SubscriptionPlanId",
                table: "SubscriptionPlanVisibilities");

            migrationBuilder.DropTable(
                name: "SubscriptionPlanTags");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Tags",
                table: "Tags");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SubscriptionPlanVisibilities",
                table: "SubscriptionPlanVisibilities");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AvailableSubscriptionPlanClubs",
                table: "AvailableSubscriptionPlanClubs");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AssignedSubscriptionPlanClubs",
                table: "AssignedSubscriptionPlanClubs");

            migrationBuilder.RenameTable(
                name: "Tags",
                newName: "Tag");

            migrationBuilder.RenameTable(
                name: "SubscriptionPlanVisibilities",
                newName: "SubscriptionPlanVisibility");

            migrationBuilder.RenameTable(
                name: "AvailableSubscriptionPlanClubs",
                newName: "AvailableSubscriptionPlanClub");

            migrationBuilder.RenameTable(
                name: "AssignedSubscriptionPlanClubs",
                newName: "AssignedSubscriptionPlanClub");

            migrationBuilder.RenameIndex(
                name: "IX_SubscriptionPlanVisibilities_RoleId",
                table: "SubscriptionPlanVisibility",
                newName: "IX_SubscriptionPlanVisibility_RoleId");

            migrationBuilder.RenameIndex(
                name: "IX_AvailableSubscriptionPlanClubs_ClubId",
                table: "AvailableSubscriptionPlanClub",
                newName: "IX_AvailableSubscriptionPlanClub_ClubId");

            migrationBuilder.RenameIndex(
                name: "IX_AssignedSubscriptionPlanClubs_ClubId",
                table: "AssignedSubscriptionPlanClub",
                newName: "IX_AssignedSubscriptionPlanClub_ClubId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Tag",
                table: "Tag",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SubscriptionPlanVisibility",
                table: "SubscriptionPlanVisibility",
                columns: new[] { "SubscriptionPlanId", "RoleId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_AvailableSubscriptionPlanClub",
                table: "AvailableSubscriptionPlanClub",
                columns: new[] { "SubscriptionPlanId", "ClubId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_AssignedSubscriptionPlanClub",
                table: "AssignedSubscriptionPlanClub",
                columns: new[] { "SubscriptionPlanId", "ClubId" });

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
                name: "IX_SubscriptionPlanTag_TagsId",
                table: "SubscriptionPlanTag",
                column: "TagsId");

            migrationBuilder.AddForeignKey(
                name: "FK_AssignedSubscriptionPlanClub_Clubs_ClubId",
                table: "AssignedSubscriptionPlanClub",
                column: "ClubId",
                principalTable: "Clubs",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AssignedSubscriptionPlanClub_SubscriptionPlans_SubscriptionPlanId",
                table: "AssignedSubscriptionPlanClub",
                column: "SubscriptionPlanId",
                principalTable: "SubscriptionPlans",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AvailableSubscriptionPlanClub_Clubs_ClubId",
                table: "AvailableSubscriptionPlanClub",
                column: "ClubId",
                principalTable: "Clubs",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AvailableSubscriptionPlanClub_SubscriptionPlans_SubscriptionPlanId",
                table: "AvailableSubscriptionPlanClub",
                column: "SubscriptionPlanId",
                principalTable: "SubscriptionPlans",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_SubscriptionPlanVisibility_Role_RoleId",
                table: "SubscriptionPlanVisibility",
                column: "RoleId",
                principalTable: "Role",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_SubscriptionPlanVisibility_SubscriptionPlans_SubscriptionPlanId",
                table: "SubscriptionPlanVisibility",
                column: "SubscriptionPlanId",
                principalTable: "SubscriptionPlans",
                principalColumn: "Id");
        }
    }
}
