using System;
using Gen8.Ledger.Core.Domain;
using CQRSlite.Events;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Gen8.Ledger.Core.ReadModel.Events
{
    public class AccountCreated : IEvent
    {
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

        public string Title { get; set; }
        public string Description { get; set; }
        public string Notes { get; set; }
        public string Code { get; set; }
        public AccountType Type { get; set; }
        public CounterpartyType CounterpartyType { get; set; }
        public Security Security { get; set; }
        public string ParentAccountId { get; set; }
        public int Version { get; set; }
        public DateTimeOffset TimeStamp { get; set; }
        public AccountCreated() {}
        public AccountCreated(Guid id, string title, string description, string notes, string code, AccountType type,
                            CounterpartyType counterpartyType, Security security, string parentAccountId)
        {
            Id = id;
            AggregateId = id.ToString();
            Title = title;
            Description = description;
            Notes = notes;
            Code = code;
            Type = type;
            CounterpartyType = counterpartyType;
            Security = security;
            ParentAccountId = parentAccountId;
        }
    }
}
