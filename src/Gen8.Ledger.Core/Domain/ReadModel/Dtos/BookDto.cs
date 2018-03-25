using System;
using System.Collections.Generic;
using Gen8.Ledger.Core.Domain;
using Gen8.Ledger.Core.Infrastructure.Domain;

namespace Gen8.Ledger.Core.ReadModel.Dtos
{
    public class BookDto : BaseEntity
    {
        public Guid AggregateId;
        public string UserId;
        public List<JournalEntry> Journal;
        public Dictionary<string, List<LedgerEntry>> Ledger;
        public Dictionary<string, List<TrialBalanceEntry>> TrialBalance;
        public BookDto() {}
        public BookDto(Guid id, string userId, JournalEntry journalEntry, Dictionary<string, LedgerEntry> ledgerEntries)
        {
            Journal = new List<JournalEntry>();
            Ledger = new Dictionary<string, List<LedgerEntry>>();
            TrialBalance = new Dictionary<string, List<TrialBalanceEntry>>();
            AggregateId = id;
            UserId = userId;
            Journal.Add(journalEntry);
            foreach (var ledgerEntry in ledgerEntries)
            {
                // Ledger
                List<LedgerEntry> ledgerList = new List<LedgerEntry>();
                ledgerList.Add(ledgerEntry.Value);
                Ledger.Add(ledgerEntry.Key, ledgerList);

                // Trial Balance
                List<TrialBalanceEntry> trialBalanceList = new List<TrialBalanceEntry>();
                TrialBalanceEntry trialBalanceEntry = new TrialBalanceEntry();
                trialBalanceEntry.CurrencyId = ledgerEntry.Value.CurrencyId;
                if (ledgerEntry.Value.Journal == JournalType.Debit)
                {
                    trialBalanceEntry.TotalDebit = ledgerEntry.Value.TotalValue;
                }
                else
                {
                    trialBalanceEntry.TotalCredit = ledgerEntry.Value.TotalValue;
                }
                trialBalanceList.Add(trialBalanceEntry);
                TrialBalance.Add(ledgerEntry.Key, trialBalanceList);
            }
        }
    }
}
