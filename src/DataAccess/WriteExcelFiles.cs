using System;
using System.Collections.Generic;
using System.Linq;
using Core;
using Core.Models;
using NetOffice.ExcelApi.Enums;

namespace DataAccess
{
    public class WriteExcelFiles : ExcelBase, IWriteExcelFiles
    {
        private int _rowsWritten;

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

            var rows = output.ToArray();
            var rowCount = rows.Length;
            object[,] range = new object[rowCount+1,18];
            range = SetColumnNames(range);
            RowsWritten = 0; // Don't count the column headers.
            for (var rowIndex = 0; rowIndex < rowCount; rowIndex++)
            {
                var rangeIndex = rowIndex+1;
                range.SetValue(rows[rowIndex].Name, rangeIndex, 0);
                range.SetValue(rows[rowIndex].Address, rangeIndex, 1);
                range.SetValue(rows[rowIndex].AddressLine2, rangeIndex, 2);

                var citations = rows[rowIndex].Citations;
                var citNum = 3;
                var offNum = 4;
                foreach (Citation t in citations)
                {
                    range.SetValue(t.CitationNumber, rangeIndex, citNum);
                    range.SetValue(t.Offense, rangeIndex, offNum);
                    citNum = citNum + 2;
                    offNum = offNum + 2;
                }
                range.SetValue(rows[rowIndex].DispositionDate, rangeIndex, 17);
                RowsWritten++;
            }
            SaveRangeToExcelFile(range);
        }


        private object[,] SetColumnNames(object[,] range)
        {
            range.SetValue("Name",0,0);
            range.SetValue( "Address", 0, 1);
            range.SetValue( "City, ST Zip", 0, 2);
            range.SetValue( "Citation Number 1", 0, 3);
            range.SetValue( "Offense 1", 0, 4);
            range.SetValue( "Citation Number 2", 0,5);
            range.SetValue( "Offense  2", 0, 6);
            range.SetValue( "Citation Number 3", 0, 7);
            range.SetValue( "Offense 3", 0, 8);
            range.SetValue( "Citation Number 4", 0, 9);
            range.SetValue( "Offense 4", 0, 10);
            range.SetValue( "Citation Number 5", 0, 11);
            range.SetValue( "Offense 5", 0, 12);
            range.SetValue( "Citation Number 6", 0, 13);
            range.SetValue( "Offense 6", 0, 14);
            range.SetValue( "Citation Number 7", 0, 15);
            range.SetValue( "Offense 7", 0, 16);
            range.SetValue("Disposition Date", 0, 17);
            return range;
        }
    }
}