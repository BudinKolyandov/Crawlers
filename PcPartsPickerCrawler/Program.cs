using AngleSharp.Html.Parser;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using NewEggCrawler;
using NewEggCrawler.Data;
using NewEggCrawler.Data.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace NewEggPartsCrawler
{
    class Program
    {
        static void Main(string[] args)
        {
            // IEnumerable<Cpu> cpuResult;
            // IEnumerable<Memory> memoryResult;
            // IEnumerable<Motherboard> motherboardResult;
            // IEnumerable<VideoCard> videoCardResult;
            // IEnumerable<Case> caseResult;
            // IEnumerable<AirCooler> airCoolerResult;
            // IEnumerable<WaterCooler> waterCoolerResult;
            // IEnumerable<PSU> powerSuppliesResult;
            // IEnumerable<HardDiskDrive> hardDrivesResult;
            // IEnumerable<SolidStateDrive> solidStateDrivesResult;

            // GatherData(out cpuResult, out memoryResult, out motherboardResult, out videoCardResult, out caseResult, out airCoolerResult, out waterCoolerResult, out powerSuppliesResult, out hardDrivesResult, out solidStateDrivesResult);

            // SaveData(cpuResult, memoryResult, motherboardResult, videoCardResult, caseResult, airCoolerResult, waterCoolerResult, powerSuppliesResult, hardDrivesResult, solidStateDrivesResult);

            using (var context = new ApplicationDbContext())
            {
                var cpus = context.Cpus.Where(x => x.Name != " ").ToList();
                Console.WriteLine(cpus.Count);

                var imgArray = TransformImage(cpus[0].ImgUrl).GetAwaiter().GetResult();

                //foreach (var cpu in cpus)
                //{
                //    var imgArray = TransformImage(cpu.ImgUrl);
                //}
            }

            // CreateAndFillExcelTable(cpuResult, memoryResult);

            // Console.WriteLine(cpuResult.Count());
        }

        private static async Task<byte[]> TransformImage(string imgUrl)
        {
            var img = new byte[5000];

            var fullUrl = @"https://" + imgUrl;
            var localPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

            using (var client = new HttpClient())
            {
                using (var response = await client.GetAsync(fullUrl))
                {
                    using (var source = await response.Content.ReadAsStreamAsync())
                    {
                        using (var target = File.OpenWrite(localPath))
                        {
                            await source.CopyToAsync(target);
                        }
                    }
                }
            }

            return img;
        }

        private static void SaveData(IEnumerable<Cpu> cpuResult, IEnumerable<Memory> memoryResult, IEnumerable<Motherboard> motherboardResult, IEnumerable<VideoCard> videoCardResult, IEnumerable<Case> caseResult, IEnumerable<AirCooler> airCoolerResult, IEnumerable<WaterCooler> waterCoolerResult, IEnumerable<PSU> powerSuppliesResult, IEnumerable<HardDiskDrive> hardDrivesResult, IEnumerable<SolidStateDrive> solidStateDrivesResult)
        {
            using (var context = new ApplicationDbContext())
            {
                context.Cpus.AddRange(cpuResult);
                context.Memories.AddRange(memoryResult);
                context.Motherboards.AddRange(motherboardResult);
                context.VideoCards.AddRange(videoCardResult);
                context.Cases.AddRange(caseResult);
                context.AirCoolers.AddRange(airCoolerResult);
                context.WaterCoolers.AddRange(waterCoolerResult);
                context.PSUs.AddRange(powerSuppliesResult);
                context.HardDiskDrives.AddRange(hardDrivesResult);
                context.SolidStateDrives.AddRange(solidStateDrivesResult);
                context.SaveChanges();
            }
        }

        private static void GatherData(out IEnumerable<Cpu> cpuResult, out IEnumerable<Memory> memoryResult, out IEnumerable<Motherboard> motherboardResult, out IEnumerable<VideoCard> videoCardResult, out IEnumerable<Case> caseResult, out IEnumerable<AirCooler> airCoolerResult, out IEnumerable<WaterCooler> waterCoolerResult, out IEnumerable<PSU> powerSuppliesResult, out IEnumerable<HardDiskDrive> hardDrivesResult, out IEnumerable<SolidStateDrive> solidStateDrivesResult)
        {
            cpuResult = new NewEggCpuGatherer().GatherCpuData().GetAwaiter().GetResult();
            memoryResult = new NewEggMemoryGatherer().GatherMemoryData().GetAwaiter().GetResult();
            motherboardResult = new NewEggMotherboardGatherer().GatherMotherboardData().GetAwaiter().GetResult();
            videoCardResult = new NewEggVideoCardGatherer().GatherVideoCardData().GetAwaiter().GetResult();
            caseResult = new NewEggCaseGatherer().GatherVideoCardData().GetAwaiter().GetResult();
            airCoolerResult = new NewEggAirCoolerGatherer().GatherAirCoolerData().GetAwaiter().GetResult();
            waterCoolerResult = new NewEggWaterCoolerGatherer().GatherWaterCoolerData().GetAwaiter().GetResult();
            powerSuppliesResult = new NewEggPowerSupplyGatherer().GatherPowerSuppliesData().GetAwaiter().GetResult();
            hardDrivesResult = new NewEggHardDrivesGatherer().GatherHardDrivesData().GetAwaiter().GetResult();
            solidStateDrivesResult = new NewEggSolidStateDrivesGatherer().GatherSolidStateDrivesData().GetAwaiter().GetResult();
        }

        private static void CreateAndFillExcelTable(IEnumerable<Cpu> cpuResult, IEnumerable<Memory> memoryResult)
        {
            using (var spreadsheetDocument = SpreadsheetDocument.Create(@"E:\DesktopOld\Desktop\Crawlers\ArdesCrawler\PcPartsPickerCrawler\Data\NewEggData.xls", SpreadsheetDocumentType.Workbook))
            {
                WorkbookPart workbookPart;
                WorksheetPart worksheetPart;
                SheetData sheetData;
                Sheets sheets;

                CreateTable(spreadsheetDocument, out workbookPart, out worksheetPart, out sheetData, out sheets);

                DataTable procesorsTable = (DataTable)JsonConvert.DeserializeObject(JsonConvert.SerializeObject(cpuResult), (typeof(DataTable)));
                AddProcessorsToTable(procesorsTable, spreadsheetDocument, worksheetPart, sheetData, sheets);

                DataTable memoriesTable = (DataTable)JsonConvert.DeserializeObject(JsonConvert.SerializeObject(memoryResult), (typeof(DataTable)));
                AddMemoriesToTable(memoriesTable, spreadsheetDocument, worksheetPart, sheetData, sheets);

                workbookPart.Workbook.Save();

                spreadsheetDocument.Close();

                Console.WriteLine($@"Excel file created , you can find the file E:\DesktopOld\Desktop\Crawlers\ArdesCrawler\PcPartsPickerCrawler\Data\NewEggData.xls");
            }
        }

        private static void AddMemoriesToTable(DataTable memoriesTable, SpreadsheetDocument spreadsheetDocument, WorksheetPart worksheetPart, SheetData sheetData, Sheets sheets)
        {
            Sheet memoriesSheet = new Sheet()
            {
                Id = spreadsheetDocument.WorkbookPart.GetIdOfPart(worksheetPart) + 1,
                SheetId = 2,
                Name = "Memories"
            };
            sheets.Append(memoriesSheet);

            Row headerRow = new Row();
            var columns = new List<string>();
            foreach (DataColumn column in memoriesTable.Columns)
            {
                columns.Add(column.ColumnName);
                Cell cell = new Cell();
                cell.DataType = CellValues.String;
                cell.CellValue = new CellValue(column.ColumnName);
                headerRow.AppendChild(cell);
            }
            sheetData.AppendChild(headerRow);
            foreach (DataRow dsrow in memoriesTable.Rows)
            {
                Row newRow = new Row();
                foreach (var col in columns)
                {
                    Cell cell = new Cell();
                    cell.DataType = CellValues.String;
                    cell.CellValue = new CellValue(dsrow[col].ToString());
                    newRow.AppendChild(cell);
                }
                sheetData.AppendChild(newRow);
            }
        }

        private static void AddProcessorsToTable(DataTable procesorsTable, SpreadsheetDocument spreadsheetDocument, WorksheetPart worksheetPart, SheetData sheetData, Sheets sheets)
        {
            Sheet processorsSheet = new Sheet()
            {
                Id = spreadsheetDocument.WorkbookPart.GetIdOfPart(worksheetPart),
                SheetId = 1,
                Name = "Processors"
            };
            sheets.Append(processorsSheet);

            Row headerRow = new Row();
            var columns = new List<string>();
            foreach (DataColumn column in procesorsTable.Columns)
            {
                columns.Add(column.ColumnName);
                Cell cell = new Cell();
                cell.DataType = CellValues.String;
                cell.CellValue = new CellValue(column.ColumnName);
                headerRow.AppendChild(cell);
            }
            sheetData.AppendChild(headerRow);
            foreach (DataRow dsrow in procesorsTable.Rows)
            {
                Row newRow = new Row();
                foreach (var col in columns)
                {
                    Cell cell = new Cell();
                    cell.DataType = CellValues.String;
                    cell.CellValue = new CellValue(dsrow[col].ToString());
                    newRow.AppendChild(cell);
                }
                sheetData.AppendChild(newRow);
            }
        }

        private static void CreateTable(SpreadsheetDocument spreadsheetDocument, out WorkbookPart workbookPart, out WorksheetPart worksheetPart, out SheetData sheetData, out Sheets sheets)
        {
            workbookPart = spreadsheetDocument.AddWorkbookPart();
            workbookPart.Workbook = new Workbook();

            worksheetPart = workbookPart.AddNewPart<WorksheetPart>();
            sheetData = new SheetData();
            worksheetPart.Worksheet = new Worksheet(sheetData);

            sheets = workbookPart.Workbook.AppendChild(new Sheets());
        }
    }
}
