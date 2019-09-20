using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using TwoDrive.Domain;
using TwoDrive.DataAccess;
using System.Collections.Generic;
using System.Linq;
using TwoDrive.DataAccess.Interface;

namespace TwoDrive.Test.DataAccessTest
{
    [TestClass]
    public class UserRepositoryTest
    {

        UserRepository userRepository;
        Mock<DbSet<User>> mockSet;
        Mock<TwoDriveContext> mockContext;
        DbContextOptions<TwoDriveContext> DbOptions;

        [TestInitialize]
        public void SetUp()
        {
            var data = new List<User>
            {
                new User { Administrator = true, Email = "pap@pap.pap", FirstName = "Juan", LastName = "Sobral", FriendList = new List<User>(), Id = 1, Password = "123asd", RootFolder = new Folder(), Username = "papap" },
                new User { Administrator = true, Email = "asd@asd.asd", FirstName = "Pedro", LastName = "Marsicano", FriendList = new List<User>(), Id = 2, Password = "123asd", RootFolder = new Folder(), Username = "papap" }
            }.AsQueryable();

            //mockeo tabla de vehiculos
            mockSet = new Mock<DbSet<User>>();
            mockSet.As<IQueryable<User>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<User>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<User>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<User>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());
            mockSet.Setup(m => m.Find(It.IsAny<object[]>())).Returns<object[]>(pk => data.FirstOrDefault(d => d.Id == (int)pk[0]));

            //mockeo contexto y lo hago apuntar al mock de mi db
            DbOptions = new DbContextOptions<TwoDriveContext>();
            mockContext = new Mock<TwoDriveContext>(DbOptions);
            mockContext.Setup(v => v.Users).Returns(mockSet.Object);

            userRepository = new UserRepository(mockContext.Object);
        }

        [TestMethod]
        public void GetAllTest()
        {
            var users = userRepository.GetAll();

            Assert.AreEqual("Pedro", users.ToList()[1].FirstName);
        }

        [TestMethod]
        public void GetByIdTest()
        {
            var user = userRepository.Get(1);

            Assert.AreEqual("Juan", user.FirstName);
        }

        [TestMethod]
        public void AddUserTest()
        {
            var auxUser = new User { FirstName = "Pepe", Id = 3};

            userRepository.Add(auxUser);

            mockSet.Verify(v => v.Add(It.IsAny<User>()), Times.Once());
            mockContext.Verify(e => e.SaveChanges(), Times.Once());
        }

        [TestMethod]
        public void UpdateUserTest()
        {
            var auxUser = new User { FirstName = "NoJuan", Id = 1 };
            var user = userRepository.Get(1);

            userRepository.Update(user, auxUser);
            var modifiedUser = userRepository.Get(1);

            mockContext.Verify(e => e.SaveChanges(), Times.Once());
            Assert.AreEqual("NoJuan", modifiedUser.FirstName);
        }

        [TestMethod]
        public void DeleteUserTest()
        {
            var auxPedro = new User { Administrator = true, Email = "asd@asd.asd", FirstName = "Pedro", LastName = "Marsicano", FriendList = new List<User>(), Id = 2, Password = "123asd", RootFolder = new Folder(), Username = "papap" };

            userRepository.Delete(auxPedro);

            mockContext.Verify(e => e.SaveChanges(), Times.Once());
        }

    }
}
