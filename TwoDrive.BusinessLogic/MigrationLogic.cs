using System;
using System.Collections.Generic;
using TwoDrive.BusinessLogic.Interface;
using TwoDrive.Domain;

namespace TwoDrive.BusinessLogic
{
    public class MigrationLogic : IMigrationLogic
    {
        private IMigration _migration;
        private ILogic<User> _userLogic;
        private FolderElementLogic<Folder> _folderLogic;
        private FolderElementLogic<File> _fileLogic;
        private List<File> files = null;
        private User Owner = null;
        private List<Tuple<string, long>> mapName;
        private string nameRoot = ("-rootFolder");

        public MigrationLogic(IMigration migration, ILogic<User> userLogic, FolderElementLogic<Folder> folderLogic, FolderElementLogic<File> fileLogic)
        {
            _migration = migration;
            _userLogic = userLogic;
            _folderLogic = folderLogic;
            _fileLogic = fileLogic;
            mapName = new List<Tuple<string, long>>();
        }
        
        public void SaveAllFiles()
        {
            if (Owner != null && files != null)
            {
                try
                {
                    
                    foreach (var file in files)
                    {
                        file.Parent = _folderLogic.GetByName(GetNameFolder(file.Parent.Id), Owner.Id);
                        file.Id = 0;
                        file.OwnerId = Owner.Id;
                        file.Readers = null;
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
                    files = _migration.GiveMeFiles();
                    foreach (var folder in folders)
                    {
                        if (folder.Id != 0)
                        {
                            if (!folder.Name.Contains(nameRoot))
                            {
                                var tuple = Tuple.Create<string, long>(folder.Name, folder.Id);
                                mapName.Add(tuple);
                                folder.Parent = _folderLogic.GetByName(GetNameFolder(folder.Parent.Id), Owner.Id);
                                folder.Id = 0;
                                folder.Files = null;
                                folder.Folders = null;
                                folder.Readers = null;
                                folder.OwnerId = Owner.Id;
                                _folderLogic.Add(folder);
                            }
                        }
                    }
                }
                catch (Exception e)
                {
                    throw new Exception("The format is not correct: " + e.Message);
                }
            }
        }

        private string GetNameFolder(long id)
        {
            string ret = null;
            foreach(var tup in mapName)
            {
                if (tup.Item2 == id) ret = tup.Item1;
            }
            return ret;
        }

        public void SaveUser()
        {
            try
            {
                var userMigration = _migration.GiveMeUser();
                long idMigration = userMigration.RootFolder.Id;
                userMigration.RootFolder = null;
                userMigration.FriendList = null;
                userMigration.Id = 0;
                var user = _userLogic.Add(userMigration);
                user.Id = _userLogic.GetUserId(user.Username);
                var tuple = Tuple.Create<string,long>(user.RootFolder.Name, idMigration);
                mapName.Add(tuple);
                Owner = user;
            }
            catch (Exception e)
            {
                throw new Exception("The format is not correct: " + e.Message);
            }
        }
    }
}
