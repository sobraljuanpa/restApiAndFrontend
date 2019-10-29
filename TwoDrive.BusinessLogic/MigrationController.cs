using System;
using System.Text.RegularExpressions;
using TwoDrive.BusinessLogic.Interface;
using TwoDrive.Domain;

namespace TwoDrive.BusinessLogic
{
    public class MigrationController : IMigrationController
    {
        IMigration _migration;
        ILogic<User> _userLogic;
        FolderElementLogic<Folder> _folderLogic;
        FolderElementLogic<File> _fileLogic;
        User Owner = null;
        Folder FolderRoot = null;
        Regex regex = new Regex("$-rootFolder");

        public MigrationController(IMigration migration, ILogic<User> userLogic, FolderElementLogic<Folder> folderLogic, FolderElementLogic<File> fileLogic)
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
                    foreach (var folder in _migration.GiveMeFolders())
                    {
                        if (regex.IsMatch(folder.Parent.Name))
                        {
                            folder.Parent = FolderRoot;
                        }
                        _folderLogic.Add(folder);
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
    }
}
