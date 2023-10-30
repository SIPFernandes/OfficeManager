using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CompaniesServiceApi.Data.Migrations
{
    public partial class CanBookRoom : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "CanBook",
                table: "Room",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CanBook",
                table: "Room");
        }
    }
}
