using System;
using MongoDB.Driver;

namespace PoweredSoft.ObjectStorage.MongoDB.Tests.Mock
{
    public class MongoDatabaseFactory
    {
        public static IMongoDatabase GetDatabase()
        {
            var client = GetClient();
            var db = client.GetDatabase("acme");
            return db;
        }

        public static MongoObjectStorageClient GetObjectStorageClient()
        {
            return new MongoObjectStorageClient(GetDatabase());
        }

        public static IMongoClient GetClient()
        {
            var mongoClient = new MongoClient();
            return mongoClient;
        }
    }
}
