using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ERP.DataAccess.Migrations
{
    public partial class ChangingForeingKeybyEmailInDayOffTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DaysOff_Employees_EmployeeId",
                table: "DaysOff");

            migrationBuilder.DropIndex(
                name: "IX_DaysOff_EmployeeId",
                table: "DaysOff");

            migrationBuilder.DropColumn(
                name: "EmployeeId",
                table: "DaysOff");

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "DaysOff",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Email",
                table: "DaysOff");

            migrationBuilder.AddColumn<int>(
                name: "EmployeeId",
                table: "DaysOff",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_DaysOff_EmployeeId",
                table: "DaysOff",
                column: "EmployeeId");

            migrationBuilder.AddForeignKey(
                name: "FK_DaysOff_Employees_EmployeeId",
                table: "DaysOff",
                column: "EmployeeId",
                principalTable: "Employees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
