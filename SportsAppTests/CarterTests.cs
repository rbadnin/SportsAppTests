using MongoDB.Driver;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using SportsAppControllers;
using UserModelNamespace;
using EventModelNamespace;


namespace SportsAppTests
{
    [TestClass]
    public class CarterTests
    {
        [TestMethod]
        public void CreateAccountTest()
        {
            // Validate that accounts are properly added to database.
            string username = "test username";
            string password = "test password";
            string location = "test location";
            string access_level = "test access level";
            string events_followed = "test events followed";
            string venue_name = "test venue name";
            string venue_address = "test venue address";
            string event_ids = "test event ids";
            string user_followers = "test user followers";
            int follow_count = 10;

            string connectionString = "mongodb+srv://cchannui:cchannui@cluster0.zjtdqmq.mongodb.net/test";
            string databaseName = "sports_app";
            string collectionName = "users";

            // Establish connection to MongoDB.
            var client = new MongoClient(connectionString);
            var db = client.GetDatabase(databaseName);
            var collection = db.GetCollection<UserModel>(collectionName);

            UserDatabaseManagement userDatabaseManagement = new UserDatabaseManagement();

            // Add document to database.
            userDatabaseManagement.createAccount(username, password, location, access_level, events_followed, venue_name, venue_address, event_ids, user_followers, follow_count);

            // Locate document with matching username.
            var result = collection.Find(document => document.Username == username).SingleAsync().Result;

            bool username_equals = result.Username.Equals(username);
            string id = result.Id;

            // Remove document from database.
            collection.DeleteOne(document => document.Id == id);

            Assert.IsTrue(username_equals);
        }

        [TestMethod]
        public void FetchEventTest1()
        {
            // Test for proper event.
            EventDatabaseManagement eventDatabaseManagement = new EventDatabaseManagement();
            var result = eventDatabaseManagement.fetchEvent("638e0d81e579743e149077e5").Result;
            Assert.IsTrue(result.Title.Equals("Umbria vs. Sicilia"));
        }

        [TestMethod]
        public void FetchEventTest2()
        {
            // Test for a non-existent event.
            EventDatabaseManagement eventDatabaseManagement = new EventDatabaseManagement();
            var result = eventDatabaseManagement.fetchEvent("").Result;
            Assert.IsNull(result);
        }

        [TestMethod]
        public void FetchEventTest3()
        {
            // Test for a incorrect name for event.
            EventDatabaseManagement eventDatabaseManagement = new EventDatabaseManagement();
            var result = eventDatabaseManagement.fetchEvent("incorrect name").Result;
            Assert.IsNull(result);
        }
    }
}