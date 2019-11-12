using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;
using TwoDrive.BusinessLogic.Interface;
using TwoDrive.Domain;

namespace TwoDrive.ImportingStrategy
{
    public class XmlMigration : IMigration
    {
        private string _path;
        private Folder folder = null;
        public XmlMigration(string path)
        {
            _path = path;
        }
        private Folder folderRoot()
        {
            try
            {
                if (folder == null)
                {
                    Folder rootFolder;
                    XmlSerializer serializer = new XmlSerializer(typeof(Folder));
                    System.IO.StreamReader reader = new System.IO.StreamReader(_path);
                    rootFolder = (Folder)serializer.Deserialize(reader);
                    folder = rootFolder;
                    return rootFolder;
                }
                else return folder;
            }
            catch (Exception)
            {
                throw new Exception("Impossible deserialize XML.");
            }
        }

        private void GiveFile(Folder folder, ref List<File> files)
        {
            if (folder.Files != null)
            {
                foreach (var fil in folder.Files)
                {
                    files.Add(fil);
                }
            }
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
