using System;
using System.Collections.Generic;
using System.Linq;
using Core;
using Core.Models;

namespace DataAccess
{
    public class WriteExcelFiles : ExcelBase, IWriteExcelFiles
    {
        private string _outputPath;
        private int _rowsWritten;

        public string OutputPath
        {
            get { return _outputPath; }
            set
            {
                _outputPath = value; 
                OutputPathChanged?.Invoke(this, EventArgs.Empty);
            }
        }

        public event EventHandler OutputPathChanged;

        public int RowsWritten
        {
            get { return _rowsWritten; }
            private set
            {
                _rowsWritten = value;
                RowsWrittenChanged?.Invoke(this,EventArgs.Empty);
            }
        }

        public event EventHandler RowsWrittenChanged;

        public void WriteToExcelFile(IEnumerable<CashBondForfitureOutput> output)
        {
            OpenWorksheet();

            var rows = output.ToArray();

            SetColumnNames();
            RowsWritten = 0;
            for (var i = 0; i < rows.Length; i++)
            {
                var rowIndex = i + 2;
                Worksheet.Cells[rowIndex, 1].Value = rows[i].Name;
                Worksheet.Cells[rowIndex, 2].Value = rows[i].Address;
                Worksheet.Cells[rowIndex, 3].Value = rows[i].AddressLine2;
                var citations = rows[i].Citations;
                var citNum = 4;
                var offNum = 5;
                foreach (Citation t in citations)
                {
                    Worksheet.Cells[rowIndex, citNum].Value = t.CitationNumber;
                    Worksheet.Cells[rowIndex, offNum].Value = t.Offense;
                    citNum = citNum + 2;
                    offNum = offNum + 2;
                }
                Worksheet.Cells[rowIndex, 18].Value = rows[i].DispositionDate;
                RowsWritten++;
            }

            Worksheet.Name = "Output";
            Workbook.SaveAs(OutputPath);
            KillExcel();
        }

        private void SetColumnNames()
        {
            Worksheet.Cells[1, 1].Value = "Name";
            Worksheet.Cells[1, 2].Value = "Address";
            Worksheet.Cells[1, 3].Value = "City, ST Zip";
            Worksheet.Cells[1, 4].Value = "Citation Number 1";
            Worksheet.Cells[1, 5].Value = "Offense 1";
            Worksheet.Cells[1, 6].Value = "Citation Number 2";
            Worksheet.Cells[1, 7].Value = "Offense  2";
            Worksheet.Cells[1, 8].Value = "Citation Number 3";
            Worksheet.Cells[1, 9].Value = "Offense 3";
            Worksheet.Cells[1, 10].Value = "Citation Number 4";
            Worksheet.Cells[1, 11].Value = "Offense 4";
            Worksheet.Cells[1, 12].Value = "Citation Number 5";
            Worksheet.Cells[1, 13].Value = "Offense 5";
            Worksheet.Cells[1, 14].Value = "Citation Number 6";
            Worksheet.Cells[1, 15].Value = "Offense 6";
            Worksheet.Cells[1, 16].Value = "Citation Number 7";
            Worksheet.Cells[1, 17].Value = "Offense 7";
            Worksheet.Cells[1, 18].Value = "Disposition Date";
        }


        protected void OpenWorksheet()
        {
            Application = new NetOffice.ExcelApi.Application();
            Workbook = Application.Workbooks.Add();
            Worksheet = (NetOffice.ExcelApi.Worksheet) Workbook.Sheets.First();
        }
    }
}