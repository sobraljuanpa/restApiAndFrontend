using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;
using TwoDrive.BusinessLogic.Interface;
using TwoDrive.Domain;

namespace TwoDrive.BusinessLogic
{
    public class XmlMigration : IMigration
    {
        string _path;
        public XmlMigration(string path)
        {
            _path = path;
        }
        private Folder folderRoot()
        {
            try
            {
                //, new XmlRootAttribute("Folder")
                XmlSerializer serializer = new XmlSerializer(typeof(Folder));
                System.IO.FileStream readerXml = new System.IO.FileStream(_path, System.IO.FileMode.Open);
                return (Folder)serializer.Deserialize(readerXml);
            }
            catch (Exception)
            {
                throw new Exception("Impossible deserialize Json.");
            }
        }

        private void GiveFile(Folder folder, ref List<File> files)
        {
            if (folder.Files != null) files.Concat(folder.Files);
            if (folder.Folders == null) return;
            else
            {
                foreach (var fol in folder.Folders)
                {
                    GiveFile(fol, ref files);
                }
            }
        }

        public List<File> GiveMeFiles()
        {
            List<File> fileList = new List<File>();
            Folder root = folderRoot();
            GiveFile(root, ref fileList);
            return fileList;
        }

        private void GiveFolder(Folder folder, ref List<Folder> folders)
        {
            folders.Add(folder);
            if (folder.Folders == null) return;
            else
            {
                foreach (var fol in folder.Folders)
                {
                    GiveFolder(fol, ref folders);
                }
            }
        }

        public List<Folder> GiveMeFolders()
        {
            List<Folder> folderList = new List<Folder>();
            Folder root = folderRoot();
            GiveFolder(root, ref folderList);
            return folderList;
        }

        public User GiveMeUser()
        {
            Folder root = folderRoot();
            return root.Readers.First();
        }
    }
}
}
