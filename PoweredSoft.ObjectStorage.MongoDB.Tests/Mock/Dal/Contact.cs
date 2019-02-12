using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace PoweredSoft.ObjectStorage.MongoDB.Tests.Mock.Dal
{
    [MongoCollection("contacts")]
    public class Contact
    {
        [BsonId, BsonElement("id")]
        public ObjectId Id { get; set; }
        [BsonElement("firstName")]
        public string FirstName { get; set; }
        [BsonElement("lastName")]
        public string LastName { get; set; }
    }
}
