using EventModelNamespace;
using SportsAppControllers;

namespace SportsAppTests
{
    [TestClass]
    public class JackTests
    {
        [TestMethod]
        public void TestMethod1()
        {
            string title = "Lazio v Empoli";
            EventDatabaseManagement eventDatabaseManager = new EventDatabaseManagement();
            var events = eventDatabaseManager.fetchAllEvents().Result;
            EventModel currEvent = events.FirstOrDefault(o => o.Title == title);
            Assert.IsTrue(currEvent.Title == title);
        }

        [TestMethod]
        public void TestMethod2()
        {
            string title = "Lazio v Empoli";
            string location = "Stadio Olimpico";
            EventDatabaseManagement eventDatabaseManager = new EventDatabaseManagement();
            var events = eventDatabaseManager.fetchAllEvents().Result;
            EventModel currEvent = events.FirstOrDefault(o => o.Title == title);
            Assert.IsTrue(currEvent.VenueName == location);
        }

        [TestMethod]
        public void TestMethod3()
        {
            string username = "jackw";
            string password = "jackw";
            UserDatabaseManagement userDatabaseManager = new UserDatabaseManagement();
            var user = userDatabaseManager.fetchAccount(username, password).Result;
            string original_eventIDs = user.EventsFollowed;
            int original_followingCount = user.FollowCount;
            string clear_eventIDs = "";
            int clear_followingCount = 0;
            userDatabaseManager.updateAccount(username, clear_eventIDs, clear_followingCount);
            var updatedUser = userDatabaseManager.fetchAccount(username, password).Result;
            //updatedUser
            Assert.IsTrue(updatedUser.EventsFollowed == "" && updatedUser.FollowCount == 0);
            userDatabaseManager.updateAccount(username, original_eventIDs, original_followingCount);
            Assert.IsTrue(updatedUser.EventsFollowed == original_eventIDs && updatedUser.FollowCount == original_followingCount);
        }

        [TestMethod]
        public void TestMethod4()
        {
            string username = "anisha";
            string password = "anisha";
            UserDatabaseManagement userDatabaseManager = new UserDatabaseManagement();
            var user = userDatabaseManager.fetchAccount(username, password).Result;
            string original_eventIDs = user.EventsFollowed;
            int original_followingCount = user.FollowCount;
            string clear_eventIDs = "testID";
            int clear_followingCount = 1;
            userDatabaseManager.updateAccount(username, clear_eventIDs, clear_followingCount);
            var updatedUser = userDatabaseManager.fetchAccount(username, password).Result;
            //updatedUser
            Assert.IsTrue(updatedUser.EventsFollowed == "testID" && updatedUser.FollowCount == 1);
            userDatabaseManager.updateAccount(username, original_eventIDs, original_followingCount);
            Assert.IsTrue(updatedUser.EventsFollowed == original_eventIDs && updatedUser.FollowCount == original_followingCount);
        }
    }
}
