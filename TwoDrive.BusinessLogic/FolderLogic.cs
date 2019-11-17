using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using TwoDrive.BusinessLogic.Interface;
using TwoDrive.DataAccess.Interface;
using TwoDrive.Domain;

namespace TwoDrive.BusinessLogic
{
    public class FolderLogic : FolderElementLogic<Folder>
    {
        private IDataRepository<File> _fileRepository;
        private IDataRepository<LogItem> _logRepository;
        public FolderLogic(IDataRepository<Folder> repository, IDataRepository<User> userRepository, IDataRepository<File> fileRepository, IDataRepository<LogItem> logRepository)
        {
            base._repository = repository;
            base._userRepository = userRepository;
            _fileRepository = fileRepository;
            _logRepository = logRepository;
        }
        public override Folder Add(Folder entity)
        {
            ValidateFormat(entity);
            entity.Parent = GetFolderId(entity.Parent.Id);
            Folder folderBefore = entity.Parent;
            Folder folderAfter = entity.Parent;
            FoldersNull(folderAfter);
            _repository.Add(entity);
            folderAfter.AddFolder(entity);
            _repository.Update(folderBefore, folderAfter);
            int number = 1 + base.NumberOfFoldersParents(entity.Parent);
            _logRepository.Add(new LogItem(entity.OwnerId, DateTime.Now, number));
            return entity;
        }

        public override void Update(Folder Entity, Folder newEntity)
        {
            FolderElementExists(Entity.Id);
            CopyEntity(Entity, newEntity);
            _repository.Update(Entity, newEntity);
            int number = 1 + base.NumberOfFoldersParents(Entity.Parent);
            _logRepository.Add(new LogItem(Entity.OwnerId, DateTime.Now, number));
        }

        public override void Delete(Folder entity)
        {
            var regex = new Regex("$-rootFolder");
            if (regex.IsMatch(entity.Name))
            {
                throw new Exception("The name format you specified is reserved for user root folders, you can not delete it");
            }
            FolderElementExists(entity.Id);
            DeleteFolder(entity);
            int number = 1 + base.NumberOfFoldersParents(entity.Parent);
            _logRepository.Add(new LogItem(entity.OwnerId, DateTime.Now, number));
        }
        public override void Move(long EntityId, long folderId)
        {
            FolderElementExists(EntityId);
            FolderElementExists(folderId);
            Folder Entity = _repository.Get(EntityId);
            Folder folder = _repository.Get(folderId);
            NotMoveSubFolder(Entity, folder);
            IsTheSameOwner(Entity, folder);
            IsFolderRoot(EntityId);
            FoldersNull(Entity.Parent);
            Entity.Parent.Folders.Remove(Entity);
            folder.Folders.Add(Entity);
            Entity.Parent = folder;
            _repository.Update(Entity.Parent, Entity.Parent);
            _repository.Update(folder, folder);
            _repository.Update(Entity, Entity);
            int number = 1 + base.NumberOfFoldersParents(Entity.Parent);
            _logRepository.Add(new LogItem(Entity.OwnerId, DateTime.Now, number));
        }

        public void AddFatherFolder(Folder folder)
        {
            folder.Parent = _repository.Get(folder.Parent.Id);
        }

        private void CopyEntity(Folder old, Folder newE){
            if (newE.Files == null) newE.Files = old.Files;
            if (newE.Folders == null) newE.Folders = old.Folders;
            if (newE.Name == null) newE.Name = old.Name;
            if (newE.OwnerId == 0) newE.OwnerId = old.OwnerId;
            if (newE.Parent == null) newE.Parent = old.Parent;
            if (newE.Readers == null) newE.Readers = old.Readers;
        }

        private void IsTheSameOwner(Folder entity, Folder folder)
        {
            if(entity.OwnerId != folder.OwnerId)
                throw new Exception("Esta accediendo a carpetas que no son su propiedad.");
        }

        private void FolderNameIsValid(Folder Entity, Folder ParentF)
        {
            if (ParentF.Folders == null) return;
            foreach (var folder in ParentF.Folders)
            {
                if(folder.Name == Entity.Name) throw new Exception("El nombre de la carpeta ya existe.");
            }
        }

        private void NotMoveSubFolder(Folder entity, Folder folder)
        {
            if (entity.Folders == null) return;
            foreach (var f in entity.Folders)
            {
                if(f.Id == folder.Id) throw new Exception("No puede mover a una subcarpeta.");
            }
        }

        private void ValidateFormat(Folder entity)
        {
            Folder folderParent = base.Get(entity.Parent.Id);
            FolderNameIsValid(entity, folderParent);
            var regex = new Regex("$-rootFolder");
            if (regex.IsMatch(entity.Name))
            {
                throw new Exception("The name format you specified is reserved for user root folders");
            }

            NameIsNull(entity.Name);
            ParentIsNull(folderParent);
            if (entity.Files == null) entity.Files = new List<File>();
            if (entity.Folders == null) entity.Folders = new List<Folder>();
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
            OwnerExists(entity.OwnerId);
            ReadersExist(entity.Readers);
        }

        private void IsFolderRoot(long folderId)
        {
            if(_repository.Get(folderId).Parent == null)
                throw new Exception("La carpeta RAIZ no puede ser movida.");
        }

        private void FoldersNull(Folder folderAfter)
        {
            if (folderAfter.Folders == null)
                folderAfter.Folders = new List<Folder>();
        }

        private Folder GetFolderId(long id)
        {
            var folder = _repository.Get(id);
            return folder;
        }

        private void DeleteFolder(Folder folder)
        {
            if (folder.Files != null)
            {
                foreach (File f in folder.Files)
                {
                    _fileRepository.Delete(f);
                }
            }
            if (folder.Folders != null)
            {
                foreach (Folder f in folder.Folders)
                {
                    DeleteFolder(f);
                }
            }
            _repository.Delete(folder);
        }

        public override List<File> FilesShared(long userId)
        {
            throw new NotImplementedException();
        }
    }
}
