using System;
using System.Collections.Generic;
using Gen8.Ledger.Core.Infrastructure.Domain;

namespace Gen8.Ledger.Core.Repositories
{
    public interface IRepository<T>
        where T : BaseEntity, new()
    {
        IEnumerable<T> FindAll();
        T Create(T item);
        void Remove(string objectId);
        void Update(string objectId, T entity);
        T FindByObjectId(string objectId);
        T FindByAggregateId(Guid aggregateId);
        T Find(string field, string value);
        bool Exists(Guid aggregateId);
    }
}
