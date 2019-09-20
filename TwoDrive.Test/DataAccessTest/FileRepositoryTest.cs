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
    public class FileRepositoryTest
    {

        FileRepository fileRepository;
        Mock<DbSet<File>> mockSet;
        Mock<TwoDriveContext> mockContext;
        DbContextOptions<TwoDriveContext> DbOptions;

        [TestInitialize]
        public void SetUp()
        {
            var data = new List<File>
            {
                new File {Name = "algo.txt", Id=1},
                new File { Name = "tarea.hs", Id=2}
            }.AsQueryable();

            //mockeo tabla de vehiculos
            mockSet = new Mock<DbSet<File>>();
            mockSet.As<IQueryable<File>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<File>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<File>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<File>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());
            mockSet.Setup(m => m.Find(It.IsAny<object[]>())).Returns<object[]>(pk => data.FirstOrDefault(d => d.Id == (int)pk[0]));

            //mockeo contexto y lo hago apuntar al mock de mi db
            DbOptions = new DbContextOptions<TwoDriveContext>();
            mockContext = new Mock<TwoDriveContext>(DbOptions);
            mockContext.Setup(v => v.Files).Returns(mockSet.Object);

            fileRepository = new FileRepository(mockContext.Object);
        }

        [TestMethod]
        public void GetAllTest()
        {
            var users = fileRepository.GetAll();

            Assert.AreEqual("tarea.hs", users.ToList()[1].Name);
        }

        [TestMethod]
        public void GetByIdTest()
        {
            var file = fileRepository.Get(1);

            Assert.AreEqual("algo.txt", file.Name);
        }

        [TestMethod]
        public void AddFileTest()
        {
            var auxFile = new File { Name = "Pepe", Id = 3 };

            fileRepository.Add(auxFile);

            mockSet.Verify(v => v.Add(It.IsAny<File>()), Times.Once());
            mockContext.Verify(e => e.SaveChanges(), Times.Once());
        }

        [TestMethod]
        public void UpdateFileTest()
        {
            var auxFile = new File { Name = "otracosa.txt", Id = 1 };
            var file = fileRepository.Get(1);

            fileRepository.Update(file, auxFile);
            var modifiedFile = fileRepository.Get(1);

            mockContext.Verify(e => e.SaveChanges(), Times.Once());
            Assert.AreEqual("otracosa.txt", modifiedFile.Name);
        }

        [TestMethod]
        public void DeleteFileTest()
        {
            var auxFile = new File { Name = "algo.txt", Id = 1 };

            fileRepository.Delete(auxFile);

            mockContext.Verify(e => e.SaveChanges(), Times.Once());
        }

    }
}
