using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using TwoDrive.BusinessLogic.Interface;
using TwoDrive.DataAccess.Interface;
using TwoDrive.Domain;

namespace TwoDrive.BusinessLogic
{
    public class FileLogic : FolderElementLogic<File>
    {
        IDataRepository<Folder> _folderRepository;

        public FileLogic(IDataRepository<File> repository, IDataRepository<Folder> folderRepository, IDataRepository<User> userRepository)
        {
            base._repository = repository;
            base._userRepository = userRepository;
            _folderRepository = folderRepository;
        }

        public File Add(File entity)
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

        public void Update(File Entity, File newEntity)
        {
            newEntity.LastModifiedDate = DateTime.Now;
            FolderElementExists(Entity.Id);
            CopyEntity(Entity, newEntity);
            _repository.Update(Entity, newEntity);
        }
        public void Move(long EntityId, long folderId)
        {
            FolderElementExists(EntityId);
            FolderElementExists(folderId);
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

        public void Delete(File Entity)
        {
            var regex = new Regex("$-rootFolder");
            if (regex.IsMatch(Entity.Name))
            {
                throw new Exception("The name format you specified is reserved for user root folders, you can not delete it");
            }
            FolderElementExists(Entity.Id);
            _repository.Delete(Entity);
        }

        private void IsTheSameOwner(File entity, Folder folder)
        {
            if (entity.OwnerId != folder.OwnerId)
                throw new Exception("Esta accediendo a carpetas que no son su propiedad.");
        }

        private void ValidateFormat(File entity)
        {
            NameIsNull(entity.Name);
            ParentIsNull(entity);
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

        public List<File> GetAllSortedFiles(string sortOrder = null, string fileName = null)
        {
            var files = from f in _repository.GetAll() select f;

            if (fileName != null)
            {
                files = files.Where(f => f.Name.Contains(fileName));
            }

            if (sortOrder == null)
            {
                sortOrder = "name_desc";
            }

            switch (sortOrder)
            {
                case "name_desc":
                    files = files.OrderByDescending(f => f.Name);
                    break;
                case "name_asc":
                    files = files.OrderBy(f => f.Name);
                    break;
                case "created_desc":
                    files = files.OrderByDescending(f => f.CreationDate);
                    break;
                case "created_asc":
                    files = files.OrderBy(f => f.CreationDate);
                    break;
                case "modified_desc":
                    files = files.OrderByDescending(f => f.LastModifiedDate);
                    break;
                case "modified_asc":
                    files = files.OrderBy(f => f.LastModifiedDate);
                    break;
            }

            return files.ToList();
        }

        public List<File> GetSortedFiles(long userId, string sortOrder = null, string fileName = null)
        {
            var files = from f in _repository.GetAll() select f;

            files = files.Where(f => f.OwnerId == userId);

            if (fileName != null)
            {
                files = files.Where(f => f.Name.Contains(fileName));    
            }

            if(sortOrder == null)
            {
                sortOrder = "name_desc";
            }

            switch (sortOrder)
            {
                case "name_desc":
                    files = files.OrderByDescending(f => f.Name);
                    break;
                case "name_asc":
                    files = files.OrderBy(f => f.Name);
                    break;
                case "created_desc":
                    files = files.OrderByDescending(f => f.CreationDate);
                    break;
                case "created_asc":
                    files = files.OrderBy(f => f.CreationDate);
                    break;
                case "modified_desc":
                    files = files.OrderByDescending(f => f.LastModifiedDate);
                    break;
                case "modified_asc":
                    files = files.OrderBy(f => f.LastModifiedDate);
                    break;
            }
            
            return files.ToList();
        }
    }
}
