using System;
using System.Collections.Generic;
using Core.Models;

namespace Core
{
    public interface IProcessor
    {
        int Citations { get; }
        event EventHandler CitationsCountChanged;

        int RowsToWrite { get; }
        event EventHandler RowsToWriteChanged;

        IEnumerable<CashBondForfitureInput> MapToCashBondForfitureInput(Dictionary<int, IEnumerable<RowItem>> origin);
        IEnumerable<CashBondForfitureOutput> MapToCashBondForfitureOutput(IEnumerable<CashBondForfitureInput> input);

    }
}