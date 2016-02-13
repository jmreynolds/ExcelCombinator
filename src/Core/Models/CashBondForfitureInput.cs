namespace Core.Models
{
    public class CashBondForfitureInput
    {
        public string OffenseDate { get; set; }
        public string CitationNumber { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string AddressLine2 { get; set; }
        public string Offense { get; set; }
        public string Juvenile { get; set; }
        public string DispOper { get; set; }
        public string DispositionDate { get; set; }
        
        // Not currently used
        // will figure out how to make sure we
        // incorporate dynamic stuff later.
        //public string LastHearingDate { get; set; }
        //public string Court { get; set; }
        //public string LastHearingCode { get; set; }
        public string DateOfBirth { get; set; }
        //public string Final { get; set; }
    }
}