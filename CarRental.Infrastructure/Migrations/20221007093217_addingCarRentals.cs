using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CarRentalAPI.Migrations
{
    public partial class addingCarRentals : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CarRentalId",
                table: "Cars",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "CarRentals",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Street = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PostalCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PricePerDay = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CarRentals", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Cars_CarRentalId",
                table: "Cars",
                column: "CarRentalId");

            migrationBuilder.AddForeignKey(
                name: "FK_Cars_CarRentals_CarRentalId",
                table: "Cars",
                column: "CarRentalId",
                principalTable: "CarRentals",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cars_CarRentals_CarRentalId",
                table: "Cars");

            migrationBuilder.DropTable(
                name: "CarRentals");

            migrationBuilder.DropIndex(
                name: "IX_Cars_CarRentalId",
                table: "Cars");

            migrationBuilder.DropColumn(
                name: "CarRentalId",
                table: "Cars");
        }
    }
}
