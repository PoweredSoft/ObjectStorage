using System;
using System.Reflection;
using MongoDB.Driver;
using PoweredSoft.ObjectStorage.Core;

namespace PoweredSoft.ObjectStorage.MongoDB
{
    public class MongoObjectStorageContext : IObjectStorageContext
    {
        public MongoObjectStorageContext(IMongoDatabase database)
        {
            Database = database;
        }

        public IMongoDatabase Database { get; }

        public IObjectStorageCollection<TEntity> GetCollection<TEntity>()
        {
            var attribute = typeof(TEntity).GetCustomAttribute<MongoCollectionAttribute>();
            if (attribute == null)
                throw new Exception("Must add MongoCollectionAttribute on entity class to use this method.");

            var mongoCollection = Database.GetCollection<TEntity>(attribute.Name);
            var ret = new MongoObjectStorageCollection<TEntity>(mongoCollection);
            return ret;
        }
    }
}
