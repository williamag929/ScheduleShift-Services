using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShiftWork.Backend.Migrations
{
    public partial class LocationsFields : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "MainAddreess",
                table: "Person",
                newName: "MainAddress");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "MainAddress",
                table: "Person",
                newName: "MainAddreess");
        }
    }
}
