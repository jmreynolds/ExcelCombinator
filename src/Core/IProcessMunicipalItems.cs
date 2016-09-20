using System.Collections.Generic;
using Core.Models;

namespace Core
{
    public interface IProcessMunicipalItems : IProcessor
    {
        IEnumerable<CashBondForfitureInput> MapToCashBondForfitureInput(Dictionary<int, IEnumerable<RowItem>> origin);
        IEnumerable<CashBondForfitureOutput> MapToCashBondForfitureOutput(IEnumerable<CashBondForfitureInput> input);
    }
}