﻿using System;
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

        public static MongoObjectStorageContext GetObjectStorageContext()
        {
            return new MongoObjectStorageContext(GetDatabase());
        }

        public static IMongoClient GetClient()
        {
            var mongoClient = new MongoClient("mongodb://root:example@localhost:27017");
            return mongoClient;
        }
    }
}
