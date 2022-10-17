using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CarRentalAPI.Migrations
{
    public partial class CarSeeder : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CarBrandId",
                table: "Cars",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "CarCondition",
                table: "Cars",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Color",
                table: "Cars",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CountryOfOrigin",
                table: "Cars",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Cars",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Displacement",
                table: "Cars",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Drive",
                table: "Cars",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Fuel",
                table: "Cars",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "FuelUsage",
                table: "Cars",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<string>(
                name: "Generate",
                table: "Cars",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Model",
                table: "Cars",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "NumberOfAvailableModels",
                table: "Cars",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "NumberOfDoors",
                table: "Cars",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "NumberOfPlaces",
                table: "Cars",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Power",
                table: "Cars",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "PresentLocation",
                table: "Cars",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PriceCategory",
                table: "Cars",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "YearOfProduction",
                table: "Cars",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "CarAttribute",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CarId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CarAttribute", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CarAttribute_Cars_CarId",
                        column: x => x.CarId,
                        principalTable: "Cars",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "CarBrand",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CarBrand", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CarEquipment",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Icon = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CarId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CarEquipment", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CarEquipment_Cars_CarId",
                        column: x => x.CarId,
                        principalTable: "Cars",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Image",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Src = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Alt = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CarBrandId = table.Column<int>(type: "int", nullable: true),
                    CarId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Image", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Image_CarBrand_CarBrandId",
                        column: x => x.CarBrandId,
                        principalTable: "CarBrand",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Image_Cars_CarId",
                        column: x => x.CarId,
                        principalTable: "Cars",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Cars_CarBrandId",
                table: "Cars",
                column: "CarBrandId");

            migrationBuilder.CreateIndex(
                name: "IX_CarAttribute_CarId",
                table: "CarAttribute",
                column: "CarId");

            migrationBuilder.CreateIndex(
                name: "IX_CarEquipment_CarId",
                table: "CarEquipment",
                column: "CarId");

            migrationBuilder.CreateIndex(
                name: "IX_Image_CarBrandId",
                table: "Image",
                column: "CarBrandId");

            migrationBuilder.CreateIndex(
                name: "IX_Image_CarId",
                table: "Image",
                column: "CarId");

            migrationBuilder.AddForeignKey(
                name: "FK_Cars_CarBrand_CarBrandId",
                table: "Cars",
                column: "CarBrandId",
                principalTable: "CarBrand",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cars_CarBrand_CarBrandId",
                table: "Cars");

            migrationBuilder.DropTable(
                name: "CarAttribute");

            migrationBuilder.DropTable(
                name: "CarEquipment");

            migrationBuilder.DropTable(
                name: "Image");

            migrationBuilder.DropTable(
                name: "CarBrand");

            migrationBuilder.DropIndex(
                name: "IX_Cars_CarBrandId",
                table: "Cars");

            migrationBuilder.DropColumn(
                name: "CarBrandId",
                table: "Cars");

            migrationBuilder.DropColumn(
                name: "CarCondition",
                table: "Cars");

            migrationBuilder.DropColumn(
                name: "Color",
                table: "Cars");

            migrationBuilder.DropColumn(
                name: "CountryOfOrigin",
                table: "Cars");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Cars");

            migrationBuilder.DropColumn(
                name: "Displacement",
                table: "Cars");

            migrationBuilder.DropColumn(
                name: "Drive",
                table: "Cars");

            migrationBuilder.DropColumn(
                name: "Fuel",
                table: "Cars");

            migrationBuilder.DropColumn(
                name: "FuelUsage",
                table: "Cars");

            migrationBuilder.DropColumn(
                name: "Generate",
                table: "Cars");

            migrationBuilder.DropColumn(
                name: "Model",
                table: "Cars");

            migrationBuilder.DropColumn(
                name: "NumberOfAvailableModels",
                table: "Cars");

            migrationBuilder.DropColumn(
                name: "NumberOfDoors",
                table: "Cars");

            migrationBuilder.DropColumn(
                name: "NumberOfPlaces",
                table: "Cars");

            migrationBuilder.DropColumn(
                name: "Power",
                table: "Cars");

            migrationBuilder.DropColumn(
                name: "PresentLocation",
                table: "Cars");

            migrationBuilder.DropColumn(
                name: "PriceCategory",
                table: "Cars");

            migrationBuilder.DropColumn(
                name: "YearOfProduction",
                table: "Cars");
        }
    }
}
