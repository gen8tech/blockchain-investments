namespace Gen8.Ledger.Core.Domain
{
    public class TrialBalanceEntry
    {
        public string CurrencyId { get; set; }
        public double TotalDebit { get; set; }
        public double TotalCredit { get; set; }
    }
}
