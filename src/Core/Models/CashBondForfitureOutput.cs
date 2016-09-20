using System.Collections.Generic;

namespace Core.Models
{
    public class CashBondForfitureOutput : DynamicOutput
    {
        //Address 
        //AddressLine2 
        //<Citation> 
        //DispositionDate 
        //DateOfBirth 
        public CashBondForfitureOutput()
        {
            DynamicItems = new List<DynamicItem>
            {
                new DynamicItem()
                {
                    ColumnName = "Name",
                    ShouldRemoveDupes = true
                },
                new DynamicItem
                {
                    ColumnName = "Address",
                    ShouldRemoveDupes = true
                },
                new DynamicItem
                {
                    ColumnName = "Address2",
                    ShouldRemoveDupes = true
                },
                new DynamicItem()
                {
                    ColumnName = "DispositionDate",
                    ShouldRemoveDupes = false
                },
                new DynamicItem()
                {
                    ColumnName = "Citations",
                    Value = new List<Citation>()
                }
            };
        }
    }

    public class Citation
    {
        public string CitationNumber { get; set; } = string.Empty;
        public string Offense { get; set; } = string.Empty;
    }
}