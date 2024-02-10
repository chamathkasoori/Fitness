using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Fitness.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddPaymentMethodCol : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SubscriptionPlanPaymentMethod_Clubs_PaymentMethodId",
                table: "SubscriptionPlanPaymentMethod");

            migrationBuilder.DropForeignKey(
                name: "FK_SubscriptionPlanPaymentMethod_SubscriptionPlans_SubscriptionPlanId",
                table: "SubscriptionPlanPaymentMethod");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SubscriptionPlanPaymentMethod",
                table: "SubscriptionPlanPaymentMethod");

            migrationBuilder.RenameTable(
                name: "SubscriptionPlanPaymentMethod",
                newName: "SubscriptionPlanPaymentMethods");

            migrationBuilder.RenameIndex(
                name: "IX_SubscriptionPlanPaymentMethod_PaymentMethodId",
                table: "SubscriptionPlanPaymentMethods",
                newName: "IX_SubscriptionPlanPaymentMethods_PaymentMethodId");

            migrationBuilder.AlterColumn<int>(
                name: "ClubId",
                table: "Member",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_SubscriptionPlanPaymentMethods",
                table: "SubscriptionPlanPaymentMethods",
                columns: new[] { "SubscriptionPlanId", "PaymentMethodId" });

            migrationBuilder.CreateTable(
                name: "PaymentMethod",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
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
                    table.PrimaryKey("PK_PaymentMethod", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserRoles_UserId",
                table: "UserRoles",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProductCategories_IconId",
                table: "ProductCategories",
                column: "IconId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductCategories_Icons_IconId",
                table: "ProductCategories",
                column: "IconId",
                principalTable: "Icons",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_SubscriptionPlanPaymentMethods_PaymentMethod_PaymentMethodId",
                table: "SubscriptionPlanPaymentMethods",
                column: "PaymentMethodId",
                principalTable: "PaymentMethod",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_SubscriptionPlanPaymentMethods_SubscriptionPlans_SubscriptionPlanId",
                table: "SubscriptionPlanPaymentMethods",
                column: "SubscriptionPlanId",
                principalTable: "SubscriptionPlans",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductCategories_Icons_IconId",
                table: "ProductCategories");

            migrationBuilder.DropForeignKey(
                name: "FK_SubscriptionPlanPaymentMethods_PaymentMethod_PaymentMethodId",
                table: "SubscriptionPlanPaymentMethods");

            migrationBuilder.DropForeignKey(
                name: "FK_SubscriptionPlanPaymentMethods_SubscriptionPlans_SubscriptionPlanId",
                table: "SubscriptionPlanPaymentMethods");

            migrationBuilder.DropTable(
                name: "PaymentMethod");

            migrationBuilder.DropIndex(
                name: "IX_UserRoles_UserId",
                table: "UserRoles");

            migrationBuilder.DropIndex(
                name: "IX_ProductCategories_IconId",
                table: "ProductCategories");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SubscriptionPlanPaymentMethods",
                table: "SubscriptionPlanPaymentMethods");

            migrationBuilder.RenameTable(
                name: "SubscriptionPlanPaymentMethods",
                newName: "SubscriptionPlanPaymentMethod");

            migrationBuilder.RenameIndex(
                name: "IX_SubscriptionPlanPaymentMethods_PaymentMethodId",
                table: "SubscriptionPlanPaymentMethod",
                newName: "IX_SubscriptionPlanPaymentMethod_PaymentMethodId");

            migrationBuilder.AlterColumn<int>(
                name: "ClubId",
                table: "Member",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SubscriptionPlanPaymentMethod",
                table: "SubscriptionPlanPaymentMethod",
                columns: new[] { "SubscriptionPlanId", "PaymentMethodId" });

            migrationBuilder.AddForeignKey(
                name: "FK_SubscriptionPlanPaymentMethod_Clubs_PaymentMethodId",
                table: "SubscriptionPlanPaymentMethod",
                column: "PaymentMethodId",
                principalTable: "Clubs",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_SubscriptionPlanPaymentMethod_SubscriptionPlans_SubscriptionPlanId",
                table: "SubscriptionPlanPaymentMethod",
                column: "SubscriptionPlanId",
                principalTable: "SubscriptionPlans",
                principalColumn: "Id");
        }
    }
}
