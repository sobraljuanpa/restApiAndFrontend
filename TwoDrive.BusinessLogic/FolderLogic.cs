using System;
using System.Collections.Generic;
using System.Text;
using TwoDrive.BusinessLogic.Interface;
using TwoDrive.DataAccess.Interface;
using TwoDrive.Domain;

namespace TwoDrive.BusinessLogic
{
    public class FolderLogic : FolderElementLogic<Folder>
    {
        public FolderLogic(IDataRepository<Folder> repository, IDataRepository<User> userRepository)
        {
            base._repository = repository;
            base._userRepository = userRepository;
        }
        public void Add(Folder entity)
        {
            ValidateFormat(entity);
            Folder folderBefore = entity.Parent;
            Folder folderAfter = entity.Parent;
            FoldersNull(folderAfter);
            folderAfter.AddFolder(entity);
            _repository.Update(folderBefore, folderAfter);
            _repository.Add(entity);
        }

        public void Update(Folder Entity, Folder newEntity)
        {
            ValidateFormat(newEntity);
            FolderElementExists(Entity.Id);
            HasTheSameLocation(Entity, newEntity);
            _repository.Update(Entity, newEntity);
        }
        public void Move(long EntityId, long folderId)
        {
            Folder Entity = _repository.Get(EntityId);
            Folder folder = _repository.Get(folderId);
            FolderElementExists(Entity.Id);
            FolderElementExists(folder.Id);
            AlreadyInFolder(Entity, folder);
            IsFolderRoot(folderId);
            Folder folderWhereFolderWas = Entity.Parent;
            Folder folderWhereIsIt = folder.Parent;
            folderWhereIsIt.AddFolder(folder);
            folderWhereFolderWas.RemoveFolder(Entity);
            _repository.Update(Entity.Parent,folderWhereFolderWas);
            _repository.Update(folder.Parent, folderWhereIsIt);
        }

        private void ValidateFormat(Folder entity)
        {
            NameIsNull(entity.Name);
            ParentIsNull(entity.Parent);
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
    }
}
