using System;
using System.Collections.Generic;

namespace Gen8.Ledger.Core.Domain
{
    public class JournalEntry
    {
        public string CurrencyId {get;set;}
        public DateTime EventDate {get;set;}
        public string Description {get;set;}
        public List<Transaction> Splits {get;set;}
    }
}
