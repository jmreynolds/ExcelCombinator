
using System;
using System.Collections.Generic;
using Core.Models;

namespace Core
{
    public interface IReadExcelFiles
    {
        string InputFile { get; set; }
        event EventHandler InputFileChanged;
        IEnumerable<string> ReadColumnNames();
        Dictionary<int, IEnumerable<string>> ReadWorkSheet();
    }
}