using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace PcPartsPickerCrawler
{
    class Program
    {
        static void Main(string[] args)
        {
            var cpuResult = new NewEggCpuGatherer().GatherCpuData().GetAwaiter().GetResult();

            string path = System.Reflection.Assembly.GetExecutingAssembly().CodeBase;
            var directory = System.IO.Path.GetDirectoryName(path);

            using (var spreadsheetDocument = SpreadsheetDocument.Create(@"E:\DesktopOld\Desktop\Crawlers\ArdesCrawler\PcPartsPickerCrawler\Data\NewEggData.xls", SpreadsheetDocumentType.Workbook))
            {
                WorkbookPart workbookPart;
                WorksheetPart worksheetPart;
                SheetData sheetData;
                Sheets sheets;

                CreateTable(spreadsheetDocument, out workbookPart, out worksheetPart, out sheetData, out sheets);

                DataTable procesorsTable = (DataTable)JsonConvert.DeserializeObject(JsonConvert.SerializeObject(cpuResult), (typeof(DataTable)));
                AddProcessorsToTable(procesorsTable, spreadsheetDocument, worksheetPart, sheetData, sheets);

                workbookPart.Workbook.Save();

                // Close the document.
                spreadsheetDocument.Close();

                Console.WriteLine($@"Excel file created , you can find the file E:\DesktopOld\Desktop\Crawlers\ArdesCrawler\PcPartsPickerCrawler\Data\NewEggData.xls");
            }

            Console.WriteLine(cpuResult.Count());
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
