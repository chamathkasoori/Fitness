using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Fitness.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InquiryReplyReplyFromId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ReplyFrom",
                table: "InquiryReplies");

            migrationBuilder.AddColumn<int>(
                name: "ReplyFromId",
                table: "InquiryReplies",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_InquiryReplies_ReplyFromId",
                table: "InquiryReplies",
                column: "ReplyFromId");

            migrationBuilder.AddForeignKey(
                name: "FK_InquiryReplies_Users_ReplyFromId",
                table: "InquiryReplies",
                column: "ReplyFromId",
                principalTable: "Users",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InquiryReplies_Users_ReplyFromId",
                table: "InquiryReplies");

            migrationBuilder.DropIndex(
                name: "IX_InquiryReplies_ReplyFromId",
                table: "InquiryReplies");

            migrationBuilder.DropColumn(
                name: "ReplyFromId",
                table: "InquiryReplies");

            migrationBuilder.AddColumn<string>(
                name: "ReplyFrom",
                table: "InquiryReplies",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
