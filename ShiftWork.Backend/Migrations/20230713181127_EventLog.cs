using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShiftWork.Backend.Migrations
{
    public partial class EventLog : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AvatarImageIn",
                table: "ScheduleShifts",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "AvatarImageOut",
                table: "ScheduleShifts",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "AvatarImage",
                table: "Person",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "MainAddreess",
                table: "Person",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PersonConfig",
                table: "Person",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PrivateKey",
                table: "Person",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "LocationAddress",
                table: "Locations",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "LocationConfig",
                table: "Locations",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AvatarImageIn",
                table: "ScheduleShifts");

            migrationBuilder.DropColumn(
                name: "AvatarImageOut",
                table: "ScheduleShifts");

            migrationBuilder.DropColumn(
                name: "AvatarImage",
                table: "Person");

            migrationBuilder.DropColumn(
                name: "MainAddreess",
                table: "Person");

            migrationBuilder.DropColumn(
                name: "PersonConfig",
                table: "Person");

            migrationBuilder.DropColumn(
                name: "PrivateKey",
                table: "Person");

            migrationBuilder.DropColumn(
                name: "LocationAddress",
                table: "Locations");

            migrationBuilder.DropColumn(
                name: "LocationConfig",
                table: "Locations");
        }
    }
}
