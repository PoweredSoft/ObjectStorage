using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;
using PoweredSoft.DynamicLinq.Helpers;
using PoweredSoft.ObjectStorage.Core;

namespace PoweredSoft.ObjectStorage.MongoDB
{
    public class MongoObjectStorageCollection<TEntity> : IObjectStorageCollection<TEntity>
    {
        public MongoObjectStorageCollection(IMongoCollection<TEntity> collection)
        {
            if (collection == null)
                throw new ArgumentNullException("collection");

            Collection = collection;
        }

        public string CollectionName => Collection.CollectionNamespace.CollectionName;

        public IMongoCollection<TEntity> Collection { get; }

        public async Task<TEntity> AddAsync(TEntity entity, CancellationToken cancellationToken = default(CancellationToken))
        {
            await Collection.InsertOneAsync(entity, cancellationToken: cancellationToken);
            return entity;
        }

        public IQueryable<TEntity> AsQueryable()
        {
            return Collection.AsQueryable();
        }

        protected virtual Expression<Func<TEntity, bool>> CreateEntityExpression(TEntity entity)
        {
            var objectKey = GetBsonIdProperty();
            var constant = objectKey.GetValue(entity);
            var expression = QueryableHelpers.CreateConditionExpression<TEntity>(objectKey.Name,
                DynamicLinq.ConditionOperators.Equal, constant, DynamicLinq.QueryConvertStrategy.LeaveAsIs);

            return expression;
        }

        protected virtual PropertyInfo GetBsonIdProperty()
        {
            var objectKey = typeof(TEntity)
                            .GetProperties(BindingFlags.Public | BindingFlags.Instance)
                            .FirstOrDefault(t => t.GetCustomAttribute<BsonIdAttribute>() != null);
            if (objectKey == null)
                throw new Exception("Must have a BsonIdAttribute on one of the properties.");
            return objectKey;
        }

        public async Task DeleteAsync(TEntity entity, CancellationToken cancellationToken = default(CancellationToken))
        {
            var expression = CreateEntityExpression(entity);
            await Collection.DeleteOneAsync(expression);
        }

        public async Task<TEntity> UpdateAsync(TEntity entity, CancellationToken cancellationToken = default(CancellationToken))
        {
            var expression = CreateEntityExpression(entity);
            await Collection.ReplaceOneAsync(expression, entity);
            return entity;
        }

        public Task<List<TEntity>> GetAllAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            return Collection.AsQueryable().ToListAsync(cancellationToken);
        }

        public Task<List<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default(CancellationToken))
        {
            return Collection.Find(predicate).ToListAsync(cancellationToken);
        }

        public Task<TEntity> GetAsync(object key, CancellationToken cancellationToken = default(CancellationToken))
        {
            var keyProp = GetBsonIdProperty();
            var expression = QueryableHelpers.CreateConditionExpression<TEntity>(keyProp.Name,
                DynamicLinq.ConditionOperators.Equal, key, DynamicLinq.QueryConvertStrategy.LeaveAsIs);
            var result = Collection.Find(expression).FirstOrDefaultAsync();
            return result;
        }

        public List<PropertyInfo> GetObjectKeys()
        {
            return new List<PropertyInfo>()
            {
                GetBsonIdProperty()
            };
        }
    }
}
