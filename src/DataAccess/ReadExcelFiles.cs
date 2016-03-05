using System;
using System.Collections.Generic;
using System.Linq;
using Core;
using Core.Exceptions;
using Core.Models;

namespace DataAccess
{
    public class ReadExcelFiles : ExcelBase, IReadExcelFiles
    {
        private int _rowCount;
        private int _rowsRead;

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


            RowCount = range.GetLength(0);
            var colCount = range.GetLength(1);


            // where int is the row number and IEnumerable is the Row content
            Dictionary<int, IEnumerable<RowItem>> rows = new Dictionary<int, IEnumerable<RowItem>>();

            try
            {
                RowsRead = 0; // We're not counting the Header...
                Dictionary<int, string> columnNames = new Dictionary<int, string>();
                for (int rowNum = 1; rowNum <= RowCount; rowNum++)
                {
                    if (rowNum == 1)
                    {
                        for (int colNum = 1; colNum <= colCount; colNum++)
                        {
                            var columnName = range.GetValue(rowNum, colNum).ToString();
                            switch (columnName)
                            {
                                case "Offense Date":
                                    columnNames.Add(colNum, columnName);
                                    break;
                                case "Citationýnumber":
                                    columnNames.Add(colNum, columnName);
                                    break;
                                case "Name":
                                    columnNames.Add(colNum, columnName);
                                    break;
                                case "Address":
                                    columnNames.Add(colNum, columnName);
                                    break;
                                case "City, St Zip":
                                    columnNames.Add(colNum, columnName);
                                    break;
                                case "Offense":
                                    columnNames.Add(colNum, columnName);
                                    break;
                                case "Bf Status Date":
                                    columnNames.Add(colNum, columnName);
                                    break;
                                case "Juvenile":
                                    columnNames.Add(colNum, columnName);
                                    break;
                                case "Disp Oper":
                                    columnNames.Add(colNum, columnName);
                                    break;
                                default:
                                    string[] allowedColumns = new[] { "Offense Date", "Citationýnumber", "Name", "Address", "City, St Zip", "Offense", "Juvenile", "Disp Oper", "Bf Status Date", };
                                    throw new InvalidColumnException($"Invalid column: {columnName}.", columnName, allowedColumns, null);
                            }
                        }

                    }
                    else
                    {
                        List<RowItem> row = columnNames
                            .Select(columnName =>
                                new RowItem
                                {
                                    ColumnName = columnName.Value,
                                    Value = range.GetValue(rowNum, columnName.Key).ToString() //Worksheet.UsedRange.Cells[rowNum, columnName.Key].Text.ToString()
                                }).ToList();
                        rows.Add(rowNum, row);
                        RowsRead++;
                    }
                }
            }
            catch (Exception exc)
            {
                throw;
            }
            return rows.Where(pair => pair.Key > 1).ToDictionary(x => x.Key, x => x.Value);
        }
    }
}