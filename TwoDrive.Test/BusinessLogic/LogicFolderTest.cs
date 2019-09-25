using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using TwoDrive.BusinessLogic;
using TwoDrive.DataAccess.Interface;
using TwoDrive.Domain;

namespace TwoDrive.Test.BusinessLogic
{
    [TestClass]
    public class LogicFolderTest
    {
        Mock<IDataRepository<Folder>> folderRepository;
        Mock<IDataRepository<User>> userRepository;
        FolderLogic folderLogic;
        File file;
        Folder folderNull;
        Folder folder;
        Folder folderRoot;

        [TestInitialize]
        public void SetUp()
        {
            folderRoot = new Folder { Parent = null, Readers = null, OwnerId = 0, Name = "ROOT", Files = null, Folders = null, Id = 0 };
            folder = new Folder { Parent = folderRoot, Readers = null, OwnerId = 0, Name = "Folder1", Files = null, Folders = null, Id = 1 };
            file = new File { Content = "Algo de texto.", CreationDate = DateTime.Now, Id = 0, LastModifiedDate = DateTime.Now, Name = "Archivo", OwnerId = 0, Parent = folder, Readers = null };
            folderNull = new Folder();
            folderRepository = new Mock<IDataRepository<Folder>>();
            userRepository = new Mock<IDataRepository<User>>();
            folderLogic = new FolderLogic(folderRepository.Object, userRepository.Object);
            //folderRepository.Setup(f => f.Get(It.IsAny<long>())).Returns(folderRoot);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void AddFolderNullToRoot()
        {
            folderLogic.Add(folderNull);
            folderRepository.VerifyAll(); //No se llaman los metodos del repo.
        }

        [TestMethod]
        public void AddFolderToRoot()
        {
            userRepository.Setup(u => u.Get(It.IsAny<long>())).Returns(new User());
            folderRepository.Setup(f => f.Add(It.IsAny<Folder>()));
            folderRepository.Setup(f => f.Update(It.IsAny<Folder>(), It.IsAny<Folder>()));
            folderLogic.Add(folder);
            folderRepository.VerifyAll();
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void UpdateFolderNull()
        {
            folderLogic.Update(folder, folderNull);
            folderRepository.VerifyAll();
        }


    }
}
