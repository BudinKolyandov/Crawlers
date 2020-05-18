using Microsoft.EntityFrameworkCore.Migrations;

namespace NewEggCrawler.Migrations
{
    public partial class AddedCpusAndMemories : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Cpus",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    ImgUrl = table.Column<string>(nullable: true),
                    Brand = table.Column<string>(nullable: true),
                    ProcesorType = table.Column<string>(nullable: true),
                    Series = table.Column<string>(nullable: true),
                    Model = table.Column<string>(nullable: true),
                    CPUSocketType = table.Column<string>(nullable: true),
                    NumberOfCores = table.Column<string>(nullable: true),
                    NumberOfThreads = table.Column<string>(nullable: true),
                    ManufacturingTech = table.Column<string>(nullable: true),
                    TDP = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cpus", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Memories",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    ImgUrl = table.Column<string>(nullable: true),
                    Brand = table.Column<string>(nullable: true),
                    Capacity = table.Column<string>(nullable: true),
                    Series = table.Column<string>(nullable: true),
                    Model = table.Column<string>(nullable: true),
                    Type = table.Column<string>(nullable: true),
                    Speed = table.Column<string>(nullable: true),
                    CASLatency = table.Column<string>(nullable: true),
                    Timing = table.Column<string>(nullable: true),
                    HeatSpreader = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Memories", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Cpus");

            migrationBuilder.DropTable(
                name: "Memories");
        }
    }
}
