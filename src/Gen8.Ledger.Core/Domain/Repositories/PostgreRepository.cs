using System;
using System.Collections.Generic;
using Gen8.Ledger.Core.Infrastructure;
using Gen8.Ledger.Core.Infrastructure.Domain;
using Npgsql;
using Dapper.Contrib.Extensions;
using Microsoft.Extensions.Configuration;
using Gen8.Ledger.Core.Repositories;

namespace Gen8.Ledger.Core.Repositories
{

    public class PostgreRepository<T> : IRelationalRepository<T>
        where T : BaseEntity, new()
    {
        private readonly string _connectionString;
        public PostgreRepository(IConfiguration config) 
        {
            _connectionString = config.GetConnectionString("bankuaccount");
        }
        public T Create(T item)
        {
            using (var sqlConnection = new NpgsqlConnection(_connectionString))
            {
                 sqlConnection.Insert(item);
            }
            return item;
        }

        public bool Exists(Guid aggregateId)
        {
            throw new NotImplementedException();
        }

        public T Find(string field, string value)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<T> FindAll()
        {
            using (var sqlConnection = new NpgsqlConnection(_connectionString))
            {
                return sqlConnection.GetAll<T>();
            }
        }

        public T FindByAggregateId(Guid aggregateId)
        {
            throw new NotImplementedException();
        }

        public T FindByObjectId(int objectId)
        {
            using (var sqlConnection = new NpgsqlConnection(_connectionString))
            {
                return sqlConnection.Get<T>(objectId);
            }
        }

        public void Remove(T entity)
        {
            using (var sqlConnection = new NpgsqlConnection(_connectionString))
            {
                sqlConnection.Delete(entity);
            }
        }

        public void Update(T entity)
        {
            using (var sqlConnection = new NpgsqlConnection(_connectionString))
            {
                sqlConnection.Update(entity);
            }
        }
    }
}