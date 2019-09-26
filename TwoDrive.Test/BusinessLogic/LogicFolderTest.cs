using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
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
            folderNull = new Folder { Parent = folderRoot};
            folderRepository = new Mock<IDataRepository<Folder>>();
            userRepository = new Mock<IDataRepository<User>>();
            folderLogic = new FolderLogic(folderRepository.Object, userRepository.Object);
            
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

        [TestMethod]
        public void UpdateFolder()
        {
            folderRepository.Setup(f => f.Update(It.IsAny<Folder>(), It.IsAny<Folder>()));
            folderRepository.Setup(f => f.Get(It.IsAny<long>())).Returns(folderRoot);
            userRepository.Setup(u => u.Get(It.IsAny<long>())).Returns(new User());
            folderLogic.Update(folderNull, folder);
            folderRepository.VerifyAll();
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void MoveFolderNull()
        {
            folderLogic.Move(folder.Id, folderNull.Id);
            folderRepository.VerifyAll();
        }

        [TestMethod]
        public void MoveFolder()
        {
            folderRepository.Setup(f => f.Update(It.IsAny<Folder>(), It.IsAny<Folder>()));
            folderRepository.Setup(f => f.Get(It.IsAny<long>())).Returns(new Folder { Parent = new Folder { Id = 8} });
            userRepository.Setup(u => u.Get(It.IsAny<long>())).Returns(new User());
            Folder folderMove = new Folder { Parent = folder, Readers = null, OwnerId = 0, Name = "Folder2", Files = null, Folders = null, Id = 2 };
            folderLogic.Move(folder.Id, folderMove.Id);
            folderRepository.VerifyAll();
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void GetFolderNull()
        {
            folderLogic.Get(3);
            folderRepository.VerifyAll();
        }

        [TestMethod]
        public void GetFolder()
        {
            folderRepository.Setup(f => f.Get(It.IsAny<long>())).Returns(folder);
            folderLogic.Get(file.Id);
            folderRepository.VerifyAll();
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void DeleteFolderNull()
        {
            folderLogic.Delete(folderNull);
            folderRepository.VerifyAll();
        }

        [TestMethod]
        public void DeleteFolder()
        {
            folderRepository.Setup(f => f.Get(It.IsAny<long>())).Returns(folder);
            folderRepository.Setup(f => f.Delete(It.IsAny<Folder>()));
            folderLogic.Delete(folder);
            folderRepository.VerifyAll();
        }

        [TestMethod]
        public void GetAll()
        {
            folderRepository.Setup(f => f.GetAll()).Returns(new List<Folder>());
            folderLogic.GetAll();
            folderRepository.VerifyAll();
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void AddReaderNotExist()
        {
            folderLogic.AddReader(folder, 7);
            folderRepository.VerifyAll();
        }
    }
}
