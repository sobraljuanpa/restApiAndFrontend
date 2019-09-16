using Microsoft.VisualStudio.TestTools.UnitTesting;
using TwoDrive.Domain;
using System.Collections.Generic;

namespace TwoDrive.Test.DomainTest
{
    [TestClass]
    public class FolderTest
    {
        User user;
        Folder folder;
        Folder dummyFolder;
        File file;

        [TestInitialize]
        public void SetUp()
        {
            user = new User();
            folder = new Folder();
            folder.Files = new List<File>();
            folder.Folders = new List<Folder>();
            folder.Readers = new List<User>();
            dummyFolder = new Folder();
            file = new File();
        }

        [TestMethod]
        public void AddFileTest()
        {
            folder.AddFile(file);

            Assert.AreEqual(file, folder.Files[0]);
        }

        [TestMethod]
        public void RemoveFileTest()
        {
            folder.AddFile(file);
            folder.RemoveFile(file);

            Assert.AreEqual(0, folder.Files.Count);
        }

        [TestMethod]
        public void AddFolderTest()
        {
            folder.AddFolder(dummyFolder);

            Assert.AreEqual(dummyFolder, folder.Folders[0]);
        }

        [TestMethod]
        public void RemoveFolderTest()
        {
            folder.AddFolder(dummyFolder);
            folder.RemoveFolder(dummyFolder);

            Assert.AreEqual(0, folder.Folders.Count);
        }

        [TestMethod]
        public void AddReaderTest()
        {
            folder.AddReader(user);

            Assert.AreEqual(user, folder.Readers[0]);
        }

        [TestMethod]
        public void RemoveReaderTest()
        {
            folder.AddReader(user);
            folder.RemoveReader(user);

            Assert.AreEqual(0, folder.Readers.Count);
        }

        [TestMethod]
        public void SetNameTest()
        {
            folder.Name = "Obligatorios";

            Assert.AreEqual("Obligatorios", folder.Name);
        }

        [TestMethod]
        public void SetOwnerTest()
        {
            folder.OwnerId = 1;

            Assert.AreEqual(1, folder.OwnerId);
        }

        [TestMethod]
        public void SetParentTest()
        {
            folder.Parent = dummyFolder;

            Assert.AreEqual(dummyFolder, folder.Parent);
        }

    }
}
