using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Fitness.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class SubscriptionPlanDiscountOnlyForChoosenApplications : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "OnlyForChoosenSources",
                table: "SubscriptionPlanDiscounts",
                newName: "OnlyForChoosenApplications");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "OnlyForChoosenApplications",
                table: "SubscriptionPlanDiscounts",
                newName: "OnlyForChoosenSources");
        }
    }
}
