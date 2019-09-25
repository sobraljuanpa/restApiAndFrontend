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
        Mock<IDataRepository<File>> fileRepository;
        Mock<IDataRepository<Folder>> folderRepository;
        Mock<IDataRepository<User>> userRepository;
        FolderLogic folderLogic;
        File file;
        Folder folderNull;
        Folder folder;

        [TestInitialize]
        public void SetUp()
        {
            folder = new Folder { Parent = null, Readers = null, OwnerId = 0, Name = "ROOT", Files = null, Folders = null, Id = 0 };
            file = new File { Content = "Algo de texto.", CreationDate = DateTime.Now, Id = 0, LastModifiedDate = DateTime.Now, Name = "Archivo", OwnerId = 0, Parent = folder, Readers = null };
            folderNull = new Folder {};
            fileRepository = new Mock<IDataRepository<File>>();
            folderRepository = new Mock<IDataRepository<Folder>>();
            userRepository = new Mock<IDataRepository<User>>();
            folderLogic = new FolderLogic(folderRepository.Object, userRepository.Object);
        }

        


    }
}
