using Microsoft.VisualStudio.TestTools.UnitTesting;
using TwoDrive.Domain;
using System.Collections.Generic;
using System;

namespace TwoDrive.Test.DomainTest
{
    [TestClass]
    public class FileTest
    {
        File file;
        Folder folder;
        User user;

        [TestInitialize]
        public void SetUp()
        {
            file = new File();
            folder = new Folder();
            user = new User();
        }

        [TestMethod]
        public void SetContentTest()
        {
            file.Content = "Content inside a file";

            Assert.AreEqual("Content inside a file", file.Content);
        }

        [TestMethod]
        public void SetDateTimeTest()
        {
            var date = DateTime.Now;
            file.CreationDate = date;

            Assert.AreEqual(date, file.CreationDate);
        }

        [TestMethod]
        public void SetLastModifiedDateTest()
        {
            var date = DateTime.Now;
            file.LastModifiedDate = date;

            Assert.AreEqual(date, file.LastModifiedDate);
        }

        [TestMethod]
        public void SetNameTest()
        {
            file.Name = "ObligatorioDisAp2.txt";

            Assert.AreEqual("ObligatorioDisAp2.txt", file.Name);
        }

        [TestMethod]
        public void SetOwnerTest()
        {
            file.OwnerId = 1;

            Assert.AreEqual(1, file.OwnerId);
        }

        [TestMethod]
        public void SetParentTest()
        {
            file.Parent = folder;

            Assert.AreEqual(folder, file.Parent);
        }

        [TestMethod]
        public void AddReaderTest()
        {
            file.Readers = new List<User>();
            file.AddReader(user);

            Assert.AreEqual(user, file.Readers[0]);
        }

        [TestMethod]
        public void RemoveReaderTest()
        {
            file.Readers = new List<User>();
            file.AddReader(user);
            file.RemoveReader(user);

            Assert.AreEqual(0, file.Readers.Count);
        }
    }
}
