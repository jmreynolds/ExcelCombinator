using System.Collections.Generic;

namespace Core.Models
{
    public class CashBondForfitureInput : DynamicInput
    {
        
        public CashBondForfitureInput()
        {
            DynamicItems = new List<DynamicItem>
            {
                new DynamicItem {ColumnName = "OffenseDate"},
                new DynamicItem {ColumnName = "CitationNumber"},
                new DynamicItem {ColumnName = "Name"},
                new DynamicItem {ColumnName = "Address"},
                new DynamicItem {ColumnName = "City"},
                new DynamicItem {ColumnName = "State"},
                new DynamicItem {ColumnName = "Zip"},
                new DynamicItem {ColumnName = "Offense"},
                new DynamicItem {ColumnName = "DispositionDate"},
            };
        }

        public CashBondForfitureInput(List<DynamicItem> items)
        {
            DynamicItems = items;
        }

    }
}