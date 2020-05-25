using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace NewEggCrawler.Migrations
{
    public partial class AddedByteArrayImage : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte[]>(
                name: "Image",
                table: "WaterCoolers",
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "Image",
                table: "VideoCards",
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "Image",
                table: "SolidStateDrives",
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "Image",
                table: "PSUs",
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "Image",
                table: "Motherboards",
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "Image",
                table: "Memories",
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "Image",
                table: "HardDiskDrives",
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "Image",
                table: "Cpus",
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "Image",
                table: "Cases",
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "Image",
                table: "AirCoolers",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Image",
                table: "WaterCoolers");

            migrationBuilder.DropColumn(
                name: "Image",
                table: "VideoCards");

            migrationBuilder.DropColumn(
                name: "Image",
                table: "SolidStateDrives");

            migrationBuilder.DropColumn(
                name: "Image",
                table: "PSUs");

            migrationBuilder.DropColumn(
                name: "Image",
                table: "Motherboards");

            migrationBuilder.DropColumn(
                name: "Image",
                table: "Memories");

            migrationBuilder.DropColumn(
                name: "Image",
                table: "HardDiskDrives");

            migrationBuilder.DropColumn(
                name: "Image",
                table: "Cpus");

            migrationBuilder.DropColumn(
                name: "Image",
                table: "Cases");

            migrationBuilder.DropColumn(
                name: "Image",
                table: "AirCoolers");
        }
    }
}
