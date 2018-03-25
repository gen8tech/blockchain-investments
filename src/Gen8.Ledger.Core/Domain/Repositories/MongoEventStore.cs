using System;
using System.Collections.Generic;
using System.Linq;
using Gen8.Ledger.Core.Infrastructure;
using CQRSlite.Events;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Gen8.Ledger.Core.Repositories
{
    // TODO: change inmemory storage to Mongo
    public class MongoEventStore : IEventStore
    {
        private readonly IEventPublisher _publisher;
        private readonly AppConfig _optionsAccessor;
        MongoClient _client;
        IMongoDatabase _db;
        string _collection;
        public MongoEventStore(IEventPublisher publisher, IOptions<AppConfig> optionsAccessor)
        {
            _publisher = publisher;
            _optionsAccessor = optionsAccessor.Value;
            _client = new MongoClient(_optionsAccessor.MONGOLAB_URI);
            _db = _client.GetDatabase(Constants.DatabaseName);
            _collection = Constants.EventStoreCollectionName;
        }

        public void Save<T>(IEnumerable<IEvent> events)
        {
            foreach (var @event in events)
            {
                 _db.GetCollection<IEvent>(_collection).InsertOne(@event);
                _publisher.Publish(@event);
            }
        }

        public IEnumerable<IEvent> Get<T>(Guid aggregateId, int fromVersion)
        {
            var filter = Builders<IEvent>.Filter.Eq("AggregateId", aggregateId.ToString());
            var events = _db.GetCollection<IEvent>(_collection).Find(filter).ToEnumerable();

            return events?.Where(x => x.Version > fromVersion) ?? new List<IEvent>();
        }
    }
}
