using System;

namespace Gen8.Ledger.Core.Domain
{
    public class Transaction
    {
        public JournalType Journal {get; set;}
        public Guid AccountAggregateId {get; set;}
        public string Tag {get;set;}
        public double Value {get; set;}
        public double Quantity {get;set;}
        public string LotId {get;set;}
    }
    public enum JournalType
    {
           Debit = 1,
           Credit = 2
    }
}
