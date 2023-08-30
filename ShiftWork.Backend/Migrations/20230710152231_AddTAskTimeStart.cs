using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShiftWork.Backend.Migrations
{
    public partial class AddTAskTimeStart : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CreatedDate",
                table: "TaskShifts",
                newName: "TaskDate");

            migrationBuilder.AddColumn<DateTime>(
                name: "EndTime",
                table: "TaskShifts",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "StartTime",
                table: "TaskShifts",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "IsApproved",
                table: "ScheduleShifts",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsApproved",
                table: "Schedules",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EndTime",
                table: "TaskShifts");

            migrationBuilder.DropColumn(
                name: "StartTime",
                table: "TaskShifts");

            migrationBuilder.DropColumn(
                name: "IsApproved",
                table: "ScheduleShifts");

            migrationBuilder.DropColumn(
                name: "IsApproved",
                table: "Schedules");

            migrationBuilder.RenameColumn(
                name: "TaskDate",
                table: "TaskShifts",
                newName: "CreatedDate");
        }
    }
}
