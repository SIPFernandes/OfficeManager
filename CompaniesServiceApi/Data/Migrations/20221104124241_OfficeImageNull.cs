using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CompaniesServiceApi.Data.Migrations
{
    public partial class OfficeImageNull : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Office_ImageId",
                table: "Office");

            migrationBuilder.AlterColumn<int>(
                name: "ImageId",
                table: "Office",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_Office_ImageId",
                table: "Office",
                column: "ImageId",
                unique: true,
                filter: "[ImageId] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Office_ImageId",
                table: "Office");

            migrationBuilder.AlterColumn<int>(
                name: "ImageId",
                table: "Office",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Office_ImageId",
                table: "Office",
                column: "ImageId",
                unique: true);
        }
    }
}
