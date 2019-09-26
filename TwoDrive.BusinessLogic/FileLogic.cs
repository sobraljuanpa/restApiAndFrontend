using System;
using System.Collections.Generic;
using System.Linq;
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

        public void Add(File entity)
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
        } 

        public void Update(File Entity, File newEntity)
        {
            newEntity.LastModifiedDate = DateTime.Now;
            ValidateFormat(newEntity);
            FolderElementExists(Entity.Id);
            HasTheSameLocation(Entity, newEntity);
            _repository.Update(Entity, newEntity);
        }
        public void Move(long EntityId, long folderId)
        {
            FolderElementExists(EntityId);
            FolderElementExists(folderId);
            File Entity = _repository.Get(EntityId);
            Folder folder = _folderRepository.Get(folderId);
            AlreadyInFolder(Entity, folder);
            Folder folderWhereFileWas = Entity.Parent;
            Folder folderWhereIsIt = folder;
            File filePrevious = Entity;
            folderFileNull(Entity.Parent);
            Entity.Parent.RemoveFile(Entity);
            _folderRepository.Update(folderWhereFileWas, Entity.Parent);
            Entity.Parent = folder;
            folderFileNull(folder);
            folder.AddFile(Entity);
            _repository.Update(filePrevious, Entity);
            _folderRepository.Update(folderWhereIsIt, folder);
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
    }
}
