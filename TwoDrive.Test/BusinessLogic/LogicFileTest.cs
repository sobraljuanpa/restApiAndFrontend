//using System.Collections.Generic;
//using Microsoft.VisualStudio.TestTools.UnitTesting;
//using TwoDrive.Domain;
//using TwoDrive.BusinessLogic;
//using TwoDrive.Test.Mocks;
//using System;

//namespace TwoDrive.Test.BusinessLogic
//{
//    [TestClass]
//    public class LogicFileTest
//    {
//        FileLogic logicFile;
//        File fileNull;
//        File fileCorrectly;
//        Folder folderNull;
//        Folder folderCorrectly2;
//        Folder folderRoot;
//        User user;
//        User user2;
//        RepositoryFileMock rFi;
//        RepositoryFolderMock rFo;
//        RepositoryUserMock rU;

//        [TestInitialize]
//        public void SetUp()
//        {
//            //  +folderCorrectly(root)
//            //      +folderCorrectly2
//            //          -fileCorrectly

//            rFi = new RepositoryFileMock();
//            rFo = new RepositoryFolderMock();
//            rU = new RepositoryUserMock();

//            user = new User { FirstName = "Jose", LastName = "Goñi", Username = "JoseG", Password = "firulais123", Email = "josepablogoni@gmail.com", Role = "Admin", FriendList = new List<User>(), RootFolder = null };
//            user.Id = 1;
//            folderRoot = new Folder { OwnerId = user.Id, Name = "ROOT", Parent = null, Readers = new List<User>(), Files = new List<File>(), Folders = new List<Folder>() };
//            folderRoot.Parent = folderRoot;
//            folderRoot.Readers.Add(user);
//            folderCorrectly2 = new Folder { OwnerId = user.Id, Name = "Folder1", Parent = null, Readers = new List<User>(), Files = new List<File>(), Folders = new List<Folder>() };
//            folderCorrectly2.Parent = folderRoot;
//            fileCorrectly = new File { OwnerId = user.Id, Name = "file1", Parent = folderCorrectly2, Readers = new List<User>(), Content = "Este es un archivo." };
//            fileCorrectly.Readers.Add(user);
//            fileCorrectly.Id = 3;
//            folderCorrectly2.Files.Add(fileCorrectly);
//            user.RootFolder = folderRoot;
//            folderRoot.Folders.Add(folderCorrectly2);

//            fileNull = new File();
//            folderNull = new Folder();
//            user2 = new User { FirstName = "Juanpa", LastName = "Sobral", Username = "JuanpaS", Password = "firulais123", Email = "junapasobral@gmail.com", Role = "User", FriendList = new List<User>(), RootFolder = null };
//            user.Id = 2;
            
//            rFi.Add(fileCorrectly);
//            rFi.Add(fileNull);
//            rFo.Add(folderRoot);
//            rFo.Add(folderCorrectly2);
//            rU.Add(user);
//            rU.Add(user2);

//            logicFile = new FileLogic(rFi,rFo,rU);
//        }

//        [TestMethod]
//        [ExpectedException(typeof(Exception))]
//        public void AddFileNullToRoot()
//        {
//            logicFile.Move(fileNull.Id, folderRoot.Id);
//        }

//        [TestMethod]
//        public void MoveFileToRootCorrectly()
//        {
//            logicFile.Move(fileCorrectly.Id, folderRoot.Id);
//            Assert.IsTrue(folderRoot.Files.Contains(fileCorrectly));
//        }

//        [TestMethod]
//        [ExpectedException(typeof(Exception))]
//        public void GetFileNotExist()
//        {
//            logicFile.Get(10);
//        }

//        [TestMethod]
//        public void GetFile()
//        {
//            File fileReturn = logicFile.Get(3);
//            Assert.AreEqual(fileReturn, fileCorrectly);
//        }

//        [TestMethod]
//        public void GetAll()
//        {
//            IEnumerable<File> filesReturn = logicFile.GetAll();
//            List<File> files  = new List<File>();
//            files.Add(fileCorrectly);
//            Assert.IsTrue(filesReturn.Equals(files));
//        }

//        [TestMethod]
//        public void AddFile()
//        {
//            List<User> readers = new List<User>();
//            readers.Add(user);
//            File file2 = new File { OwnerId = user.Id, Name = "NEWFILE", Parent = folderCorrectly2, Readers = readers, Content = "This is a new file." };
//            file2.Id = 4;
//            logicFile.Add(file2);
//            Assert.AreEqual(logicFile.Get(4), file2);
//        }

//        [TestMethod]
//        public void UpdateFile()
//        {
//            List<User> readers = new List<User>();
//            readers.Add(user);
//            File file2 = new File { OwnerId = user.Id, Name = "NEWFILE", Parent = folderCorrectly2, Readers = readers, Content = "This is a new file." };
//            file2.Id = 4;
//            logicFile.Add(file2);
//            Assert.AreEqual(logicFile.Get(4), file2);
//        }

//        [TestMethod]
//        [ExpectedException(typeof(Exception))]
//        public void DeleteFile()
//        {
//            logicFile.Delete(logicFile.Get(4));
//            logicFile.Get(4);
//        }

//        [TestMethod]
//        public void AddReaders()
//        {
//            logicFile.AddReader(logicFile.Get(3), user2.Id);
//            Assert.IsTrue(logicFile.Get(3).Readers.Count == 2);
//        }

//        [TestMethod]
//        public void RemoveReaders()
//        {
//            logicFile.RemoveReader(logicFile.Get(3), user2.Id);
//            Assert.IsTrue(logicFile.Get(3).Readers.Count == 1);
//        }
//    }
//}
