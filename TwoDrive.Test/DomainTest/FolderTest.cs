using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using TwoDrive.Domain;

namespace TwoDrive.Test
{
    [TestClass]
    public class FolderTest
    {
        Folder folder;

        [TestInitialize]
        public void SetUp()
        {
            folder = new Folder();
            
        }
        [TestMethod]
        public void SetIDTest()
        {
            Guid aux = new Guid("AC937AJS8-6EBE-4A07-BEB9-B7AC59485B90");
            folder.Id = aux;
            Assert.AreEqual(aux, folder.Id);
        }
        [TestMethod]
        public void SetOneFileTest()
        {
            Guid file = new Guid("AC937AJS8-6E9F2-4A07-BEB9-B7AC59485B90");
            folder.AddToList(folder.FilesList,file);
            Assert.IsTrue(folder.FilesList.Contains(file));
        }
        [TestMethod]
        public void RemoveOneFileNotExistTest()
        {
            Guid fileNotExist = new Guid("AC937AJS8-6E9F2-4A07-BEB9-H728JK");
            folder.RemoveFromList(folder.FilesList,fileNotExist);
            Assert.IsTrue(folder.FilesList.Count == 1);
        }
        [TestMethod]
        public void RemoveOneFileTest()
        {
            Guid file = new Guid("AC937AJS8-6E9F2-4A07-BEB9-B7AC59485B90");
            folder.RemoveFromList(folder.FilesList, file);
            Assert.IsTrue(folder.FilesList.Count == 0);
        }

        [TestMethod]
        public void SetTwoFileTest()
        {
            Guid file1 = new Guid("AC937AJS8-6EBE-4A07-BEB9-B5948598J");
            Guid file2 = new Guid("AC937AJS8-6EBE-4A07-BEB9-B5PONH235");
            folder.AddToList(folder.FilesList, file1);
            folder.AddToList(folder.FilesList, file2);
            Assert.IsTrue(folder.FilesList.Count == 2);
        }
        [TestMethod]
        public void RemoveTwoFileTest()
        {
            Guid file1 = new Guid("AC937AJS8-6EBE-4A07-BEB9-B5948598J");
            Guid file2 = new Guid("AC937AJS8-6EBE-4A07-BEB9-B5PONH235");
            folder.RemoveFromList(folder.FilesList, file1);
            folder.RemoveFromList(folder.FilesList, file2);
            Assert.IsTrue(folder.FilesList.Count == 0);
        }

        [TestMethod]
        public void SetOneFolderTest()
        {
            Guid folder1 = new Guid("AC937AJS8-6E9F2-4A07-BEB9-B7AC59485B90");
            folder.AddToList(folder.FoldersList, folder1);
            Assert.IsTrue(folder.FoldersList.Contains(folder1));
        }
        [TestMethod]
        public void RemoveOneFolderNotExistTest()
        {
            Guid folderNotExist = new Guid("AC937AJS8-6E9F2-4A07-BEB9-H728JK");
            folder.RemoveFromList(folder.FoldersList, folderNotExist);
            Assert.IsTrue(folder.FoldersList.Count == 1);
        }
        [TestMethod]
        public void RemoveOneFolderTest()
        {
            Guid folder1 = new Guid("AC937AJS8-6E9F2-4A07-BEB9-B7AC59485B90");
            folder.RemoveFromList(folder.FoldersList, folder1);
            Assert.IsTrue(folder.FoldersList.Count == 0);
        }

        [TestMethod]
        public void SetTwoFolderTest()
        {
            Guid folder1 = new Guid("AC937AJS8-6EBE-4A07-BEB9-B5948598J");
            Guid folder2 = new Guid("AC937AJS8-6EBE-4A07-BEB9-B5PONH235");
            folder.AddToList(folder.FoldersList, folder1);
            folder.AddToList(folder.FoldersList, folder2);
            Assert.IsTrue(folder.FoldersList.Count == 2);
        }
        [TestMethod]
        public void RemoveTwoFolderTest()
        {
            Guid folder1 = new Guid("AC937AJS8-6EBE-4A07-BEB9-B5948598J");
            Guid folder2 = new Guid("AC937AJS8-6EBE-4A07-BEB9-B5PONH235");
            folder.RemoveFromList(folder.FoldersList, folder1);
            folder.RemoveFromList(folder.FoldersList, folder2);
            Assert.IsTrue(folder.FoldersList.Count == 0);
        }

