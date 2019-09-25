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
        Folder folder;

        [TestInitialize]
        public void SetUp()
        {
            folder = new Folder { Parent = null, Readers = null, OwnerId = 0, Name = "ROOT", Files = null, Folders = null, Id = 0 };
            file = new File { Content = "Algo de texto.", CreationDate = DateTime.Now, Id = 0, LastModifiedDate = DateTime.Now, Name = "Archivo", OwnerId = 0, Parent = folder, Readers = null };
            fileRepository = new Mock<IDataRepository<File>>();
            folderRepository = new Mock<IDataRepository<Folder>>();
            userRepository = new Mock<IDataRepository<User>>();
            fileLogic = new FileLogic(fileRepository.Object, folderRepository.Object, userRepository.Object);
            //fileRepository.Setup(f => f.Add(It.IsAny<File>()));
            //fileRepository.Setup(f => f.Update(It.IsAny<File>(), It.IsAny<File>()));
            //folderRepository.Setup(f => f.Add(It.IsAny<Folder>()));
            //folderRepository.Setup(f => f.Update(It.IsAny<Folder>(), It.IsAny<Folder>()));
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void AddFileNullToRoot()
        {
            File fileNull = new File();
            fileRepository.VerifyAll(); //No se llaman los metodos del repo.
            fileLogic.Add(fileNull);
        }

        [TestMethod]
        public void AddFileToRoot()
        {
            fileRepository.Setup(f => f.Add(It.IsAny<File>()));
            userRepository.Setup(u => u.Get(It.IsAny<long>())).Returns(new User());
            folderRepository.Setup(f => f.Add(It.IsAny<Folder>()));
            folderRepository.Setup(f => f.Update(It.IsAny<Folder>(), It.IsAny<Folder>()));
            fileLogic.Add(file);
            fileRepository.VerifyAll();
        }

    }
}
