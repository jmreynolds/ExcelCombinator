using System;
using System.Collections.Generic;
using System.Linq;
using Core;
using Core.Models;

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
            object[,] range = new object[rowCount,20];
            range = SetColumnNames(range);
            RowsWritten = 1;
            for (var i = 1; i < rowCount; i++)
            {
                var rowIndex = i;
                var name = rows[i].DynamicItems
                    .Where(x => x.ColumnName == "Name")
                    .Select(x => x.Value)
                    .FirstOrDefault();
                if (string.IsNullOrEmpty(name)) throw new ArgumentException($"Row {i} has no name.");

                var citations = rows[i].DynamicItems
                    .Where(x => x.ColumnName == "Citations")
                    .Select(x => x.Value)
                    .FirstOrDefault();
                if (citations == null) throw new ArgumentException($"Row {i} has no citations. Check on person: {name}");

                var address = rows[i].DynamicItems
                    .Where(x => x.ColumnName == "Address")
                    .Select(x => x.Value)
                    .FirstOrDefault();
                if (string.IsNullOrEmpty(address)) throw new ArgumentException($"Row {i} has no address. Check on person: {name}");


                var city = rows[i].DynamicItems
                    .Where(x => x.ColumnName == "City")
                    .Select(x => x.Value)
                    .FirstOrDefault();
                if (string.IsNullOrEmpty(city)) throw new ArgumentException($"Row {i} has no City. Check on person: {name}");
                var state = rows[i].DynamicItems
                    .Where(x => x.ColumnName == "State")
                    .Select(x => x.Value)
                    .FirstOrDefault();
                if (string.IsNullOrEmpty(state)) throw new ArgumentException($"Row {i} has no State. Check on person: {name}");
                var zip = rows[i].DynamicItems
                    .Where(x => x.ColumnName == "Zip")
                    .Select(x => x.Value)
                    .FirstOrDefault();
                //if (string.IsNullOrEmpty(zip)) throw new ArgumentException($"Row {i} has no Zip. Check on person: {name}");

                var dispositionDate = rows[i].DynamicItems
                    .Where(x => x.ColumnName == "DispositionDate")
                    .Select(x => x.Value)
                    .FirstOrDefault();
                if (string.IsNullOrEmpty(dispositionDate)) throw new ArgumentException($"Row {i} has no dispositiondate. Check on person: {name}");

                range.SetValue(name, rowIndex, 0);
                range.SetValue(address, rowIndex, 1);
                range.SetValue(city, rowIndex, 2);
                range.SetValue(state, rowIndex, 3);
                range.SetValue(zip, rowIndex, 4);
                range.SetValue(dispositionDate, rowIndex, 19);

                var citNum = 5;
                var offNum = 6;
                foreach (Citation t in citations)
                {
                    range.SetValue(t.CitationNumber, rowIndex, citNum);
                    range.SetValue(t.Offense, rowIndex, offNum);
                    citNum = citNum + 2;
                    offNum = offNum + 2;
                }
                RowsWritten++;
            }
            SaveRangeToExcelFile(range);
        }


        private object[,] SetColumnNames(object[,] range)
        {
            range.SetValue("Name",0,0);
            range.SetValue( "Address", 0, 1);
            range.SetValue("City", 0, 2);
            range.SetValue("State", 0, 3);
            range.SetValue("Zip", 0, 4);
            range.SetValue( "Citation Number 1", 0, 5);
            range.SetValue( "Offense 1", 0, 6);
            range.SetValue( "Citation Number 2", 0,7);
            range.SetValue( "Offense  2", 0, 8);
            range.SetValue( "Citation Number 3", 0, 9);
            range.SetValue( "Offense 3", 0, 10);
            range.SetValue( "Citation Number 4", 0, 11);
            range.SetValue( "Offense 4", 0, 12);
            range.SetValue( "Citation Number 5", 0, 13);
            range.SetValue( "Offense 5", 0, 14);
            range.SetValue( "Citation Number 6", 0, 15);
            range.SetValue( "Offense 6", 0, 16);
            range.SetValue( "Citation Number 7", 0, 17);
            range.SetValue( "Offense 7", 0, 18);
            range.SetValue("DispositionDate", 0, 19);
            return range;
        }
    }
}