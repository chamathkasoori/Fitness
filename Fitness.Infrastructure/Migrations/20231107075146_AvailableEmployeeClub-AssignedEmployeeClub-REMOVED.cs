using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Fitness.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AvailableEmployeeClubAssignedEmployeeClubREMOVED : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AssignedEmployeeClubs");

            migrationBuilder.DropTable(
                name: "AvailableEmployeeClubs");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AssignedEmployeeClubs",
                columns: table => new
                {
                    EmployeeId = table.Column<int>(type: "int", nullable: false),
                    ClubId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AssignedEmployeeClubs", x => new { x.EmployeeId, x.ClubId });
                    table.ForeignKey(
                        name: "FK_AssignedEmployeeClubs_Clubs_ClubId",
                        column: x => x.ClubId,
                        principalTable: "Clubs",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AssignedEmployeeClubs_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "AvailableEmployeeClubs",
                columns: table => new
                {
                    EmployeeId = table.Column<int>(type: "int", nullable: false),
                    ClubId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AvailableEmployeeClubs", x => new { x.EmployeeId, x.ClubId });
                    table.ForeignKey(
                        name: "FK_AvailableEmployeeClubs_Clubs_ClubId",
                        column: x => x.ClubId,
                        principalTable: "Clubs",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AvailableEmployeeClubs_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_AssignedEmployeeClubs_ClubId",
                table: "AssignedEmployeeClubs",
                column: "ClubId");

            migrationBuilder.CreateIndex(
                name: "IX_AvailableEmployeeClubs_ClubId",
                table: "AvailableEmployeeClubs",
                column: "ClubId");
        }
    }
}
