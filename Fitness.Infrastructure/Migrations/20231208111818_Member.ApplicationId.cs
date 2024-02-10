using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Fitness.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class MemberApplicationId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ApplicationId",
                table: "Members",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Members_ApplicationId",
                table: "Members",
                column: "ApplicationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Members_Applications_ApplicationId",
                table: "Members",
                column: "ApplicationId",
                principalTable: "Applications",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Members_Applications_ApplicationId",
                table: "Members");

            migrationBuilder.DropIndex(
                name: "IX_Members_ApplicationId",
                table: "Members");

            migrationBuilder.DropColumn(
                name: "ApplicationId",
                table: "Members");
        }
    }
}
