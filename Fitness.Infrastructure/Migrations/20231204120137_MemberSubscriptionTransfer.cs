using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Fitness.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class MemberSubscriptionTransfer : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MemberSubscriptionTransfers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OldMemberSubscriptionId = table.Column<int>(type: "int", nullable: false),
                    NewMemberSubscriptionId = table.Column<int>(type: "int", nullable: false),
                    NoOfDays = table.Column<int>(type: "int", nullable: false),
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
                    table.PrimaryKey("PK_MemberSubscriptionTransfers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MemberSubscriptionTransfers_MemberSubscriptions_NewMemberSubscriptionId",
                        column: x => x.NewMemberSubscriptionId,
                        principalTable: "MemberSubscriptions",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_MemberSubscriptionTransfers_MemberSubscriptions_OldMemberSubscriptionId",
                        column: x => x.OldMemberSubscriptionId,
                        principalTable: "MemberSubscriptions",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_MemberSubscriptionTransfers_NewMemberSubscriptionId",
                table: "MemberSubscriptionTransfers",
                column: "NewMemberSubscriptionId");

            migrationBuilder.CreateIndex(
                name: "IX_MemberSubscriptionTransfers_OldMemberSubscriptionId",
                table: "MemberSubscriptionTransfers",
                column: "OldMemberSubscriptionId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MemberSubscriptionTransfers");
        }
    }
}
