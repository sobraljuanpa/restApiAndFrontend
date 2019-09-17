using System;
using System.Collections.Generic;
using TwoDrive.BusinessLogic.Interface;
using TwoDrive.DataAccess.Interface;
using TwoDrive.Domain;

namespace TwoDrive.BusinessLogic
{
    public class LogicFile : ILogic<File>
    {
        IDataRepository<File> _repositoryFile;
        IDataRepository<User> _repositoryUser;
        IDataRepository<Folder> _repositoryFolder;
        User _user;
        public LogicFile(IDataRepository<File> repositoryFile, IDataRepository<User> repositoryUser, IDataRepository<Folder> repositoryFolder, User user)
        {
            _repositoryFile = repositoryFile;
            _repositoryUser = repositoryUser;
            _repositoryFolder = repositoryFolder;
            _user = user;
        }
        public void Add(File entity)
        {
            FormatIsCorrect(entity);
            HasThePermissions(entity.OwnerId);
            entity.LastModifiedDate = DateTime.Now;
            entity.CreationDate = DateTime.Now;
            Folder folderBefore = entity.Parent;
            Folder folderAfter = entity.Parent;
            folderAfter.AddFile(entity);
            _repositoryFile.Add(entity);
        }

        public void Delete(File Entity)
        {
            FileExist(Entity.Id);
            HasThePermissions(Entity.OwnerId);
            _repositoryFile.Delete(Entity);
        }

        public File Get(long id)
        {
            FileExist(id);
            HasThePermissions(_repositoryFile.Get(id).OwnerId);
            return _repositoryFile.Get(id);
        }

        public IEnumerable<File> GetAll()
        {
            return _repositoryFile.GetAll();
        }

        public void Update(File Entity, File newEntity)
        {
            newEntity.LastModifiedDate = DateTime.Now;
            FormatIsCorrect(newEntity);
            FileExist(Entity.Id);
            HasThePermissions(Entity.OwnerId);
            HasTheSameLocation(Entity, newEntity);
            _repositoryFile.Update(Entity, newEntity);
        }

        public void Move(File Entity, Folder folder)
        {
            FileExist(Entity.Id);
            FolderExist(folder.Id);
            FileAlreadyInFolder(Entity, folder);
            HasThePermissions(Entity.OwnerId);
            HasThePermissions(folder.OwnerId);
            Folder folderWhereFileWas = Entity.Parent;
            Folder folderWhereIsIt = folder;
            File filePrevious = Entity;
            Entity.Parent.RemoveFile(Entity);
            _repositoryFolder.Update(folderWhereFileWas, Entity.Parent);
            Entity.Parent = folder;
            folder.AddFile(Entity);
            _repositoryFile.Update(filePrevious,Entity);
            _repositoryFolder.Update(folderWhereIsIt,folder);
        }

        public void AddReader(File Entity, User user)
        {
            HasThePermissions(Entity.OwnerId);
            UserExist(user);
            Entity.AddReader(user);
        }

        public void RemoveReader(File Entity, User user)
        {
            HasThePermissions(Entity.OwnerId);
            UserExist(user);
            Entity.RemoveReader(user);
        }

        private void HasTheSameLocation(File entity, File newEntity)
        {
            if(!entity.Parent.Equals(newEntity.Parent))
                throw new Exception("Las ubicaciones no coinciden.");
        }

        private void UserExist(User user)
        {
            if(_repositoryUser.Get(user.Id) == null)
                throw new Exception("El usuario al que quiere agregar a los lectores no existe.");
        }

        private void HasThePermissions(long ownerId)
        {
            if(_user.Id == ownerId)
                throw new Exception("El usuario no tiene los permisos para esta accion.");
        }

        private void FileAlreadyInFolder(File entity, FolderElement folder)
        {
            if(entity.Parent.Equals(folder))
                throw new Exception("El archivo ya esta en esa carpeta.");
        }

        private void FolderExist(long id)
        {
            if(_repositoryFolder.Get(id) == null)
                throw new Exception("La carpeta no existe.");
        }

        private void FormatIsCorrect(File entity)
        {
            IsNullName(entity.Name);
            IsNullParent(entity.Parent);
            IsNullReaders(entity.Readers);
            IsNullContent(entity.Content);
            TheOwnerExist(entity.OwnerId);
            TheReadersExist(entity.Readers);
        }

        private void TheReadersExist(List<User> readers)
        {
            foreach(User reader in readers)
            {
                if (_repositoryUser.Get(reader.Id) == null)
                    throw new Exception("Uno de los lectores especificados no existe.");
            }
        }

        private void TheOwnerExist(long owners)
        {
            if (_repositoryUser.Get(owners) == null)
                throw new Exception("Uno de los dueños especificado no existe.");
        }

        private void IsNullContent(string content)
        {
            if (content == "")
                throw new Exception("El archivo no contiene nada.");
        }

        private void IsNullReaders(List<User> readers)
        {
            if (readers.Count == 0)
                throw new Exception("El archivo no tiene ningun lector.");
        }

        private void IsNullParent(Folder parent)
        {
            if (parent == null)
                throw new Exception("No existe una carpeta padre para el archivo.");
        }

        private void IsNullName(string name)
        {
            if(name == "")
                throw new Exception("El nombre del archivo está vacio.");
        }

        private void FileExist(long id)
        {
            if(_repositoryFile.Get(id) == null)
                throw new Exception("El archivo no existe.");
        }
    }
}
