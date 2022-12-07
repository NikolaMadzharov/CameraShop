using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TechRentingSystem.Migrations
{
    public partial class addedIsDeletedToCamera : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "Cameras",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Cameras");
        }
    }
}
