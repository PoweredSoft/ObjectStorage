using System;
using System.Linq;
using System.Threading.Tasks;
using PoweredSoft.ObjectStorage.MongoDB.Tests.Mock;
using PoweredSoft.ObjectStorage.MongoDB.Tests.Mock.Dal;
using Xunit;

namespace PoweredSoft.ObjectStorage.MongoDB.Tests
{
    public class CrudTests
    {
        [Fact]
        public async Task UpdateMany()
        {
            var osc = MongoDatabaseFactory.GetObjectStorageContext();
            var collection = osc.GetCollection<Contact>();
            var a = await collection.AddAsync(new Contact
            {
                LastName = "A",
                FirstName = "A"
            });

            var b = await collection.AddAsync(new Contact
            {
                LastName = "B",
                FirstName = "B"
            });

            await collection.UpdateManyAsync(t => t.LastName == "A" || t.LastName == "B", t => t.LastName, "C");
            var howManyCs = await collection.GetAllAsync(t => t.LastName == "C");
            var howManyCs2 = await collection.LongCountAsync(t => t.LastName == "C");
            Assert.Equal(2, howManyCs.Count);
            Assert.Equal(2, howManyCs2);


            // clean up.
            await collection.DeleteAsync(a);
            await collection.DeleteAsync(b);
        }

        [Fact]
        public async Task CreateUpdateThenDelete()
        {
            var osc = MongoDatabaseFactory.GetObjectStorageContext();
            var collection = osc.GetCollection<Contact>();
            var contact = await collection.AddAsync(new Contact
            {
                FirstName = "Chuck",
                LastName = "Not Norris"
            });
            Assert.NotNull(contact);


            contact.LastName = "Norris";
            var updatedContact = await collection.UpdateAsync(contact);
            Assert.Equal("Norris", updatedContact.LastName);

            // update name back to not norris
            await collection.UpdateOneAsync(t => t.Id == contact.Id, t => t.LastName, "Not Norris");

            var updatedContact2 = await collection.FirstAsync(t => t.Id == contact.Id);
            Assert.Equal("Not Norris", updatedContact2.LastName);

            // delete the test.
            await collection.DeleteAsync(updatedContact);

            // check that it was deleted.
            var shouldBeNull = collection.AsQueryable().FirstOrDefault(t => t.Id == updatedContact.Id);
            Assert.Null(shouldBeNull);
        }

        [Fact]
        public async Task TestGetAsync()
        {
            var osc = MongoDatabaseFactory.GetObjectStorageContext();
            var collection = osc.GetCollection<Contact>();
            var contact = await collection.AddAsync(new Contact
            {
                FirstName = "David",
                LastName = "Lebee"
            });

            // make sure you can retreive it easily.
            var fetchedUsingGetAsync = await collection.GetAsync(contact.Id);
            Assert.NotNull(fetchedUsingGetAsync);

            Assert.True(await collection.AnyAsync(t => t.Id == contact.Id), "Any async does not work");
            Assert.NotNull(await collection.FirstOrDefaultAsync(t => t.Id == contact.Id));

            // does not crash.
            await collection.FirstAsync(t => t.Id == contact.Id);


            // now delete it.
            await collection.DeleteAsync(fetchedUsingGetAsync);
        }
    }
}
