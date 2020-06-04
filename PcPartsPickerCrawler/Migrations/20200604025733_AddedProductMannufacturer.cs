using Microsoft.EntityFrameworkCore.Migrations;

namespace NewEggCrawler.Migrations
{
    public partial class AddedProductMannufacturer : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ProductUrl",
                table: "WaterCoolers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ProductUrl",
                table: "VideoCards",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ProductUrl",
                table: "SolidStateDrives",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ProductUrl",
                table: "PSUs",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ProductUrl",
                table: "Motherboards",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ProductUrl",
                table: "Memories",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ProductUrl",
                table: "HardDiskDrives",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ProductUrl",
                table: "Cpus",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ProductUrl",
                table: "Cases",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ProductUrl",
                table: "AirCoolers",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProductUrl",
                table: "WaterCoolers");

            migrationBuilder.DropColumn(
                name: "ProductUrl",
                table: "VideoCards");

            migrationBuilder.DropColumn(
                name: "ProductUrl",
                table: "SolidStateDrives");

            migrationBuilder.DropColumn(
                name: "ProductUrl",
                table: "PSUs");

            migrationBuilder.DropColumn(
                name: "ProductUrl",
                table: "Motherboards");

            migrationBuilder.DropColumn(
                name: "ProductUrl",
                table: "Memories");

            migrationBuilder.DropColumn(
                name: "ProductUrl",
                table: "HardDiskDrives");

            migrationBuilder.DropColumn(
                name: "ProductUrl",
                table: "Cpus");

            migrationBuilder.DropColumn(
                name: "ProductUrl",
                table: "Cases");

            migrationBuilder.DropColumn(
                name: "ProductUrl",
                table: "AirCoolers");
        }
    }
}
