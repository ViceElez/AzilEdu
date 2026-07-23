using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AzilEdu.Api.Migrations
{
    /// <inheritdoc />
    public partial class ChangeEmployeesStatusSetName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employees_EmployeeTypes_EmployeePositionId",
                table: "Employees");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EmployeeTypes",
                table: "EmployeeTypes");

            migrationBuilder.RenameTable(
                name: "EmployeeTypes",
                newName: "EmployeePositions");

            migrationBuilder.AddPrimaryKey(
                name: "PK_EmployeePositions",
                table: "EmployeePositions",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_EmployeePositions_EmployeePositionId",
                table: "Employees",
                column: "EmployeePositionId",
                principalTable: "EmployeePositions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employees_EmployeePositions_EmployeePositionId",
                table: "Employees");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EmployeePositions",
                table: "EmployeePositions");

            migrationBuilder.RenameTable(
                name: "EmployeePositions",
                newName: "EmployeeTypes");

            migrationBuilder.AddPrimaryKey(
                name: "PK_EmployeeTypes",
                table: "EmployeeTypes",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_EmployeeTypes_EmployeePositionId",
                table: "Employees",
                column: "EmployeePositionId",
                principalTable: "EmployeeTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
