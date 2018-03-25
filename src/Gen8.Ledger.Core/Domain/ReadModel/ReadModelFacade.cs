using System.Collections.Generic;
using Gen8.Ledger.Core.ReadModel.Dtos;
using Gen8.Ledger.Core.Repositories;

namespace Gen8.Ledger.Core.ReadModel
{
    public class ReadModelFacade : IReadModelFacade
    {
        private readonly IRepository<BookDto> _repo;
        public ReadModelFacade(IRepository<BookDto> repo)
        {
            _repo = repo;
        }
        public IEnumerable<BookDto> GetTransactionItems()
        {
            return _repo.FindAll();
        }
        public BookDto Find(string field, string value)
        {
            return _repo.Find(field, value);
        }
    }
}
