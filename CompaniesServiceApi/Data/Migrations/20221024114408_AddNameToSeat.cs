using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CompaniesServiceApi.Data.Migrations
{
    public partial class AddNameToSeat : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Seat",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "Seat");
        }
    }
}
