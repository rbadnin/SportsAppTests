using MongoDB.Driver;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using SportsAppControllers;
using UserModelNamespace;
using EventModelNamespace;


namespace SportsAppTests
{
    [TestClass]
    public class AnishaTests
    {
        [TestMethod]
        public void CreateEventTest()
        {
            // Validate that accounts are properly added to database.
            string title = "test_title";
            string icon = "test_icon";
            string image = "test_imageURL";
            string venueName = "test_venue name";
            string venueAddress = "test_venue_address";
            string date = "test_date";
            string description = "test_description";
            int follow_count = 3;

            string connectionString = "mongodb+srv://cchannui:cchannui@cluster0.zjtdqmq.mongodb.net/test";
            string databaseName = "sports_app";
            string collectionName = "events";

            // Establish connection to MongoDB.
            var client = new MongoClient(connectionString);
            var db = client.GetDatabase(databaseName);
            var collection = db.GetCollection<EventModel>(collectionName);

            EventDatabaseManagement eventDatabaseManagement = new EventDatabaseManagement();

            // Add event to mongodb.
            eventDatabaseManagement.createEvent(title, icon, image, venueName, venueAddress, date, description, follow_count);

            // Locate event with matching title.
            var result = collection.Find(document => document.Title == title).SingleAsync().Result;

            bool title_equals = result.Title.Equals(title);
            string id = result.Id;

            // Remove event from database
            collection.DeleteOne(document => document.Id == id);

            Assert.IsTrue(title_equals);
        }

        [TestMethod]
        public void TestMethod2()
        {
            // est for null event location
            string location = "Fake location";
            string title = "Trieste vs Trento";
            EventDatabaseManagement eventDatabaseManager = new EventDatabaseManagement();
            var events = eventDatabaseManager.fetchAllEvents().Result;
            EventModel currEvent = events.FirstOrDefault(o => o.Title == title);
            Assert.AreNotEqual(" ", location);
        }


        [TestMethod]
        public void FetchEventTest()
        {
            // Test for proper event
            EventDatabaseManagement eventDatabaseManagement = new EventDatabaseManagement();
            var result = eventDatabaseManagement.fetchEvent("63877f3c6c0dcf2317eccdbf").Result;
            Assert.IsTrue(result.Title.Equals("Fiorentina vs. Lazio"));
        }

        [TestMethod]
        public void FetchEventTest2nd()
        {
            // Test for a nonexistent event
            EventDatabaseManagement eventDatabaseManagement = new EventDatabaseManagement();
            var result = eventDatabaseManagement.fetchEvent("1234567890").Result;
            Assert.IsNull(result);
        }
        

    }
}
