using System;
using System.Collections.Generic;
using Gen8.Ledger.Core.Domain;
using CQRSlite.Events;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Gen8.Ledger.Core.ReadModel.Events
{
    public class TransactionCreated : IEvent
	{
        public TransactionCreated() {}
        public TransactionCreated(Guid id, string userId, JournalEntry journalEntry, Dictionary<string, LedgerEntry> ledgerEntry)
        {
            Id = id;
            AggregateId = id.ToString();
            UserId = userId;
            JournalEntry = journalEntry;
            LedgerEntry = ledgerEntry;
        }
        private ObjectId _objectId;
        [BsonId]
        public ObjectId ObjectId
        {
            get
            {
                if (_objectId.ToString().Equals("000000000000000000000000"))
                    _objectId = ObjectId.GenerateNewId();

                return _objectId;
            }
            set
            {
                _objectId = value;
            }
        }
        public Guid Id
        {
            get
            {
                return new Guid(AggregateId);
            }
            set
            {
                AggregateId = value.ToString();
            }
        }
        public string AggregateId {get; set;}
        public string UserId { get; set; }
        public JournalEntry JournalEntry { get; set; }
        public Dictionary<string, LedgerEntry> LedgerEntry { get; set; }
        public int Version { get; set; }
        public DateTimeOffset TimeStamp { get; set; }
	}
}
