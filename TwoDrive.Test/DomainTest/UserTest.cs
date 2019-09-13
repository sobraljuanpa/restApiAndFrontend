using Microsoft.VisualStudio.TestTools.UnitTesting;
using TwoDrive.Domain;
using System.Collections.Generic;

namespace TwoDrive.Test.DomainTest
{
    [TestClass]
    public class UserTest
    {

        User user;
        User dummy;

        [TestInitialize]
        public void SetUp()
        {
            user = new User();
            dummy = new User();
            user.FriendList = new List<User>();
        }

        [TestMethod]
        public void SetFirstNameTest()
        {
            user.FirstName = "Juan";

            Assert.AreEqual("Juan", user.FirstName);
        }

        [TestMethod]
        public void SetLastNameTest()
        {
            user.LastName = "Perez";

            Assert.AreEqual("Perez", user.LastName);
        }

        [TestMethod]
        public void SetUsernameTest()
        {
            user.Username = "juan.perez";

            Assert.AreEqual("juan.perez", user.Username);
        }

        [TestMethod]
        public void SetPasswordTest()
        {
            user.Password = "firulais123";

            Assert.AreEqual("firulais123", user.Password);
        }

        [TestMethod]
        public void SetEmailTest()
        {
            user.Email = "juan.perez@gmail.com";

            Assert.AreEqual("juan.perez@gmail.com", user.Email);
        }

        [TestMethod]
        public void SetAdministratorTest()
        {
            user.Administrator = true;

            Assert.AreEqual(true, user.Administrator);
        }

        [TestMethod]
        public void AddToFriendListTest()
        {
            user.AddToFriendList(dummy);

            Assert.AreEqual(1, user.FriendList.Count);
        }

        [TestMethod]
        public void RemoveFromFriendListTest()
        {
            user.AddToFriendList(dummy);
            user.RemoveFromFriendList(dummy);

            Assert.AreEqual(0, user.FriendList.Count);
        }

    }
}