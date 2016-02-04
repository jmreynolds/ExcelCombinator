using System.Collections.Generic;

namespace Core.Models
{
    public class CashBondForfitureOutput
    {
        public string Name { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string AddressLine2 { get; set; } = string.Empty;
        public List<Citation> Citations { get; set; } = new List<Citation>();
        public string DispositionDate { get; set; } = string.Empty;
        public string DateOfBirth { get; set; } = string.Empty;
    }

    public class Citation
    {
        public string CitationNumber { get; set; } = string.Empty;
        public string Offense { get; set; } = string.Empty;
    }
}