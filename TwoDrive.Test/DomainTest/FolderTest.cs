using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace TwoDrive.Test
{
    [TestClass]
    public class FolderTest
    {
        FolderTest folder;

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
    }
}
