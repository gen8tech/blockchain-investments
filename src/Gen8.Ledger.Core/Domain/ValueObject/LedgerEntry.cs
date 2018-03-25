using System;

namespace Gen8.Ledger.Core.Domain
{
    public class LedgerEntry
    {
        public DateTime EventDate { get; set; }
        public string CurrencyId { get; set; }
        public double TotalValue { get; set; }
        public JournalType Journal { get; set; }
    }
}
