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
            Cells = Worksheet.Cells;
            var colCount = Cells.MaxDataColumn;
            var columns = new List<string>();
            for (int i = 0; i <= colCount; i++)
            {
                columns.Add(Cells.GetCell(0,i).ToString());
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

            RowsRead = 0; // We're not counting the Header...
            Dictionary<int, string> columnNames = new Dictionary<int, string>();
            for (int rowNum = 0; rowNum < RowCount; rowNum++)
            {
                if (rowNum == 0)
                {
                    for (int colNum = 0; colNum < colCount; colNum++)
                    {
                        var colName = range.GetValue(rowNum, colNum).ToString();
                        if (colName == "Offense Date") columnNames.Add(colNum, colName);
                        if (colName == "Citationýnumber") columnNames.Add(colNum, colName);
                        if (colName == "Name") columnNames.Add(colNum, colName);
                        if (colName == "Address") columnNames.Add(colNum, colName);
                        if (colName == "City, St Zip") columnNames.Add(colNum, colName);
                        if (colName == "Offense") columnNames.Add(colNum, colName);
                        if (colName == "Bf Status Date") columnNames.Add(colNum, colName);
                        if (colName == "Juvenile") columnNames.Add(colNum, colName);
                        if (colName == "Disp Oper") columnNames.Add(colNum, colName);
                        //Bf row[6]
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
            return rows.ToDictionary(x => x.Key, x => x.Value);
        }
    }
}