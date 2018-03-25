using System;
using Gen8.Ledger.Core.Domain;
using CQRSlite.Events;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Gen8.Ledger.Core.ReadModel.Events
{
    public class ParentAccountAssigned : IEvent
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

        public string ParentAccountId { get; set; }
        public int Version { get; set; }
        public DateTimeOffset TimeStamp { get; set; }
        public ParentAccountAssigned() {}
        public ParentAccountAssigned(Guid id, string parentAccountId)
        {
            Id = id;
            AggregateId = id.ToString();
            ParentAccountId = parentAccountId;
        }
    }
}
