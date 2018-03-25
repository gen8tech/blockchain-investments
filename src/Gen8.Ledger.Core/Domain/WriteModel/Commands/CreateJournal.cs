using System;
using Gen8.Ledger.Core.Domain;

namespace Gen8.Ledger.Core.WriteModel.Commands
{
    public class CreateJournal : BaseCommand
	{
        public readonly string UserId;
        public readonly JournalEntry JournalEntry;
        public CreateJournal(Guid id, string userId, JournalEntry journalEntry)
        {
            Id = id;
            UserId = userId;
            JournalEntry = journalEntry;
        }
	}
}
