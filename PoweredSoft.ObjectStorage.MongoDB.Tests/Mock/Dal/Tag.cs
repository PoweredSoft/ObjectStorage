using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace PoweredSoft.ObjectStorage.MongoDB.Tests.Mock.Dal
{
    [MongoCollection("tags")]
    public class Tag
    {
        [BsonId, BsonElement("id")]
        public ObjectId Id { get; set; }
        [BsonElement("name")]
        public string Name { get; set; }
    }
}
