using System;
namespace PoweredSoft.ObjectStorage.MongoDB
{
    [AttributeUsage(AttributeTargets.Class)]
    public class MongoCollectionAttribute : Attribute
    {
        public MongoCollectionAttribute(string name)
        {
            Name = name;
        }

        public string Name { get; }
    }
}
