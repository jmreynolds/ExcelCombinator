using System;
using System.Collections.Generic;
using System.Linq;
using Core;
using Core.Extensions;
using Core.Models;
using NetOffice.ExcelApi;
using NetOffice.ExcelApi.Enums;

namespace DataAccess
{
    public class ReadExcelFiles : ExcelBase, IReadExcelFiles
    {
        private string _inputFile;


        private int _rowCount;
        private int _rowsRead;

        public string InputFile
        {
            get { return _inputFile; }
            set
            {
                _inputFile = value;
                InputFileChanged?.Invoke(this, EventArgs.Empty);
            }
        }

        public int RowCount
        {
            get { return _rowCount; }
            private set
            {
                _rowCount = value;
                RowCountChanged?.Invoke(this, EventArgs.Empty);
            }
        }

        public int RowsRead
        {
            get { return _rowsRead; }
            private set
            {
                _rowsRead = value;
                RowsReadChanged?.Invoke(this, EventArgs.Empty);
            }
        }

        public event EventHandler RowsReadChanged;
        public event EventHandler InputFileChanged;

        public event EventHandler RowCountChanged;

        public IEnumerable<string> ReadColumnNames()
        {
            OpenWorksheet(InputFile);
            Cells = Worksheet.Rows.First().Cells;
            var colCount = Worksheet.UsedRange.Cells.Columns.Count;
            var columns = new List<string>();
            try
            {
                for (int i = 1; i <= colCount; i++)
                {
                    columns.Add(Cells[i].ToString());
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                KillExcel();
            }
            return columns;
        }

        public Dictionary<int, IEnumerable<RowItem>> ReadWorkSheet() => ReadRange(ReadRange());

        private Dictionary<int, IEnumerable<RowItem>> ReadRange(object[,] range)
        {
            if (range == null) throw new ArgumentException(nameof(range));
            var rangeDictionary = range.ConvertToDictionary();
            // where int is the row number and IEnumerable is the Row content
            Dictionary<int, IEnumerable<RowItem>> rows = new Dictionary<int, IEnumerable<RowItem>>();
            

            //try
            //{
            //    var colCount = rangeDictionary.Keys.Count; //range.Cells.Columns.Count;
            //    RowCount = rangeDictionary.Values.Count;
            //    RowsRead = 0; // We're not counting the Header...
            //    Dictionary<int, string> columnNames = new Dictionary<int, string>();
            //    for (int rowNum = 1; rowNum <= RowCount; rowNum++)
            //    {
            //        if (rowNum == 1)
            //        {
            //            for (int colNum = 1; colNum <= colCount; colNum++)
            //            {
            //                var colName = rangeDictionary.;
            //                if (colName == "Offense Date") columnNames.Add(colNum, colName);
            //                if (colName == "Citationýnumber") columnNames.Add(colNum, colName);
            //                if (colName == "Name") columnNames.Add(colNum, colName);
            //                if (colName == "Address") columnNames.Add(colNum, colName);
            //                if (colName == "City, St Zip") columnNames.Add(colNum, colName);
            //                if (colName == "Offense") columnNames.Add(colNum, colName);
            //                if (colName == "Bf Status Date") columnNames.Add(colNum, colName);
            //                if (colName == "Juvenile") columnNames.Add(colNum, colName);
            //                if (colName == "Disp Oper") columnNames.Add(colNum, colName);
            //                //Bf row[6]
            //            }

            //        }
            //        else
            //        {
            //            List<RowItem> row = columnNames
            //                .Select(columnName =>
            //                    new RowItem
            //                    {
            //                        ColumnName = columnName.Value,
            //                        Value = Worksheet.UsedRange.Cells[rowNum, columnName.Key].Text.ToString()
            //                    }).ToList();

            //            rows.Add(rowNum, row);
            //            RowsRead++;
            //        }
            //    }
            //}
            //catch (Exception exc)
            //{
            //    throw;
            //}
            return rows.Where(pair => pair.Key > 1).ToDictionary(x => x.Key, x => x.Value);
        }
        private object[,] ReadRange()
        {
            Range range = null;
            object[,] valueArray;
            try
            {
                OpenWorksheet(InputFile);
                range = Worksheet.UsedRange;
                valueArray =
                (object[,])range.get_Value
                    (XlRangeValueDataType.xlRangeValueDefault);
            }
            catch (Exception e)
            {
                throw new ApplicationException(e.Message,e.InnerException);
            }
            finally
            {
                KillExcel();
            }
            return valueArray;
        }
        private void OpenWorksheet(string fileName)
        {
            Application = new NetOffice.ExcelApi.Application();
            Workbook = Application.Workbooks.Open(fileName);
            Worksheet = (NetOffice.ExcelApi.Worksheet)Workbook.Sheets.First();
        }
    }
}