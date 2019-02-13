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
        public async Task CreateUpdateThenDelete()
        {
            var osc = MongoDatabaseFactory.GetObjectStorageClient();
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

            // delete the test.
            await collection.DeleteAsync(updatedContact);

            // check that it was deleted.
            var shouldBeNull = collection.AsQueryable().FirstOrDefault(t => t.Id == updatedContact.Id);
            Assert.Null(shouldBeNull);
        }

        [Fact]
        public async Task TestGetAsync()
        {
            var osc = MongoDatabaseFactory.GetObjectStorageClient();
            var collection = osc.GetCollection<Contact>();
            var contact = await collection.AddAsync(new Contact
            {
                FirstName = "David",
                LastName = "Lebee"
            });

            // make sure you can retreive it easily.
            var fetchedUsingGetAsync = await collection.GetAsync(contact.Id);
            Assert.NotNull(fetchedUsingGetAsync);

            // now delete it.
            await collection.DeleteAsync(fetchedUsingGetAsync);
        }
    }
}
