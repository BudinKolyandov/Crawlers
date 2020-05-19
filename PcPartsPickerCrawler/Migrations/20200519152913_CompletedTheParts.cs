using Microsoft.EntityFrameworkCore.Migrations;

namespace NewEggCrawler.Migrations
{
    public partial class CompletedTheParts : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AirCoolers",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    ImgUrl = table.Column<string>(nullable: true),
                    Brand = table.Column<string>(nullable: true),
                    FanSize = table.Column<string>(nullable: true),
                    CPUSocketCompatibility = table.Column<string>(nullable: true),
                    RPM = table.Column<string>(nullable: true),
                    PowerConnector = table.Column<string>(nullable: true),
                    MaxCPUCoolerHeight = table.Column<string>(nullable: true),
                    HeatsinkDimensions = table.Column<string>(nullable: true),
                    Weight = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AirCoolers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Cases",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    ImgUrl = table.Column<string>(nullable: true),
                    Brand = table.Column<string>(nullable: true),
                    Type = table.Column<string>(nullable: true),
                    Color = table.Column<string>(nullable: true),
                    CaseMaterial = table.Column<string>(nullable: true),
                    MotherboardCompatibility = table.Column<string>(nullable: true),
                    MaxGPULengthAllowance = table.Column<string>(nullable: true),
                    MaxCPUCoolerHeightAllowance = table.Column<string>(nullable: true),
                    SidePanelWindow = table.Column<string>(nullable: true),
                    ExpansionSlots = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cases", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "HardDiskDrives",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    ImgUrl = table.Column<string>(nullable: true),
                    Brand = table.Column<string>(nullable: true),
                    Interface = table.Column<string>(nullable: true),
                    Capacity = table.Column<string>(nullable: true),
                    RPM = table.Column<string>(nullable: true),
                    Cache = table.Column<string>(nullable: true),
                    Usage = table.Column<string>(nullable: true),
                    FormFactor = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HardDiskDrives", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Motherboards",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    ImgUrl = table.Column<string>(nullable: true),
                    Brand = table.Column<string>(nullable: true),
                    Model = table.Column<string>(nullable: true),
                    CPUSocketType = table.Column<string>(nullable: true),
                    CPUType = table.Column<string>(nullable: true),
                    Chipset = table.Column<string>(nullable: true),
                    MemoryStandard = table.Column<string>(nullable: true),
                    NumberOfMemorySlots = table.Column<string>(nullable: true),
                    AudioChipset = table.Column<string>(nullable: true),
                    LANChipset = table.Column<string>(nullable: true),
                    MaxLANSpeed = table.Column<string>(nullable: true),
                    FormFactor = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Motherboards", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PSUs",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    ImgUrl = table.Column<string>(nullable: true),
                    Brand = table.Column<string>(nullable: true),
                    Type = table.Column<string>(nullable: true),
                    MaximumPower = table.Column<string>(nullable: true),
                    Fans = table.Column<string>(nullable: true),
                    Modular = table.Column<string>(nullable: true),
                    EnergyEfficiency = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PSUs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SolidStateDrives",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    ImgUrl = table.Column<string>(nullable: true),
                    Brand = table.Column<string>(nullable: true),
                    DeviceType = table.Column<string>(nullable: true),
                    UsedFor = table.Column<string>(nullable: true),
                    FormFactor = table.Column<string>(nullable: true),
                    Capacity = table.Column<string>(nullable: true),
                    Interface = table.Column<string>(nullable: true),
                    MemoryComponents = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SolidStateDrives", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "VideoCards",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    ImgUrl = table.Column<string>(nullable: true),
                    Brand = table.Column<string>(nullable: true),
                    Model = table.Column<string>(nullable: true),
                    Interface = table.Column<string>(nullable: true),
                    GPU = table.Column<string>(nullable: true),
                    StreamProcessors = table.Column<string>(nullable: true),
                    DirectX = table.Column<string>(nullable: true),
                    OpenGL = table.Column<string>(nullable: true),
                    Cooler = table.Column<string>(nullable: true),
                    SystemRequirements = table.Column<string>(nullable: true),
                    FormFactor = table.Column<string>(nullable: true),
                    MaxGPULength = table.Column<string>(nullable: true),
                    SlotWidth = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VideoCards", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "WaterCoolers",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    ImgUrl = table.Column<string>(nullable: true),
                    Brand = table.Column<string>(nullable: true),
                    Type = table.Column<string>(nullable: true),
                    BlockCompatibility = table.Column<string>(nullable: true),
                    PumpCapacity = table.Column<string>(nullable: true),
                    RadiatorDim = table.Column<string>(nullable: true),
                    FanSize = table.Column<string>(nullable: true),
                    FanRPM = table.Column<string>(nullable: true),
                    FanAirFlow = table.Column<string>(nullable: true),
                    TubeDim = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WaterCoolers", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AirCoolers");

            migrationBuilder.DropTable(
                name: "Cases");

            migrationBuilder.DropTable(
                name: "HardDiskDrives");

            migrationBuilder.DropTable(
                name: "Motherboards");

            migrationBuilder.DropTable(
                name: "PSUs");

            migrationBuilder.DropTable(
                name: "SolidStateDrives");

            migrationBuilder.DropTable(
                name: "VideoCards");

            migrationBuilder.DropTable(
                name: "WaterCoolers");
        }
    }
}
