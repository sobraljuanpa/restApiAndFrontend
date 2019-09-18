using System;
using System.Collections.Generic;
using System.Text;
using TwoDrive.BusinessLogic.Interface;
using TwoDrive.DataAccess.Interface;
using TwoDrive.Domain;

namespace TwoDrive.BusinessLogic
{
    public class FolderLogicN : FolderElementLogic<Folder>
    {
        IDataRepository<Folder> _repository;
        public void Add(Folder entity)
        {
            ValidateFormat(entity);
            entity.LastModifiedDate = DateTime.Now;
            entity.CreationDate = DateTime.Now;
            Folder folderBefore = entity.Parent;
            Folder folderAfter = entity.Parent;
            folderAfter.AddFile(entity);
            _repository.Add(entity);
        }

        public void Update(Folder Entity, Folder newEntity)
        {
            newEntity.LastModifiedDate = DateTime.Now;
            ValidateFormat(newEntity);
            FolderElementExists(Entity.Id);
            HasTheSameLocation(Entity, newEntity);
            _repository.Update(Entity, newEntity);
        }
        public void Move(Folder Entity, Folder folder)
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

        private void ValidateFormat(Folder entity)
        {
            NameIsNull(entity.Name);
            ParentIsNull(entity.Parent);
            ReadersIsNull(entity.Readers);
            ContentIsNull(entity.Content);
            OwnerExists(entity.OwnerId);
            ReadersExist(entity.Readers);
        }
    }
}
