using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CompaniesServiceApi.Data.Migrations
{
    public partial class AddCoordinatesToSeat : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ChairCoordinate",
                table: "Seat");

            migrationBuilder.DropColumn(
                name: "ChairNumber",
                table: "Seat");

            migrationBuilder.RenameColumn(
                name: "TableNumber",
                table: "Seat",
                newName: "CoordinateY");

            migrationBuilder.RenameColumn(
                name: "TableCoordinate",
                table: "Seat",
                newName: "CoordinateX");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CoordinateY",
                table: "Seat",
                newName: "TableNumber");

            migrationBuilder.RenameColumn(
                name: "CoordinateX",
                table: "Seat",
                newName: "TableCoordinate");

            migrationBuilder.AddColumn<int>(
                name: "ChairCoordinate",
                table: "Seat",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ChairNumber",
                table: "Seat",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
