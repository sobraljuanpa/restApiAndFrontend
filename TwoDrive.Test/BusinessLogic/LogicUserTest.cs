using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using TwoDrive.BusinessLogic;
using TwoDrive.BusinessLogic.Interface;
using TwoDrive.DataAccess.Interface;
using TwoDrive.Domain;

namespace TwoDrive.Test.BusinessLogic
{
    [TestClass]
    public class LogicUserTest
    {
        Mock<IDataRepository<User>> userRepository;
        Mock<IDataRepository<Folder>> folderRepository;
        User userNull;
        User user;
        ILogic<User> userLogic;

        [TestInitialize]
        public void SetUp()
        {
            user = new User { Email = "josepablogoni@gmail.com", FirstName = "Jose Pablo", Username = "josepablogoni", FriendList = new List<User>(), LastName = "Goni", Password = "josepablo", Role = "User" };
            user = new User();
            userRepository = new Mock<IDataRepository<User>>();
            folderRepository = new Mock<IDataRepository<Folder>>();
            userLogic = new UserLogic(userRepository.Object, folderRepository.Object);
        }

        [TestMethod]
        public void AuthenticateUserInSystem()
        {
            userRepository.Setup(u => u.Authenticate(user.Username, user.Password)).Returns(user);
            userLogic.Authenticate(user.Username,user.Password);
            userRepository.VerifyAll();
        }


        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public void AuthenticateUserNotInSystem()
        {
            userRepository.Setup(u => u.Authenticate(userNull.Username, userNull.Password)).Returns<User>(null);
            userLogic.Authenticate(user.Username, user.Password);
            userRepository.VerifyAll();
        }

    }
}
