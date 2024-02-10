using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Fitness.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class EmployeeAddToAllClubs : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "AvaialbleInNewClubs",
                table: "Employees",
                newName: "AvaialbleToNewClubs");

            migrationBuilder.AddColumn<bool>(
                name: "AddToAllClubs",
                table: "Employees",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AddToAllClubs",
                table: "Employees");

            migrationBuilder.RenameColumn(
                name: "AvaialbleToNewClubs",
                table: "Employees",
                newName: "AvaialbleInNewClubs");
        }
    }
}
