using System.Collections.Generic;
using Gen8.Ledger.Core.ReadModel.Dtos;
using Gen8.Ledger.Core.Repositories;

namespace Gen8.Ledger.Core.ReadModel
{
    public class ReadModelFacade : IReadModelFacade
    {
        private readonly INoSqlRepository<BookDto> _nosqlRepo;
        private readonly IRelationalRepository<BookDto> _relationalRepo;
        
        public ReadModelFacade(INoSqlRepository<BookDto> nosqlRepo, IRelationalRepository<BookDto> relationalRepo)
        {
            _nosqlRepo = nosqlRepo;
            _relationalRepo = relationalRepo;
        }
        public IEnumerable<BookDto> GetTransactionItems()
        {
            return _nosqlRepo.FindAll();
        }
        public BookDto Find(string field, string value)
        {
            return _nosqlRepo.Find(field, value);
        }
    }
}
