using System;
using System.Collections.Generic;
using TwoDrive.BusinessLogic.Interface;
using TwoDrive.DataAccess.Interface;
using TwoDrive.Domain;

namespace TwoDrive.BusinessLogic
{
    public class FileLogicN : FolderElementLogic<File>
    {
        IDataRepository<File> _repository;
        IDataRepository<Folder> _folderRepository;
        public void Add(File entity)
        {
            ValidateFormat(entity);
            entity.LastModifiedDate = DateTime.Now;
            entity.CreationDate = DateTime.Now;
            Folder folderBefore = entity.Parent;
            Folder folderAfter = entity.Parent;
            folderAfter.AddFile(entity);
            _repository.Add(entity);
        }

        public void Update(File Entity, File newEntity)
        {
            newEntity.LastModifiedDate = DateTime.Now;
            ValidateFormat(newEntity);
            FolderElementExists(Entity.Id);
            HasTheSameLocation(Entity, newEntity);
            _repository.Update(Entity, newEntity);
        }
        public void Move(File Entity, Folder folder)
        {
            FolderElementExists(Entity.Id);
            FolderElementExists(folder.Id);
            AlreadyInFolder(Entity, folder);
            Folder folderWhereFileWas = Entity.Parent;
            Folder folderWhereIsIt = folder;
            File filePrevious = Entity;
            Entity.Parent.RemoveFile(Entity);
            _folderRepository.Update(folderWhereFileWas, Entity.Parent);
            Entity.Parent = folder;
            folder.AddFile(Entity);
            _repository.Update(filePrevious, Entity);
            _folderRepository.Update(folderWhereIsIt, folder);
        }

        private void ValidateFormat(File entity)
        {
            NameIsNull(entity.Name);
            ParentIsNull(entity.Parent);
            ReadersIsNull(entity.Readers);
            ContentIsNull(entity.Content);
            OwnerExists(entity.OwnerId);
            ReadersExist(entity.Readers);
        }

        private void ContentIsNull(string content)
        {
            if (content == "")
                throw new Exception("El archivo no contiene nada.");
        }
    }
}
