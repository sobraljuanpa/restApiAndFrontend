using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TwoDrive.Domain;
using TwoDrive.BusinessLogic;
using TwoDrive.Test.Mocks;
using System;

namespace TwoDrive.Test.BusinessLogic
{
    [TestClass]
    public class LogicFolderTest
    {
        FolderLogic logicFolder;
        File fileNull;
        File fileCorrectly;
        Folder folderNull;
        Folder folderCorrectly2;
        Folder folderRoot;
        User user;
        User user2;
        RepositoryFileMock rFi;
        RepositoryFolderMock rFo;
        RepositoryUserMock rU;

        [TestInitialize]
        public void SetUp()
        {
            //  +folderCorrectly(root)
            //      +folderCorrectly2
            //          -fileCorrectly

            rFi = new RepositoryFileMock();
            rFo = new RepositoryFolderMock();
            rU = new RepositoryUserMock();

            user = new User { FirstName = "Jose", LastName = "Goñi", Username = "JoseG", Password = "firulais123", Email = "josepablogoni@gmail.com", Administrator = true, FriendList = new List<User>(), RootFolder = null };
            user.Id = 1;
            folderRoot = new Folder { OwnerId = user.Id, Name = "ROOT", Parent = null, Readers = new List<User>(), Files = new List<File>(), Folders = new List<Folder>() };
            folderRoot.Parent = folderRoot;
            folderRoot.Readers.Add(user);
            folderCorrectly2 = new Folder { OwnerId = user.Id, Name = "Folder1", Parent = null, Readers = new List<User>(), Files = new List<File>(), Folders = new List<Folder>() };
            folderCorrectly2.Parent = folderRoot;
            fileCorrectly = new File { OwnerId = user.Id, Name = "file1", Parent = folderCorrectly2, Readers = new List<User>(), Content = "Este es un archivo." };
            fileCorrectly.Readers.Add(user);
            fileCorrectly.Id = 3;
            folderCorrectly2.Files.Add(fileCorrectly);
            user.RootFolder = folderRoot;
            folderRoot.Folders.Add(folderCorrectly2);
            folderNull = new Folder();

            fileNull = new File();
            folderNull = new Folder();
            user2 = new User { FirstName = "Juanpa", LastName = "Sobral", Username = "JuanpaS", Password = "firulais123", Email = "junapasobral@gmail.com", Administrator = false, FriendList = new List<User>(), RootFolder = null };
            user.Id = 2;

            rFi.Add(fileCorrectly);
            rFi.Add(fileNull);
            rFo.Add(folderRoot);
            rFo.Add(folderCorrectly2);
            rU.Add(user);
            rU.Add(user2);

            logicFolder = new FolderLogic(rFo, rU);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void AddFolderNullToRoot()
        {
            logicFolder.Move(folderNull.Id, folderRoot.Id);
        }

        [TestMethod]
        public void MoveFolderToRootCorrectly()
        {
            logicFolder.Move(folderCorrectly2.Id, folderRoot.Id);
            Assert.IsTrue(folderRoot.Files.Contains(fileCorrectly));
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void GetFolderNotExist()
        {
            logicFolder.Get(10);
        }

        




    }
}