        [TestMethod]
        public void SetOneReaderTest()
        {
            Guid reader = new Guid("AC937AJS8-6E9F2-4A07-BEB9-B7AC59485B90");
            folder.AddToList(folder.ReadersList, reader);
            Assert.IsTrue(folder.ReadersList.Contains(reader));
        }
        [TestMethod]
        public void RemoveOneReaderNotExistTest()
        {
            Guid readerNotExist = new Guid("AC937AJS8-6E9F2-4A07-BEB9-H728JK");
            folder.RemoveFromList(folder.ReadersList, readerNotExist);
            Assert.IsTrue(folder.ReadersList.Count == 1);
        }
        [TestMethod]
        public void RemoveOneReaderTest()
        {
            Guid reader = new Guid("AC937AJS8-6E9F2-4A07-BEB9-B7AC59485B90");
            folder.RemoveFromList(folder.ReadersList, reader);
            Assert.IsTrue(folder.ReadersList.Count == 0);
        }

        [TestMethod]
        public void SetTwoReaderTest()
        {
            Guid reader1 = new Guid("AC937AJS8-6EBE-4A07-BEB9-B5948598J");
            Guid reader2 = new Guid("AC937AJS8-6EBE-4A07-BEB9-B5PONH235");
            folder.AddToList(folder.ReadersList, reader1);
            folder.AddToList(folder.ReadersList, reader2);
            Assert.IsTrue(folder.ReadersList.Count == 2);
        }
        [TestMethod]
        public void RemoveTwoReaderTest()
        {
            Guid reader1 = new Guid("AC937AJS8-6EBE-4A07-BEB9-B5948598J");
            Guid reader2 = new Guid("AC937AJS8-6EBE-4A07-BEB9-B5PONH235");
            folder.RemoveFromList(folder.ReadersList, reader1);
            folder.RemoveFromList(folder.ReadersList, reader2);
            Assert.IsTrue(folder.ReadersList.Count == 0);
        }
        [TestMethod]
        public void SetOneOwnerTest()
        {
            Guid owner = new Guid("AC937AJS8-6E9F2-4A07-BEB9-B7AC59485B90");
            folder.AddToList(folder.OwnersList, owner);
            Assert.IsTrue(folder.OwnersList.Contains(owner));
        }
        [TestMethod]
        public void RemoveOneOwnerNotExistTest()
        {
            Guid ownerNotExist = new Guid("AC937AJS8-6E9F2-4A07-BEB9-H728JK");
            folder.RemoveFromList(folder.OwnersList, ownerNotExist);
            Assert.IsTrue(folder.OwnersList.Count == 1);
        }
        [TestMethod]
        public void RemoveOneOwnerTest()
        {
            Guid owner = new Guid("AC937AJS8-6E9F2-4A07-BEB9-B7AC59485B90");
            folder.RemoveFromList(folder.OwnersList, owner);
            Assert.IsTrue(folder.OwnersList.Count == 0);
        }

        [TestMethod]
        public void SetTwoOwnerTest()
        {
            Guid owner1 = new Guid("AC937AJS8-6EBE-4A07-BEB9-B5948598J");
            Guid owner2 = new Guid("AC937AJS8-6EBE-4A07-BEB9-B5PONH235");
            folder.AddToList(folder.OwnersList, owner1);
            folder.AddToList(folder.OwnersList, owner2);
            Assert.IsTrue(folder.OwnersList.Count == 2);
        }
        [TestMethod]
        public void RemoveTwoOwnerTest()
        {
            Guid owner1 = new Guid("AC937AJS8-6EBE-4A07-BEB9-B5948598J");
            Guid owner2 = new Guid("AC937AJS8-6EBE-4A07-BEB9-B5PONH235");
            folder.RemoveFromList(folder.OwnersList, owner1);
            folder.RemoveFromList(folder.OwnersList, owner2);
            Assert.IsTrue(folder.OwnersList.Count == 0);
        }
        [TestMethod]
        public void SetParentFolderIdTest()
        {
            Guid IdParentFodler = new Guid("AC937AJS8-6EBE-8P07-BEB9-B7AC59485B90");
            folder.ParentFolder = IdParentFodler;
            Assert.AreEqual(IdParentFodler, folder.ParentFolder);
        }
    }
}
