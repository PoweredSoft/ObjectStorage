using System;
using MongoDB.Driver;
using PoweredSoft.ObjectStorage.MongoDB.Tests.Mock;
using PoweredSoft.ObjectStorage.MongoDB.Tests.Mock.Dal;
using Xunit;

namespace PoweredSoft.ObjectStorage.MongoDB.Tests
{
    public class CollectionNameAttributeTests
    {
        [Fact]
        public void TestingGetCollection()
        {
            var objectStorageClient = MongoDatabaseFactory.GetObjectStorageContext();
            var collection = objectStorageClient.GetCollection<Contact>();
            Assert.NotNull(collection);
            Assert.NotNull(collection.CollectionName);
        }
    }
}
