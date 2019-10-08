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
        Folder folder;
        ILogic<User> userLogic;

        [TestInitialize]
        public void SetUp()
        {
            folder = new Folder { Name = "josepablogoni-rootFolder", Id = 1 };
            user = new User { Id = 1, Email = "josepablogoni@gmail.com", FirstName = "Jose Pablo", Username = "josepablogoni", FriendList = new List<User>(), LastName = "Goni", Password = "josepablo", Role = "User" };
            folder.OwnerId = user.Id;
            userNull = new User();
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
        public void AuthenticateUserNotInSystem()
        {
            userRepository.Setup(u => u.Authenticate(userNull.Username, userNull.Password)).Returns<User>(null);
            userLogic.Authenticate(userNull.Username, userNull.Password);
            userRepository.VerifyAll();
        }

        [TestMethod]
        public void GetUserIdInSystem()
        {
            IEnumerable<User> list = new List<User> { user};
            userRepository.Setup(u => u.GetAll()).Returns(list);
            long id = userLogic.GetUserId(user.Username);
            Assert.AreEqual(id, user.Id);
            userRepository.VerifyAll();
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void GetUserIdNotInSystem()
        {
            userRepository.Setup(u => u.GetAll()).Returns<User>(null);
            long id = userLogic.GetUserId(userNull.Username);
            userRepository.VerifyAll();
        }

        [TestMethod]
        public void GetUserRootfoldeId()
        {
            IEnumerable<Folder> list = new List<Folder> { folder };
            folderRepository.Setup(f => f.GetAll()).Returns(list);
            long id = userLogic.GetUserRootFolderId(user.Username);
            Assert.AreEqual(id, folder.Id);
            userRepository.VerifyAll();
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void GetUserNotInSystemRootfoldeId()
        {
            folderRepository.Setup(f => f.GetAll()).Returns<Folder>(null);
            long id = userLogic.GetUserRootFolderId(userNull.Username);
            userRepository.VerifyAll();
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void AddUserNull()
        {
            userLogic.Add(userNull);
        }

        [TestMethod]
        public void AddUserCorrect()
        {
            userRepository.Setup(u => u.Get(user.Id)).Returns(user);
            IEnumerable<User> list = new List<User> { user };
            userRepository.Setup(u => u.GetAll()).Returns(list);
            folderRepository.Setup(f => f.Get(folder.Id)).Returns(folder);
            IEnumerable<Folder> listF = new List<Folder> { folder };
            folderRepository.Setup(f => f.GetAll()).Returns(listF);
            var userAux = userLogic.Add(user);
            Assert.AreEqual(userAux, user);
            userRepository.VerifyAll();
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void DeleteUserNull()
        {
            userLogic.Delete(userNull);
        }

        [TestMethod]
        public void DeleteUser()
        {
            userRepository.Setup(u => u.Get(user.Id)).Returns(user);
            userLogic.Delete(user);
            userRepository.VerifyAll();
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void GetUserNull()
        {
            userRepository.Setup(u => u.Get(0)).Returns<User>(null);
            userLogic.Get(0);
        }

        [TestMethod]
        public void GetUser()
        {
            userRepository.Setup(u => u.Get(user.Id)).Returns(user);
            var userAux = userLogic.Get(user.Id);
            Assert.AreEqual(userAux, user);
            userRepository.VerifyAll();
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void UpdateUserNotInSystem()
        {
            userLogic.Update(user,userNull);
        }

       

    }
}
