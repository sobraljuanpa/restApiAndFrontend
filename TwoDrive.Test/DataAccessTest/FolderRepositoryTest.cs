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
    public class FolderRepositoryTest
    {
        FolderRepository folderRepository;
        Mock<DbSet<Folder>> mockSet;
        Mock<TwoDriveContext> mockContext;
        DbContextOptions<TwoDriveContext> DbOptions;

        [TestInitialize]
        public void SetUp()
        {
            var data = new List<Folder>
            {
                new Folder{Id = 1, Name = "ObligatorioBD"},
                new Folder{Id = 2, Name = "ObligatorioIngSoft"},
                new Folder{Id = 3, Name = "ObligatorioDisAp"}
            }.AsQueryable();

            //mockeo tabla de vehiculos
            mockSet = new Mock<DbSet<Folder>>();
            mockSet.As<IQueryable<Folder>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<Folder>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<Folder>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<Folder>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());
            mockSet.Setup(m => m.Find(It.IsAny<object[]>())).Returns<object[]>(pk => data.FirstOrDefault(d => d.Id == (int)pk[0]));

            //mockeo contexto y lo hago apuntar al mock de mi db
            DbOptions = new DbContextOptions<TwoDriveContext>();
            mockContext = new Mock<TwoDriveContext>(DbOptions);
            mockContext.Setup(v => v.Folders).Returns(mockSet.Object);

            folderRepository = new FolderRepository(mockContext.Object);
        }

        [TestMethod]
        public void GetAllTest()
        {
            var folders = folderRepository.GetAll();

            Assert.AreEqual("ObligatorioBD", folders.ToList()[0].Name);
        }

        [TestMethod]
        public void GetByIdTest()
        {
            var folder = folderRepository.Get(2);

            Assert.AreEqual("ObligatorioIngSoft", folder.Name);
        }

        [TestMethod]
        public void AddFolderTest()
        {
            var auxFolder = new Folder { Name = "Pepe", Id = 4 };

            folderRepository.Add(auxFolder);

            mockSet.Verify(v => v.Add(It.IsAny<Folder>()), Times.Once());
            mockContext.Verify(e => e.SaveChanges(), Times.Once());
        }

        [TestMethod]
        public void UpdateUserTest()
        {
            var auxFolder = new Folder { Name = "Pepe", Id = 1 };
            var folder = folderRepository.Get(1);

            folderRepository.Update(folder, auxFolder);
            var modifiedFolder = folderRepository.Get(1);

            mockContext.Verify(e => e.SaveChanges(), Times.Once());
            Assert.AreEqual("Pepe", modifiedFolder.Name);
        }

        [TestMethod]
        public void DeleteFolderTest()
        {
            var auxFolder = new Folder { Name = "ObligatorioBD", Id = 1 };

            folderRepository.Delete(auxFolder);

            mockContext.Verify(e => e.SaveChanges(), Times.Once());
        }

    }
}
