namespace ArdesCrawler
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using DocumentFormat.OpenXml;
    using DocumentFormat.OpenXml.Packaging;
    using DocumentFormat.OpenXml.Spreadsheet;
    using Newtonsoft.Json;

    class Program
    {
        static void Main(string[] args)
        {
            var hardDisksResult = new ArdesBgDataGatherer().GatherData("tvardi-diskove", 54).GetAwaiter().GetResult();
            var processorsResult = new ArdesBgDataGatherer().GatherData("protsesori", 6).GetAwaiter().GetResult();
            var motherboardsResult = new ArdesBgDataGatherer().GatherData("danni-platki", 20).GetAwaiter().GetResult();
            var coolersResult = new ArdesBgDataGatherer().GatherData("ohladiteli", 16).GetAwaiter().GetResult();
            var videoCardsResult = new ArdesBgDataGatherer().GatherData("video-karti", 21).GetAwaiter().GetResult();
            var memoriesResult = new ArdesBgDataGatherer().GatherData("pamet", 21).GetAwaiter().GetResult();
            var powerSupliesResult = new ArdesBgDataGatherer().GatherData("zahranvane", 16).GetAwaiter().GetResult();
            var casesResult = new ArdesBgDataGatherer().GatherData("kompyutarni-kutii", 22).GetAwaiter().GetResult();

            using (var spreadsheetDocument = SpreadsheetDocument.Create(@"C:\Users\Budin\Desktop\Crawlers\ArdesCrawler\ArdesCrawler\Data\ArdesData.xls", SpreadsheetDocumentType.Workbook))
            {
                WorkbookPart workbookPart;
                WorksheetPart worksheetPart;
                SheetData sheetData;
                Sheets sheets;
                CreateTable(spreadsheetDocument, out workbookPart, out worksheetPart, out sheetData, out sheets);
                DataTable hardDiscsTable = (DataTable)JsonConvert.DeserializeObject(JsonConvert.SerializeObject(hardDisksResult), (typeof(DataTable)));
                AddHardDrivesToTable(hardDiscsTable, spreadsheetDocument, worksheetPart, sheetData, sheets);

                DataTable procesorsTable = (DataTable)JsonConvert.DeserializeObject(JsonConvert.SerializeObject(processorsResult), (typeof(DataTable)));
                AddProcessorsToTable(procesorsTable, spreadsheetDocument, worksheetPart, sheetData, sheets);

                DataTable motherboardsTable = (DataTable)JsonConvert.DeserializeObject(JsonConvert.SerializeObject(motherboardsResult), (typeof(DataTable)));
                AddMotherboardsToTable(motherboardsTable, spreadsheetDocument, worksheetPart, sheetData, sheets);

                DataTable coolersTable = (DataTable)JsonConvert.DeserializeObject(JsonConvert.SerializeObject(coolersResult), (typeof(DataTable)));
                AddCoolersToTable(coolersTable, spreadsheetDocument, worksheetPart, sheetData, sheets);

                DataTable videoCardsTable = (DataTable)JsonConvert.DeserializeObject(JsonConvert.SerializeObject(videoCardsResult), (typeof(DataTable)));
                AddVideoCardsToTable(videoCardsTable, spreadsheetDocument, worksheetPart, sheetData, sheets);

                DataTable memoriesTable = (DataTable)JsonConvert.DeserializeObject(JsonConvert.SerializeObject(memoriesResult), (typeof(DataTable)));
                AddMemoriesToTable(memoriesTable, spreadsheetDocument, worksheetPart, sheetData, sheets);

                DataTable powerSupliesTable = (DataTable)JsonConvert.DeserializeObject(JsonConvert.SerializeObject(powerSupliesResult), (typeof(DataTable)));
                AddPowerSupliesToTable(powerSupliesTable, spreadsheetDocument, worksheetPart, sheetData, sheets);

                DataTable casesTable = (DataTable)JsonConvert.DeserializeObject(JsonConvert.SerializeObject(casesResult), (typeof(DataTable)));
                AddCasesToTable(casesTable, spreadsheetDocument, worksheetPart, sheetData, sheets);

                workbookPart.Workbook.Save();

                // Close the document.
                spreadsheetDocument.Close();

                Console.WriteLine(@"Excel file created , you can find the file C:\Users\Budin\Desktop\Crawlers\ArdesCrawler\ArdesCrawler\Data\ArdesData.xls");
            }
        }

        private static void AddCasesToTable(DataTable casesTable, SpreadsheetDocument spreadsheetDocument, WorksheetPart worksheetPart, SheetData sheetData, Sheets sheets)
        {
            Sheet casesSheet = new Sheet()
            {
                Id = spreadsheetDocument.WorkbookPart.GetIdOfPart(worksheetPart),
                SheetId = 8,
                Name = "Cases"
            };
            sheets.Append(casesSheet);

            Row headerRow = new Row();
            var columns = new List<string>();
            foreach (DataColumn column in casesTable.Columns)
            {
                columns.Add(column.ColumnName);
                Cell cell = new Cell();
                cell.DataType = CellValues.String;
                cell.CellValue = new CellValue(column.ColumnName);
                headerRow.AppendChild(cell);
            }
            sheetData.AppendChild(headerRow);
            foreach (DataRow dsrow in casesTable.Rows)
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

        private static void AddPowerSupliesToTable(DataTable powerSupliesTable, SpreadsheetDocument spreadsheetDocument, WorksheetPart worksheetPart, SheetData sheetData, Sheets sheets)
        {
            Sheet powerSupliesSheet = new Sheet()
            {
                Id = spreadsheetDocument.WorkbookPart.GetIdOfPart(worksheetPart),
                SheetId = 7,
                Name = "PowerSuplies"
            };
            sheets.Append(powerSupliesSheet);

            Row headerRow = new Row();
            var columns = new List<string>();
            foreach (DataColumn column in powerSupliesTable.Columns)
            {
                columns.Add(column.ColumnName);
                Cell cell = new Cell();
                cell.DataType = CellValues.String;
                cell.CellValue = new CellValue(column.ColumnName);
                headerRow.AppendChild(cell);
            }
            sheetData.AppendChild(headerRow);
            foreach (DataRow dsrow in powerSupliesTable.Rows)
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

        private static void AddMemoriesToTable(DataTable memoriesTable, SpreadsheetDocument spreadsheetDocument, WorksheetPart worksheetPart, SheetData sheetData, Sheets sheets)
        {
            Sheet memoriesSheet = new Sheet()
            {
                Id = spreadsheetDocument.WorkbookPart.GetIdOfPart(worksheetPart),
                SheetId = 6,
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

        private static void AddVideoCardsToTable(DataTable videoCardsTable, SpreadsheetDocument spreadsheetDocument, WorksheetPart worksheetPart, SheetData sheetData, Sheets sheets)
        {
            Sheet videoCardsSheet = new Sheet()
            {
                Id = spreadsheetDocument.WorkbookPart.GetIdOfPart(worksheetPart),
                SheetId = 5,
                Name = "VideoCards"
            };
            sheets.Append(videoCardsSheet);

            Row headerRow = new Row();
            var columns = new List<string>();
            foreach (DataColumn column in videoCardsTable.Columns)
            {
                columns.Add(column.ColumnName);
                Cell cell = new Cell();
                cell.DataType = CellValues.String;
                cell.CellValue = new CellValue(column.ColumnName);
                headerRow.AppendChild(cell);
            }
            sheetData.AppendChild(headerRow);
            foreach (DataRow dsrow in videoCardsTable.Rows)
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

        private static void AddCoolersToTable(DataTable coolersTable, SpreadsheetDocument spreadsheetDocument, WorksheetPart worksheetPart, SheetData sheetData, Sheets sheets)
        {
            Sheet coolersSheet = new Sheet()
            {
                Id = spreadsheetDocument.WorkbookPart.GetIdOfPart(worksheetPart),
                SheetId = 4,
                Name = "Coolers"
            };
            sheets.Append(coolersSheet);

            Row headerRow = new Row();
            var columns = new List<string>();
            foreach (DataColumn column in coolersTable.Columns)
            {
                columns.Add(column.ColumnName);
                Cell cell = new Cell();
                cell.DataType = CellValues.String;
                cell.CellValue = new CellValue(column.ColumnName);
                headerRow.AppendChild(cell);
            }
            sheetData.AppendChild(headerRow);
            foreach (DataRow dsrow in coolersTable.Rows)
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

        private static void AddMotherboardsToTable(DataTable motherboardsTable, SpreadsheetDocument spreadsheetDocument, WorksheetPart worksheetPart, SheetData sheetData, Sheets sheets)
        {
            Sheet motherboardsSheet = new Sheet()
            {
                Id = spreadsheetDocument.WorkbookPart.GetIdOfPart(worksheetPart),
                SheetId = 3,
                Name = "Motherboards"
            };
            sheets.Append(motherboardsSheet);

            Row headerRow = new Row();
            var columns = new List<string>();
            foreach (DataColumn column in motherboardsTable.Columns)
            {
                columns.Add(column.ColumnName);
                Cell cell = new Cell();
                cell.DataType = CellValues.String;
                cell.CellValue = new CellValue(column.ColumnName);
                headerRow.AppendChild(cell);
            }
            sheetData.AppendChild(headerRow);
            foreach (DataRow dsrow in motherboardsTable.Rows)
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
                SheetId = 2,
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

        private static void AddHardDrivesToTable(DataTable table, SpreadsheetDocument spreadsheetDocument, WorksheetPart worksheetPart, SheetData sheetData, Sheets sheets)
        {
            Sheet hardDrivesSheet = new Sheet()
            {
                Id = spreadsheetDocument.WorkbookPart.GetIdOfPart(worksheetPart),
                SheetId = 1,
                Name = "HardDrives"
            };
            sheets.Append(hardDrivesSheet);
            Row headerRow = new Row();
            var columns = new List<string>();
            foreach (DataColumn column in table.Columns)
            {
                columns.Add(column.ColumnName);
                Cell cell = new Cell();
                cell.DataType = CellValues.String;
                cell.CellValue = new CellValue(column.ColumnName);
                headerRow.AppendChild(cell);
            }
            sheetData.AppendChild(headerRow);
            foreach (DataRow dsrow in table.Rows)
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
