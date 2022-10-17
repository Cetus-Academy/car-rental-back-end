using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CarRentalAPI.Migrations
{
    public partial class CarSeederv1_1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cars_CarBrand_CarBrandId",
                table: "Cars");

            migrationBuilder.AlterColumn<int>(
                name: "CarBrandId",
                table: "Cars",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Cars_CarBrand_CarBrandId",
                table: "Cars",
                column: "CarBrandId",
                principalTable: "CarBrand",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cars_CarBrand_CarBrandId",
                table: "Cars");

            migrationBuilder.AlterColumn<int>(
                name: "CarBrandId",
                table: "Cars",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Cars_CarBrand_CarBrandId",
                table: "Cars",
                column: "CarBrandId",
                principalTable: "CarBrand",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
