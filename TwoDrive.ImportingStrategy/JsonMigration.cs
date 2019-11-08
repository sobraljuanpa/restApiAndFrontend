using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using TwoDrive.BusinessLogic.Interface;
using TwoDrive.Domain;

namespace TwoDrive.ImportingStrategy
{
    public class JsonMigration : IMigration
    {
        string _path;
        public JsonMigration(string path)
        {
            _path = path;
        }
        private Folder folderRoot()
        {
            try
            {
                Folder rootFolder;
                using (System.IO.StreamReader jsonStream = System.IO.File.OpenText(_path))
                {
                    var json = jsonStream.ReadToEnd();
                    rootFolder = JsonConvert.DeserializeObject<Folder>(json);
                }
                return rootFolder;
            }
            catch (Exception)
            {
                throw new Exception("Impossible deserialize Json.");
            }
        }

        private void GiveFile(Folder folder, ref List<File> files)
        {
            if (folder.Files != null)
            {
                foreach(var fil in folder.Files)
                {
                    files.Add(fil);
                }
            }
            if (folder.Folders == null) return;
            else
            {
                foreach(var fol in folder.Folders)
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
                foreach(var fol in folder.Folders)
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
