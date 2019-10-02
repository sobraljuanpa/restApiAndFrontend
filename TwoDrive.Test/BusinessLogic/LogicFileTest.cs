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
    public class LogicFileTest
    {
        Mock<IDataRepository<File>> fileRepository;
        Mock<IDataRepository<Folder>> folderRepository;
        Mock<IDataRepository<User>> userRepository;
        FileLogic fileLogic;
        File file;
        File fileNull;
        Folder folder;

        [TestInitialize]
        public void SetUp()
        {
            folder = new Folder { Parent = null, Readers = null, OwnerId = 0, Name = "ROOT", Files = null, Folders = null, Id = 0 };
            file = new File { Content = "Algo de texto.", CreationDate = DateTime.Now, Id = 0, LastModifiedDate = DateTime.Now, Name = "Archivo", OwnerId = 0, Parent = folder, Readers = null };
            fileNull = new File { Parent = folder };
            fileRepository = new Mock<IDataRepository<File>>();
            folderRepository = new Mock<IDataRepository<Folder>>();
            userRepository = new Mock<IDataRepository<User>>();
            fileLogic = new FileLogic(fileRepository.Object, folderRepository.Object, userRepository.Object);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void AddFileNullToRoot()
        {
            fileLogic.Add(fileNull);
            fileRepository.VerifyAll(); //No se llaman los metodos del repo.
        }

        [TestMethod]
        public void AddFileToRoot()
        {
            fileRepository.Setup(f => f.Add(It.IsAny<File>()));
            userRepository.Setup(u => u.Get(It.IsAny<long>())).Returns(new User());
            folderRepository.Setup(f => f.Get(It.IsAny<long>())).Returns(folder);
            folderRepository.Setup(f => f.Add(It.IsAny<Folder>()));
            folderRepository.Setup(f => f.Update(It.IsAny<Folder>(), It.IsAny<Folder>()));
            fileLogic.Add(file);
            fileRepository.VerifyAll();
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void UpdateFileNull()
        {
            fileLogic.Update(file,fileNull);
            fileRepository.VerifyAll();
        }

        [TestMethod]
        public void UpdateFile()
        {
            fileRepository.Setup(f => f.Update(It.IsAny<File>(), It.IsAny<File>()));
            fileRepository.Setup(f => f.Get(It.IsAny<long>())).Returns(new File());
            userRepository.Setup(u => u.Get(It.IsAny<long>())).Returns(new User());
            folderRepository.Setup(f => f.Add(It.IsAny<Folder>()));
            folderRepository.Setup(f => f.Update(It.IsAny<Folder>(), It.IsAny<Folder>()));
            folderRepository.Setup(f => f.Get(It.IsAny<long>())).Returns(new Folder());
            fileLogic.Update(fileNull, file);
            fileRepository.VerifyAll();
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void MoveFileNull()
        {
            fileLogic.Move(file.Id, fileNull.Id);
            fileRepository.VerifyAll();
        }

        [TestMethod]
        public void MoveFile()
        {
            fileRepository.Setup(f => f.Get(It.IsAny<long>())).Returns(file);
            folderRepository.Setup(f => f.Add(It.IsAny<Folder>()));
            folderRepository.Setup(f => f.Update(It.IsAny<Folder>(), It.IsAny<Folder>()));
            Folder folderMove = new Folder { Parent = folder, Readers = null, OwnerId = 0, Name = "folder1", Files = null, Folders = null, Id = 2 };
            folderRepository.Setup(f => f.Get(It.IsAny<long>())).Returns(folderMove);
            fileLogic.Move(file.Id, folder.Id);
            fileRepository.VerifyAll();
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void GetFileNull()
        {
            fileLogic.Get(3);
            fileRepository.VerifyAll();
        }

        [TestMethod]
        public void GetFile()
        {
            fileRepository.Setup(f => f.Get(It.IsAny<long>())).Returns(file);
            fileLogic.Get(file.Id);
            fileRepository.VerifyAll();
        }

        [TestMethod]
        [ExpectedException(typeof(System.ArgumentNullException))]
        public void DeleteFileNull()
        {
            fileLogic.Delete(fileNull);
            fileRepository.VerifyAll();
        }

        [TestMethod]
        public void DeleteFile()
        {
            fileRepository.Setup(f => f.Get(It.IsAny<long>())).Returns(file);
            fileRepository.Setup(f => f.Delete(It.IsAny<File>()));
            fileLogic.Delete(file);
            fileRepository.VerifyAll();
        }

        [TestMethod]
        public void GetAll()
        {
            fileRepository.Setup(f => f.GetAll()).Returns(new List<File>());
            fileLogic.GetAll();
            fileRepository.VerifyAll();
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void AddReaderNotExist()
        {
            fileLogic.AddReader(file,7);
            fileRepository.VerifyAll();
        }

        [TestMethod]
        public void AddReader()
        {
            userRepository.Setup(u => u.Get(It.IsAny<long>())).Returns(new User());
            fileRepository.Setup(f => f.Update(It.IsAny<File>(), It.IsAny<File>()));
            fileLogic.AddReader(file,2);
            fileRepository.VerifyAll();
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void RemoveReaderNotExist()
        {
            fileLogic.RemoveReader(file, 7);
            fileRepository.VerifyAll();
        }

        [TestMethod]
        public void RemoveReader()
        {
            userRepository.Setup(u => u.Get(It.IsAny<long>())).Returns(new User());
            fileRepository.Setup(f => f.Update(It.IsAny<File>(), It.IsAny<File>()));
            fileLogic.RemoveReader(file, 2);
            fileRepository.VerifyAll();
        }
    }
}
