using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace AzilEdu.Api.Migrations
{
    /// <inheritdoc />
    public partial class NewEmployeeStatuses : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employees_EmployeeStatuses_EmployeeStatusId",
                table: "Employees");

            migrationBuilder.DropForeignKey(
                name: "FK_Employees_EmployeeTypes_EmployeePositionId",
                table: "Employees");

            migrationBuilder.InsertData(
                table: "EmployeeStatuses",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Aktivan" },
                    { 2, "Na dopustu ili bolovanju" },
                    { 3, "Neaktivan" }
                });

            migrationBuilder.InsertData(
                table: "EmployeeTypes",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Djelatnik azila" },
                    { 2, "Veterinar" },
                    { 3, "Koordinator volontera" },
                    { 4, "Administrator" }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_EmployeeStatuses_EmployeeStatusId",
                table: "Employees",
                column: "EmployeeStatusId",
                principalTable: "EmployeeStatuses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_EmployeeTypes_EmployeePositionId",
                table: "Employees",
                column: "EmployeePositionId",
                principalTable: "EmployeeTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employees_EmployeeStatuses_EmployeeStatusId",
                table: "Employees");

            migrationBuilder.DropForeignKey(
                name: "FK_Employees_EmployeeTypes_EmployeePositionId",
                table: "Employees");

            // ✅ NOVO: Prvo postavi NULL prije brisanja seed podataka
            migrationBuilder.Sql("UPDATE Employees SET EmployeeStatusId = NULL WHERE EmployeeStatusId IN (1, 2, 3)");
            migrationBuilder.Sql("UPDATE Employees SET EmployeePositionId = NULL WHERE EmployeePositionId IN (1, 2, 3, 4)");

            migrationBuilder.DeleteData(
                table: "EmployeeStatuses",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "EmployeeStatuses",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "EmployeeStatuses",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "EmployeeTypes",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "EmployeeTypes",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "EmployeeTypes",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "EmployeeTypes",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_EmployeeStatuses_EmployeeStatusId",
                table: "Employees",
                column: "EmployeeStatusId",
                principalTable: "EmployeeStatuses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_EmployeeTypes_EmployeePositionId",
                table: "Employees",
                column: "EmployeePositionId",
                principalTable: "EmployeeTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
