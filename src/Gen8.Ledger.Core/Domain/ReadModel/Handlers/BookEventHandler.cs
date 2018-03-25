using System.Collections.Generic;
using Gen8.Ledger.Core.Domain;
using Gen8.Ledger.Core.ReadModel.Dtos;
using Gen8.Ledger.Core.ReadModel.Events;
using Gen8.Ledger.Core.Repositories;
using CQRSlite.Events;

namespace Gen8.Ledger.Core.ReadModel.Handlers
{
	public class BookEventHandler : IEventHandler<BookCreated>,
                                    IEventHandler<TransactionCreated>
    {
        private readonly IRepository<BookDto> _repo;
        public BookEventHandler(IRepository<BookDto> repo)
        {
            _repo = repo;
        }
        public void Handle(TransactionCreated message)
        {
            BookDto book = _repo.FindByAggregateId(message.Id);
            book.Journal.Add(message.JournalEntry);
            foreach (var ledgerAccount in message.LedgerEntry)
            {
                // Ledger
                List<LedgerEntry> ledgerList = new List<LedgerEntry>();
                if (book.Ledger.ContainsKey(ledgerAccount.Key))
                {
                    ledgerList = book.Ledger[ledgerAccount.Key];
                }
                ledgerList.Add(ledgerAccount.Value);
                book.Ledger[ledgerAccount.Key] = ledgerList;

                // Trial Balance
                List<TrialBalanceEntry> trialBalanceList = new List<TrialBalanceEntry>();
                bool isNewEntry = true;
                if (book.TrialBalance.ContainsKey(ledgerAccount.Key)) {
                    trialBalanceList = book.TrialBalance[ledgerAccount.Key];
                    foreach (TrialBalanceEntry trialBalanceEntry in trialBalanceList)
                    {
                        if(trialBalanceEntry.CurrencyId == ledgerAccount.Value.CurrencyId)
                        {
                            isNewEntry = false;
                        }
                        else
                        {
                            trialBalanceEntry.CurrencyId = ledgerAccount.Value.CurrencyId;
                        }
                        if (ledgerAccount.Value.Journal == JournalType.Debit)
                        {
                            trialBalanceEntry.TotalDebit += ledgerAccount.Value.TotalValue;
                        }
                        else
                        {
                            trialBalanceEntry.TotalCredit += ledgerAccount.Value.TotalValue;
                        }
                        if (isNewEntry)
                            trialBalanceList.Add(trialBalanceEntry);
                    }
                }
                book.TrialBalance[ledgerAccount.Key] = trialBalanceList;
            }

            _repo.Update(book.UniqueId, book);
        }

        public void Handle(BookCreated message)
        {
            var transaction = new BookDto(message.Id, message.UserId, message.JournalEntry, message.LedgerEntry);
            _repo.Create(transaction);
        }
    }
}
