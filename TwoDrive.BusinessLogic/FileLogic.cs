﻿using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using TwoDrive.BusinessLogic.Interface;
using TwoDrive.DataAccess.Interface;
using TwoDrive.Domain;

namespace TwoDrive.BusinessLogic
{
    public class FileLogic : FolderElementLogic<File>
    {
        private IDataRepository<Folder> _folderRepository;
        private IDataRepository<LogItem> _logRepository;

        public FileLogic(IDataRepository<File> repository, IDataRepository<Folder> folderRepository, IDataRepository<User> userRepository, IDataRepository<LogItem> logRepository)
        {
            base._repository = repository;
            base._userRepository = userRepository;
            _folderRepository = folderRepository;
            _logRepository = logRepository;
        }

        public override File Add(File entity)
        {
            ValidateFormat(entity);
            entity.LastModifiedDate = DateTime.Now;
            entity.CreationDate = DateTime.Now;
            entity.Parent = GetFileId(entity.Parent.Id);
            Folder folderBefore = entity.Parent;
            Folder folderAfter = entity.Parent;
            folderFileNull(folderAfter);
            _repository.Add(entity);           
            folderAfter.AddFile(entity);
            _folderRepository.Update(folderBefore, folderAfter);
            return entity;
        } 

        public override void Update(File Entity, File newEntity)
        {
            newEntity.LastModifiedDate = DateTime.Now;
            FolderElementExists(Entity.Id);
            CopyEntity(Entity, newEntity);
            _repository.Update(Entity, newEntity);
            int number = 1 + base.NumberOfFoldersParents(Entity.Parent);
            _logRepository.Add(new LogItem(Entity.OwnerId, DateTime.Now, number));
        }
        public override void Move(long EntityId, long folderId)
        {
            FolderElementExists(EntityId);
            if (_folderRepository.Get(folderId) == null) throw new Exception("El elemento no existe.");
            File Entity = _repository.Get(EntityId);
            Folder folder = _folderRepository.Get(folderId);
            IsTheSameOwner(Entity, folder);
            Folder folderWhereFileWas = Entity.Parent;
            Folder folderWhereIsIt = folder;
            File filePrevious = Entity;
            folderFileNull(Entity.Parent);
            Entity.Parent.RemoveFile(Entity);
            _folderRepository.Update(folderWhereFileWas, Entity.Parent);
            Entity.Parent = folder;
            Entity.LastModifiedDate = DateTime.Now;
            folderFileNull(folder);
            folder.AddFile(Entity);
            _repository.Update(filePrevious, Entity);
            _folderRepository.Update(folderWhereIsIt, folder);
        }

        public override void Delete(File Entity)
        {
            var regex = new Regex("$-rootFolder");
            if (regex.IsMatch(Entity.Name))
            {
                throw new Exception("The name format you specified is reserved for user root folders, you can not delete it");
            }
            FolderElementExists(Entity.Id);
            _repository.Delete(Entity);
            int number = 1 + base.NumberOfFoldersParents(Entity.Parent);
            _logRepository.Add(new LogItem(Entity.OwnerId, DateTime.Now, number));
        }

        private void IsTheSameOwner(File entity, Folder folder)
        {
            if (entity.OwnerId != folder.OwnerId)
                throw new Exception("Esta accediendo a carpetas que no son su propiedad.");
        }

        private void ValidateFormat(File entity)
        {
            NameIsNull(entity.Name);
            if (_folderRepository.Get(entity.Parent.Id) == null) throw new Exception("No existe una carpeta padre para el archivo.");
            try
            {
                ReadersIsNull(entity.Readers);
            }
            catch (Exception)
            {
                List<User> readers = new List<User>();
                if (entity.Parent.Readers == null) entity.Parent.Readers = new List<User>();
                readers = entity.Parent.Readers;
                entity.Readers = readers;
            }
            ContentIsNull(entity.Content);
            OwnerExists(entity.OwnerId);
            ReadersExist(entity.Readers);
        }

        private void CopyEntity(File old, File newF)
        {
            if (newF.Name == null) newF.Name = old.Name;
            if (newF.OwnerId == 0) newF.OwnerId = old.OwnerId;
            if (newF.Parent == null) newF.Parent = old.Parent;
            if (newF.Readers == null) newF.Readers = old.Readers;
            if (newF.Content == null) newF.Content = old.Content;
            if (newF.CreationDate == null) newF.CreationDate = old.CreationDate;
        }

        private void ContentIsNull(string content)
        {
            if (content == "")
                throw new Exception("El archivo no contiene nada.");
        }

        private void folderFileNull(Folder folderAfter)
        {
            if (folderAfter.Files == null)
                folderAfter.Files = new List<File>();
        }

        private Folder GetFileId(long Id)
        {
            var folder = _folderRepository.Get(Id);
            return folder;
        }

        private void ParentsFolders(Folder folder, ref List<Folder> folders)
        {
            if (folder == null) return;
            folders.Add(folder);
            ParentsFolders(folder.Parent, ref folders);
        }

        public override List<File> FilesShared(long userId)
        {
            List<File> files = new List<File>();
            List<Folder> folderShared = new List<Folder>();
            foreach (var folder in _folderRepository.GetAll())
            {
                if(folder.Readers.Count > 0)
                {
                    foreach(var user in folder.Readers)
                    {
                        if (user.Id == userId)
                            if(!folderShared.Contains(folder))
                                folderShared.Add(folder);
                    }
                }
            }
            List<Folder> parentFolders = new List<Folder>();
            bool exist = false;
            foreach (var file in _repository.GetAll())
            {
                this.ParentsFolders(file.Parent, ref parentFolders);
                foreach(var fol in parentFolders)
                {
                    if (folderShared.Contains(fol)) exist = true;
                }
                if (exist)
                {
                    if (!files.Contains(file))
                        files.Add(file);
                }
                else
                {
                    if (file.Readers.Count > 0)
                    {
                        foreach (var user in file.Readers)
                        {
                            if (user.Id == user.Id)
                                if(!files.Contains(file))
                                    files.Add(file);
                        }
                    }
                }
                exist = false;
                parentFolders.RemoveAll(x => x.Id != 0);
            }
            return files;
        }
    }
}
