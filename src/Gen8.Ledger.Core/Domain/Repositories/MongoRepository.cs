using MongoDB.Bson;
using MongoDB.Driver;
using System.Collections.Generic;
using Gen8.Ledger.Core.Infrastructure;
using Gen8.Ledger.Core.Infrastructure.Domain;
using Microsoft.Extensions.Options;
using System.Linq;
using System;

namespace Gen8.Ledger.Core.Repositories
{
    public class MongoRepository<T> : IRepository<T>
        where T : BaseEntity, new()
    {
        private readonly AppConfig _optionsAccessor;
        MongoClient _client;
        IMongoDatabase _db;
        string _collection;
        public MongoRepository(IOptions<AppConfig> optionsAccessor)
        {
            _optionsAccessor = optionsAccessor.Value;
            _client = new MongoClient(_optionsAccessor.MONGOLAB_URI);
            _db = _client.GetDatabase(Constants.DatabaseName);
            _collection = typeof(T).Name;
        }
        public virtual IEnumerable<T> FindAll()
        {
            return _db.GetCollection<T>(_collection).Find(r => true).ToList();
        }

        public virtual T FindByObjectId(string objectId)
        {

            var filter = Builders<T>.Filter.Eq(r => r.ObjectId, new ObjectId(objectId));
            return _db.GetCollection<T>(_collection).Find(filter).First();
        }

        public virtual T FindByAggregateId(Guid aggregateId)
        {
            var filter = Builders<T>.Filter.Eq("AggregateId", aggregateId);
            return _db.GetCollection<T>(_collection).Find(filter).First();
        }
        public virtual T Find(string field, string value)
        {
            var filter = Builders<T>.Filter.Eq(field, value);
            var result = _db.GetCollection<T>(_collection).Find(filter);
            if (result.Count() > 0)
                return result.First();
            return null;
        }
        public virtual bool Exists(Guid aggregateId)
        {
            var filter = Builders<T>.Filter.Eq("AggregateId", aggregateId);
            var count = _db.GetCollection<T>(_collection).Find(filter).Count();
            return count > 0;
        }
        public virtual T Create(T p)
        {
            _db.GetCollection<T>(_collection).InsertOne(p);
            return p;
        }

        public virtual void Update(string objectId, T p)
        {
            var currentObjectId = new ObjectId(objectId);
            p.ObjectId = currentObjectId;
            var filter = Builders<T>.Filter.Eq(r => r.ObjectId, currentObjectId);
            var operation = _db.GetCollection<T>(_collection).FindOneAndReplace(filter, p);
        }
        public virtual void Remove(string objectId)
        {
            var filter = Builders<T>.Filter.Eq(r => r.ObjectId, new ObjectId(objectId));
            var operation = _db.GetCollection<T>(_collection).FindOneAndDelete(filter);
        }
    }
}
