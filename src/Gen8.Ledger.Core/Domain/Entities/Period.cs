using System;
using Gen8.Ledger.Core.Infrastructure.Domain;

namespace Gen8.Ledger.Core.Domain
{
    public class Period : BaseEntity
    {
        public string Title {get; set;}
        public int Days {get;set;}
        public int Terms {get;set;}
        public DateTime StartDate {get; set;}
        public DateTime EndDate {get; set;}
    }
}
