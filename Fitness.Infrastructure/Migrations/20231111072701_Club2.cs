using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Fitness.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Club2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Frozen",
                table: "Clubs");

            migrationBuilder.RenameColumn(
                name: "Status",
                table: "Clubs",
                newName: "AccountingNumber");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "AccountingNumber",
                table: "Clubs",
                newName: "Status");

            migrationBuilder.AddColumn<string>(
                name: "Frozen",
                table: "Clubs",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
