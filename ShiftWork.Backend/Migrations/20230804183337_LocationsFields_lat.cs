using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShiftWork.Backend.Migrations
{
    public partial class LocationsFields_lat : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CityCode",
                table: "Locations",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "CountryCode",
                table: "Locations",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Latitude",
                table: "Locations",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Longitude",
                table: "Locations",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Ration",
                table: "Locations",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "StateCode",
                table: "Locations",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Zipcode",
                table: "Locations",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CityCode",
                table: "Locations");

            migrationBuilder.DropColumn(
                name: "CountryCode",
                table: "Locations");

            migrationBuilder.DropColumn(
                name: "Latitude",
                table: "Locations");

            migrationBuilder.DropColumn(
                name: "Longitude",
                table: "Locations");

            migrationBuilder.DropColumn(
                name: "Ration",
                table: "Locations");

            migrationBuilder.DropColumn(
                name: "StateCode",
                table: "Locations");

            migrationBuilder.DropColumn(
                name: "Zipcode",
                table: "Locations");
        }
    }
}
