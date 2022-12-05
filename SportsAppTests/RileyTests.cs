using SportsAppControllers;

namespace SportsAppTests
{
    // This class tests fetchAccount()
    [TestClass]
    public class RileyTests
    {
        UserDatabaseManagement userDatabaseManagement = new UserDatabaseManagement();

        // Tests a valid citizen user fetch
        [TestMethod]
        public void TestMethod1()
        {
            string username = "anisha";
            string password = "anisha";
            var result = userDatabaseManagement.fetchAccount(username, password).Result;
            Assert.IsTrue(result.Username == username && result.Password == password);
        }

        // Tests a valid admin user fetch
        [TestMethod]
        public void TestMethod2()
        {
            string username = "admin";
            string password = "admin1234";
            var result = userDatabaseManagement.fetchAccount(username, password).Result;
            Assert.IsTrue(result.Username == username && result.Password == password);
        }

        // Tests a bad username fetch
        [TestMethod]
        public void TestMethod3()
        {
            string username = "badusername";
            string password = "admin1234";
            var result = userDatabaseManagement.fetchAccount(username, password).Result;
            Assert.IsNull(result);
        }

        // Tests a bad password fetch
        [TestMethod]
        public void TestMethod4()
        {
            string username = "admin";
            string password = "badpassword";
            var result = userDatabaseManagement.fetchAccount(username, password).Result;
            Assert.IsNull(result);
        }
    }
}