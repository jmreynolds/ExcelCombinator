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
                new DynamicItem {ColumnName = "AddressLine2"},
                new DynamicItem {ColumnName = "Offense"},
                new DynamicItem {ColumnName = "Juvenile"},
                new DynamicItem {ColumnName = "DispOper"},
                new DynamicItem {ColumnName = "DispositionDate"},
                new DynamicItem {ColumnName = "LastHearingDate"},
                new DynamicItem {ColumnName = "Court"},
                new DynamicItem {ColumnName = "LastHearingCode"},
                new DynamicItem {ColumnName = "DateOfBirth"},
                new DynamicItem {ColumnName = "Final"}
            };
        }

        public CashBondForfitureInput(List<DynamicItem> items)
        {
            DynamicItems = items;
        }

    }
}