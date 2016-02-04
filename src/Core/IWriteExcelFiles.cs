using System;
using System.Collections.Generic;
using Core.Models;

namespace Core
{
    public interface IWriteExcelFiles
    {
        string OutputPath { get; set; }
        event EventHandler OutputPathChanged;
        void WriteToExcelFile(IEnumerable<CashBondForfitureOutput> output);
    }
}