using System;
using System.Collections.Generic;
using System.Linq;
using Core;
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
            Dictionary<int, IEnumerable<string>> origin)
        {
         return   origin.Values
                .Select(row => row.ToArray())
                .Select(rowArray =>
                    new CashBondForfitureInput
                    {
                        OffenseDate = rowArray[0],
                        CitationNumber = rowArray[1],
                        Name = rowArray[2],
                        DateOfBirth = rowArray[3],
                        Address = rowArray[4],
                        AddressLine2 = rowArray[5],
                        Offense = rowArray[6],
                        Final = rowArray[7],
                        DispositionDate = rowArray[8],
                        LastHearingDate = rowArray[9],
                        Court = rowArray[10],
                        LastHearingCode = rowArray[11],
                        Juvenile = rowArray[12],
                        DispOper = rowArray[13]
                    });
        }

        public IEnumerable<CashBondForfitureOutput> MapToCashBondForfitureOutput(IEnumerable<CashBondForfitureInput> input)
        {
            Citations = 0;
            CashBondForfitureOutputCompare comparer = new CashBondForfitureOutputCompare();
            var rows = input.ToList();
            List<CashBondForfitureOutput> result = new List<CashBondForfitureOutput>();
            foreach (var row in rows)
            {
                var newItem = new CashBondForfitureOutput()
                {
                    Name = row.Name,
                    Address = row.Address,
                    AddressLine2 = row.AddressLine2,
                    DateOfBirth = row.DateOfBirth,
                    DispositionDate = row.DispositionDate,
                };

                CashBondForfitureOutput existingItem = null;

                existingItem = result.FirstOrDefault(x => comparer.Equals(x, newItem) && x.Citations.Count <= 7);
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

    public class CashBondForfitureOutputCompare : IEqualityComparer<CashBondForfitureOutput>
    {
        public bool Equals(CashBondForfitureOutput x, CashBondForfitureOutput y)
        {
            if (x == null && y == null) return true;
            if (x == null || y == null) return false;

            return x.Name == y.Name && x.Address == y.Address && x.AddressLine2 == y.AddressLine2 && x.DateOfBirth == y.DateOfBirth;
        }

        public int GetHashCode(CashBondForfitureOutput obj)
        {
            var dob = obj.DateOfBirth.Trim().ToUpper().GetHashCode();
            var name = obj.Name.Trim().ToUpper().GetHashCode();
            var add1 = obj.Address.Trim().ToUpper().GetHashCode();
            var add2 = obj.AddressLine2.Trim().ToUpper().GetHashCode();
            return dob + name + add1 + add2;
        }
    }
}