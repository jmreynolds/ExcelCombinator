using System;
using System.Collections.Generic;
using Core.Models;

namespace Core
{
    public interface IProcessor
    {
        int Citations { get; }
        event EventHandler CitationsCountChanged;
        //int DuplicateItemsCount { get; }
        //event EventHandler DuplicateItemsCountChanged;

        int RowsToWrite { get; }
        event EventHandler RowsToWriteChanged;

        IEnumerable<DynamicInput> MapDynamicInput(Dictionary<int, IEnumerable<RowItem>> origin);
        IEnumerable<DynamicOutput> MapDynamicInputToDynamicOutput(IEnumerable<DynamicInput> input);

    }
}