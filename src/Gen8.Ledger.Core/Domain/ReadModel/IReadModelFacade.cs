using System.Collections.Generic;
using Gen8.Ledger.Core.ReadModel.Dtos;

namespace Gen8.Ledger.Core.ReadModel
{
    public interface IReadModelFacade
    {
        IEnumerable<BookDto> GetTransactionItems();
        BookDto Find(string field, string value);
    }
}
