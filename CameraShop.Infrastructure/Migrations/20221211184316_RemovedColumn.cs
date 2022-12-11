using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TechRentingSystem.Migrations
{
    public partial class RemovedColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Cameras");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "Cameras",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
