using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Fitness.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class MemberOtp2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MemberOtp_Member_MemberId",
                table: "MemberOtp");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MemberOtp",
                table: "MemberOtp");

            migrationBuilder.RenameTable(
                name: "MemberOtp",
                newName: "MemberOtps");

            migrationBuilder.RenameIndex(
                name: "IX_MemberOtp_MemberId",
                table: "MemberOtps",
                newName: "IX_MemberOtps_MemberId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MemberOtps",
                table: "MemberOtps",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_MemberOtps_Member_MemberId",
                table: "MemberOtps",
                column: "MemberId",
                principalTable: "Member",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MemberOtps_Member_MemberId",
                table: "MemberOtps");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MemberOtps",
                table: "MemberOtps");

            migrationBuilder.RenameTable(
                name: "MemberOtps",
                newName: "MemberOtp");

            migrationBuilder.RenameIndex(
                name: "IX_MemberOtps_MemberId",
                table: "MemberOtp",
                newName: "IX_MemberOtp_MemberId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MemberOtp",
                table: "MemberOtp",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_MemberOtp_Member_MemberId",
                table: "MemberOtp",
                column: "MemberId",
                principalTable: "Member",
                principalColumn: "Id");
        }
    }
}
