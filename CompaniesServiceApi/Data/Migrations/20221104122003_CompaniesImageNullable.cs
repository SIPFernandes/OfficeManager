using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CompaniesServiceApi.Data.Migrations
{
    public partial class CompaniesImageNullable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Company_ImageId",
                table: "Company");

            migrationBuilder.AlterColumn<int>(
                name: "ImageId",
                table: "Company",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_Company_ImageId",
                table: "Company",
                column: "ImageId",
                unique: true,
                filter: "[ImageId] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Company_ImageId",
                table: "Company");

            migrationBuilder.AlterColumn<int>(
                name: "ImageId",
                table: "Company",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Company_ImageId",
                table: "Company",
                column: "ImageId",
                unique: true);
        }
    }
}
