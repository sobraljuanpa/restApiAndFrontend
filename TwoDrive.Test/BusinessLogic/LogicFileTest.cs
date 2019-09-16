using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TwoDrive.BusinessLogic.Interface;
using TwoDrive.Domain;
using TwoDrive.BusinessLogic;
using TwoDrive.Test.Mocks;
using System;

namespace TwoDrive.Test.BusinessLogic
{
    [TestClass]
    public class LogicFileTest
    {
        ILogic<File> logicFile;
        File fileNull;
        File fileCorrectly;
        Folder folderNull;
        Folder folderCorrectly2;
        Folder folderRoot;
        User user;
        User user2;

        [TestInitialize]
        public void SetUp()
        {
            //  +folderCorrectly(root)
            //      +folderCorrectly2
            //          -fileCorrectly

            RepositoryFileMock rFi = new RepositoryFileMock();
            RepositoryFolderMock rFo = new RepositoryFolderMock();
            RepositoryUserMock rU = new RepositoryUserMock();

            user = new User("Jose", "Goñi", "JoseG", "firulais123", "josepablogoni@gmail.com", true, new List<User>(), null);
            user.Id = 1;
            folderRoot = new Folder(user, "ROOT", null, new List<User>(), new List<File>(), new List<Folder>());
            folderRoot.Parent = folderRoot;
            folderRoot.Readers.Add(user);
            folderCorrectly2 = new Folder(user, "Folder1", null, new List<User>(), new List<File>(), new List<Folder>());
            folderCorrectly2.Parent = folderRoot;
            fileCorrectly = new File(user, "file1", folderCorrectly2, new List<User>(), "Este es un archivo.");
            fileCorrectly.Readers.Add(user);
            fileCorrectly.Id = 3;
            folderCorrectly2.Files.Add(fileCorrectly);
            user.RootFolder = folderRoot;
            folderRoot.Folders.Add(folderCorrectly2);

            fileNull = new File();
            folderNull = new Folder();
            user2 = new User("Juanpa", "Sobral", "JuanpaS", "firulais123", "junapasobral@gmail.com", false, new List<User>(), null);
            user.Id = 2;
            
            rFi.Add(fileCorrectly);
            rFi.Add(fileNull);
            rFo.Add(folderRoot);
            rFo.Add(folderCorrectly2);
            rU.Add(user);
            rU.Add(user2);

            logicFile = new LogicFile(rFi, rU, rFo, user);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void AddFileNullToRoot()
        {
            logicFile.Move(fileNull, folderRoot);
        }

        [TestMethod]
        public void MoveFileToRootCorrectly()
        {
            logicFile.Move(fileCorrectly, folderRoot);
            Assert.IsTrue(folderRoot.Files.Contains(fileCorrectly));
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void getFileNotExist()
        {
            logicFile.Get(10);
        }

        [TestMethod]
        public void getFile()
        {
            File fileReturn = logicFile.Get(3);
            Assert.AreEqual(fileReturn, fileCorrectly);
        }

    }
}
