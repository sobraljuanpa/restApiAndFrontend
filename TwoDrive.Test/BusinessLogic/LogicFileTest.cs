using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TwoDrive.Domain;

namespace TwoDrive.Test.BusinessLogic
{
    [TestClass]
    public class LogicFileTest
    {
        File fileNull;
        File fileCorrectly;
        File fileWithoutPermissions;
        Folder folderNull;
        Folder folderCorrectly;
        Folder folderWithoutPermission;
        User user;

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

    }
}
