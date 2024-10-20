using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShiftWork.Backend.Migrations
{
    public partial class AddTAsk20241018 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Notification",
                table: "Locations",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "RatioMax",
                table: "Locations",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "ValidateOnSite",
                table: "Locations",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "ValidateRatio",
                table: "Locations",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Notification",
                table: "Locations");

            migrationBuilder.DropColumn(
                name: "RatioMax",
                table: "Locations");

            migrationBuilder.DropColumn(
                name: "ValidateOnSite",
                table: "Locations");

            migrationBuilder.DropColumn(
                name: "ValidateRatio",
                table: "Locations");
        }
    }
}
