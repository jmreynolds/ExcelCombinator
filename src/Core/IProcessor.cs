using System.Collections.Generic;
using Core.Models;

namespace Core
{
    public interface IProcessor
    {
        IEnumerable<CashBondForfitureInput> MapToCashBondForfitureInput(Dictionary<int, IEnumerable<string>> origin);
        IEnumerable<CashBondForfitureOutput> MapToCashBondForfitureOutput(IEnumerable<CashBondForfitureInput> input);

    }
}