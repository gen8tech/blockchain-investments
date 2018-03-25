using Gen8.Ledger.Core.Infrastructure.Domain;

namespace Gen8.Ledger.Core.Domain
{
    public class Lot : BaseEntity
    {
        public Account Account {get; set;}
        public bool IsClosed {get; set;}
    }
}
