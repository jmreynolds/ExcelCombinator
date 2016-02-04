using System;
using System.Collections.Generic;
using System.Linq;
using Core;

namespace DataAccess
{
    public class ReadExcelFiles : ExcelBase, IReadExcelFiles
    {
        private string _inputFile;
        

        public string InputFile
        {
            get { return _inputFile; }
            set
            {
                _inputFile = value; 
                InputFileChanged?.Invoke(this, EventArgs.Empty);
            }
        }

        public event EventHandler InputFileChanged;

        private int _rowCount;

        public int RowCount
        {
            get { return _rowCount; }
            private set
            {
                _rowCount = value;
                RowCounterChanged?.Invoke(this, EventArgs.Empty);
            }
        }

        public event EventHandler RowCounterChanged;

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

        public virtual Dictionary<int, IEnumerable<string>> ReadWorkSheet()
        {
            // where int is the row number and IEnumerable is the Row content
            Dictionary<int, IEnumerable<string>> rows = new Dictionary<int, IEnumerable<string>>();
            try
            {
                OpenWorksheet(InputFile);
                var colCount = Worksheet.UsedRange.Cells.Columns.Count;
                var rowCount = Worksheet.UsedRange.Cells.Rows.Count;
                for (int rowNum = 1; rowNum <= rowCount; rowNum++)
                {
                    List<string> row = new List<string>();
                    for (int colNum = 1; colNum <= colCount; colNum++)
                    {
                        row.Add(Worksheet.UsedRange.Cells[rowNum, colNum].Text.ToString());
                    }
                    rows.Add(rowNum, row);
                }
            }
            catch (Exception exc)
            {
                throw;
            }
            finally
            {
                KillExcel();
            }
            RowCount = rows.Count-1;
            return rows.Where(pair => pair.Key > 1).ToDictionary(x => x.Key, x => x.Value);
        }

        protected void OpenWorksheet(string fileName)
        {
            Application = new NetOffice.ExcelApi.Application();
            Workbook = Application.Workbooks.Open(fileName);
            Worksheet = (NetOffice.ExcelApi.Worksheet) Workbook.Sheets.First();
        }
    }
}