namespace Core.Models
{
    public class CashBondForfitureInput
    {
        public string OffenseDate { get; set; } = string.Empty;
        public string CitationNumber { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string AddressLine2 { get; set; } = string.Empty;
        public string Offense { get; set; } = string.Empty;
        public string Juvenile { get; set; } = string.Empty;
        public string DispOper { get; set; } = string.Empty;
        public string DispositionDate { get; set; } = string.Empty;

        // Not currently used
        // will figure out how to make sure we
        // incorporate dynamic stuff later.
        public string LastHearingDate { get; set; } = string.Empty;
        public string Court { get; set; } = string.Empty;
        public string LastHearingCode { get; set; } = string.Empty;
        public string DateOfBirth { get; set; } = string.Empty;
        public string Final { get; set; }= string.Empty;
    }
}