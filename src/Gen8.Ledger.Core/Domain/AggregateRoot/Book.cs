using System;
using System.Collections.Generic;
using Gen8.Ledger.Core.ReadModel.Events;
using CQRSlite.Domain;

namespace Gen8.Ledger.Core.Domain
{
    public class Book : AggregateRoot
    {
        private string _userId;
        private JournalEntry _journalEntry;
        private Dictionary<string, LedgerEntry> _ledgerEntry;

        public void AddTransaction(Guid id, string userId, JournalEntry journalEntry)
        {
            _ledgerEntry = PostLedger(journalEntry);
            ApplyChange(new TransactionCreated(id, userId, journalEntry, _ledgerEntry));
        }
        private Book(){}
        public Book(Guid id, string userId, JournalEntry journalEntry)
        {
            Id = id;
            _userId = userId;
            _journalEntry = journalEntry;
            _ledgerEntry = PostLedger(journalEntry);
            ApplyChange(new BookCreated(id, userId, journalEntry, _ledgerEntry));
        }
        private Dictionary<string, LedgerEntry> PostLedger(JournalEntry journalEntry)
        {
            Dictionary<string, LedgerEntry> ledgerEntry = new Dictionary<string, LedgerEntry>();

            LedgerEntry debitEntry = new LedgerEntry();
            LedgerEntry creditEntry = new LedgerEntry();

            // Journal data
            debitEntry.CurrencyId = journalEntry.CurrencyId;
            debitEntry.EventDate = journalEntry.EventDate;
            creditEntry.CurrencyId = journalEntry.CurrencyId;
            creditEntry.EventDate = journalEntry.EventDate;

            // Transactions
            foreach(Transaction transaction in journalEntry.Splits)
            {
                if (transaction.Journal == JournalType.Debit)
                {
                    debitEntry.Journal = JournalType.Debit;
                    debitEntry.TotalValue = transaction.Quantity * transaction.Value;
                    ledgerEntry.Add(transaction.AccountAggregateId.ToString(), debitEntry);
                } else
                {
                    creditEntry.Journal = JournalType.Credit;
                    creditEntry.TotalValue = transaction.Quantity * transaction.Value;
                    ledgerEntry.Add(transaction.AccountAggregateId.ToString(), creditEntry);
                }
            }

            return ledgerEntry;
        }
    }
}
