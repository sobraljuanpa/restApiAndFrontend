using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
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
        public Folder Add(Folder entity)
        {
            ValidateFormat(entity);
            entity.Parent = GetFolderId(entity.Parent.Id);
            Folder folderBefore = entity.Parent;
            Folder folderAfter = entity.Parent;
            FoldersNull(folderAfter);
            _repository.Add(entity);
            folderAfter.AddFolder(entity);
            _repository.Update(folderBefore, folderAfter);
            return entity;
        }

        public void Update(Folder Entity, Folder newEntity)
        {
            FolderElementExists(Entity.Id);
            CopyEntity(Entity, newEntity);
            _repository.Update(Entity, newEntity);
        }
        public void Move(long EntityId, long folderId)
        {  
            FolderElementExists(EntityId);
            FolderElementExists(folderId);
            Folder Entity = _repository.Get(EntityId);
            Folder folder = _repository.Get(folderId);
            IsTheSameOwner(Entity, folder);
            IsFolderRoot(folderId);
            Folder folderWhereFolderWas = Entity.Parent;
            Folder folderWhereIsIt = folder.Parent;
            FoldersNull(folderWhereIsIt);
            folderWhereIsIt.AddFolder(folder);
            folderWhereFolderWas.RemoveFolder(Entity);
            _repository.Update(Entity.Parent,folderWhereFolderWas);
            _repository.Update(folder.Parent, folderWhereIsIt);
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
            foreach (var folder in ParentF.Folders)
            {
                if(folder.Name == Entity.Name) throw new Exception("El nombre de la carpeta ya existe.");
            }
        }

        private void ValidateFormat(Folder entity)
        {
            FolderNameIsValid(entity, entity.Parent);
            var regex = new Regex("$-rootFolder");
            if (regex.IsMatch(entity.Name))
            {
                throw new Exception("The name format you specified is reserved for user root folders");
            }

            NameIsNull(entity.Name);
            ParentIsNull(entity);
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
    }
}
