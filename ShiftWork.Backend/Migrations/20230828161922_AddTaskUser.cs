using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShiftWork.Backend.Migrations
{
    public partial class AddTaskUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AreaId",
                table: "TaskShifts",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CreatedBy",
                table: "TaskShifts",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "LocationId",
                table: "TaskShifts",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PersonId",
                table: "TaskShifts",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "isSchedule",
                table: "TaskShifts",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "PrimaryPhone",
                table: "Person",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AreaId",
                table: "TaskShifts");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "TaskShifts");

            migrationBuilder.DropColumn(
                name: "LocationId",
                table: "TaskShifts");

            migrationBuilder.DropColumn(
                name: "PersonId",
                table: "TaskShifts");

            migrationBuilder.DropColumn(
                name: "isSchedule",
                table: "TaskShifts");

            migrationBuilder.DropColumn(
                name: "PrimaryPhone",
                table: "Person");
        }
    }
}
