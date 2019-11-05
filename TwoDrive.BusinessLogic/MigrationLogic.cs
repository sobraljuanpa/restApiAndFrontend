using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using TwoDrive.BusinessLogic.Interface;
using TwoDrive.Domain;

namespace TwoDrive.BusinessLogic
{
    public class MigrationLogic : IMigrationLogic
    {
        IMigration _migration;
        ILogic<User> _userLogic;
        FolderElementLogic<Folder> _folderLogic;
        FolderElementLogic<File> _fileLogic;
        User Owner = null;
        Folder FolderRoot = null;
        Regex regex = new Regex("$-rootFolder");

        public MigrationLogic(IMigration migration, ILogic<User> userLogic, FolderElementLogic<Folder> folderLogic, FolderElementLogic<File> fileLogic)
        {
            _migration = migration;
            _userLogic = userLogic;
            _folderLogic = folderLogic;
            _fileLogic = fileLogic;
        }
        
        public void SaveAllFiles()
        {
            if (Owner != null)
            {
                try
                {
                    foreach (var file in _migration.GiveMeFiles())
                    {
                        if (regex.IsMatch(file.Parent.Name))
                        {
                            file.Parent = FolderRoot;
                        }
                        _fileLogic.Add(file);
                    }
                }
                catch (Exception e)
                {
                    throw new Exception("The format is not correct: " + e.Message);
                }
            }
        }

        public void SaveAllFolders()
        {
            if (Owner != null)
            {
                try
                {
                    List<Folder> folders = _migration.GiveMeFolders();
                    List<File> files = _migration.GiveMeFiles();
                    ChangeId(ref folders, ref files, _folderLogic.GetAll().Last().Id + 1, _fileLogic.GetAll().Last().Id + 1);
                    foreach (var folder in folders)
                    {
                        if (!regex.IsMatch(folder.Name))
                        {
                            if (regex.IsMatch(folder.Parent.Name))
                            {
                                folder.Parent = FolderRoot;
                            }
                            _folderLogic.Add(folder);
                        }
                    }
                }
                catch (Exception e)
                {
                    throw new Exception("The format is not correct: " + e.Message);
                }
            }
        }

        public void SaveUser()
        {
            try
            {
                var user = _userLogic.Add(_migration.GiveMeUser());
                user.Id = _userLogic.GetUserId(user.Username);
                FolderRoot = user.RootFolder;
            }
            catch (Exception e)
            {
                throw new Exception("The format is not correct: " + e.Message);
            }
        }

        private void ChangeId(ref List<Folder> folders,ref List<File> files,long startIdFolder, long startIdFile)
        {
            long idFolderActually = folders.First().Id;
            long idFilesActually = files.First().Id;
            long variationFile = startIdFile - idFilesActually;
            long variationFolder = startIdFolder - idFolderActually;
            foreach(var folder in folders)
            {
                if (folder.Parent != null) folder.Parent.Id += variationFolder;
                folder.Id += variationFolder;
                foreach(var file in folder.Files)
                {
                    file.Id += variationFile;
                    file.Parent.Id += variationFolder;
                }
            }
        }
    }
}
