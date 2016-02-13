using System;
using System.Collections.Generic;
using System.Linq;
using Core;
using Core.EqualityImplementations;
using Core.Models;

namespace DataAccess
{
    public class Processor : IProcessor
    {
        private int _citations;
        private int _rowsToWrite;

        public int Citations
        {
            get { return _citations; }
            set
            {
                _citations = value;
                CitationsCountChanged?.Invoke(this, EventArgs.Empty);
            }
        }

        public event EventHandler CitationsCountChanged;

        public IEnumerable<CashBondForfitureInput> MapToCashBondForfitureInput(
            Dictionary<int, IEnumerable<RowItem>> origin)
        {
            var foo = origin.Values
                .Select(items =>
                {
                    var rowItems = items as RowItem[] ?? items.ToArray();
                    return rowItems;
                })
                .ToList();
            foreach (var rowArrays in foo)
            {
                var result = new CashBondForfitureInput();
                foreach (var rowItem in rowArrays)
                {
                    var columnName = rowItem.ColumnName;
                    var rowValue = rowItem.Value;
                    switch (columnName)
                    {
                        case "Offense Date":
                            result.OffenseDate = rowValue;
                            break;
                        case "Citationýnumber":
                            result.CitationNumber = rowValue;
                            break;
                        case "Name":
                            result.Name = rowValue;
                            break;
                        case "Address":
                            result.Address = rowValue;
                            break;
                        case "City, St Zip":
                            result.AddressLine2 = rowValue;
                            break;
                        case "Offense":
                            result.Offense = rowValue;
                            break;
                        case "Juvenile":
                            result.Juvenile = rowValue;
                            break;
                        case "Disp Oper":
                            result.DispOper = rowValue;
                            break;
                        case "Bf Status Date":
                            result.DispositionDate = rowValue;
                            break;
                        default:
                            //throw new ArgumentException($"Invalid column: {columnName}.");
                            //TODO - JMR - How should I handle extra columns???
                            break;
                    }
                }
                yield return result;
            }
        }


        public IEnumerable<CashBondForfitureOutput> MapToCashBondForfitureOutput(
            IEnumerable<CashBondForfitureInput> input)
        {
            Citations = 0;
            var comparer = new CashBondForfitureOutputCompare();
            var rows = input.ToList();
            var result = new List<CashBondForfitureOutput>();

            for (int rowIndex = 0; rowIndex < rows.Count; rowIndex++)
            {
                var row = rows[rowIndex];
                var newItem = new CashBondForfitureOutput
                {
                    Name = row.Name,
                    Address = row.Address,
                    AddressLine2 = row.AddressLine2,
                    DateOfBirth = row.DateOfBirth,
                    DispositionDate = row.DispositionDate
                };

                var existingItem = result.FirstOrDefault(x => comparer.Equals(x, newItem) && x.Citations.Count <= 7);
                if (existingItem != null)
                {
                    existingItem.Citations.Add(new Citation {CitationNumber = row.CitationNumber, Offense = row.Offense});
                    Citations++;
                }
                else
                {
                    newItem.Citations.Add(new Citation {CitationNumber = row.CitationNumber, Offense = row.Offense});
                    result.Add(newItem);
                    Citations++;
                }
            }
            RowsToWrite = result.Count;
            return result;
        }

        public int RowsToWrite
        {
            get { return _rowsToWrite; }
            private set
            {
                _rowsToWrite = value;
                RowsToWriteChanged?.Invoke(this, EventArgs.Empty);
            }
        }

        public event EventHandler RowsToWriteChanged;
    }
}